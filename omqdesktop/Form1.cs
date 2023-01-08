using Microsoft.VisualBasic.ApplicationServices;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json.Nodes;
using NAudio;
using NAudio.Wave;
using System.Diagnostics.Contracts;

namespace omqdesktop
{
    public partial class Form1 : Form
    {
        string api_key;
        HttpClient client = new HttpClient();
        List<string> titleArtist = new List<string>();
        List<string> previewUrls = new List<string>();
        List<string> coverImgs = new List<string>();
        int currentSong = 0;
        int currentScore = 0;
        WaveOutEvent waveoutevent = new WaveOutEvent();
        int cbwidth = 383;
        int cbheight = 28;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            var field = typeof(System.Net.Http.Headers.HttpRequestHeaders)
                .GetField("invalidHeaders", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
              ?? typeof(System.Net.Http.Headers.HttpRequestHeaders)
                .GetField("s_invalidHeaders", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            if (field != null)
            {
                var invalidFields = (HashSet<string>)field.GetValue(null);
                invalidFields.Remove("Content-Type");
            }
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var values = new Dictionary<string, string>
            {
                { "client_id", "" },
                { "client_secret", "" },
                { "grant_type", "client_credentials" },
                { "scope", "public" }
            };

            var contentReq = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://osu.ppy.sh/oauth/token", contentReq);

            var responseString = await response.Content.ReadAsStringAsync();
            var APIresponseObj = JsonNode.Parse(responseString);
            api_key = (APIresponseObj["access_token"].ToString());
            btnStart.Visible = false;

            panel1.Left = 12;
            panel1.Top = 12;
            panel1.Visible = true;
            label2.Text = "This will now get all top beatmaps from the player ID's default game mode, randomize them, and then do a \nlookup to retrieve both an .mp3 preview and the title + artist of every single one of them. \nThis takes a while, so please be patient.";

        }

        private async void btnGetTops_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            string user_id = textBox1.Text + "/";
            string type = "best/";

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + api_key);

            string urlUser = "https://osu.ppy.sh/api/v2/users/" + user_id + "scores/" + type + "?limit=100";

            List<string> beatmapIds = new List<string>();

            using (HttpResponseMessage res = await client.GetAsync(urlUser))
            {
                using (HttpContent content = res.Content)
                {
                    var data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        var dataObj = JsonNode.Parse(data);
                        if (dataObj is JsonArray jArray)
                        {
                            foreach (var i in jArray)
                            {
                                beatmapIds.Add(i["beatmap"]["id"].ToString());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("NO Data----------");
                    }
                }
            }

            Console.WriteLine(beatmapIds.Count);
            //Shuffle beatmaps
            beatmapIds.Shuffle();

            string urlBeatmap = "https://osu.ppy.sh/api/v2/beatmaps/lookup/";

            for (var i = 0; i < beatmapIds.Count - 1; i++)
            {
                progressBar1.Value++;
                using (HttpResponseMessage res = await client.GetAsync(urlBeatmap + "?id=" + beatmapIds[i]))
                {
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            var dataObj = JsonNode.Parse(data);
                            coverImgs.Add(dataObj["beatmapset"]["covers"]["list@2x"].ToString());
                            titleArtist.Add(dataObj["beatmapset"]["artist"].ToString() + " - " + dataObj["beatmapset"]["title"].ToString());
                            previewUrls.Add(dataObj["beatmapset"]["preview_url"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("There's no data?");
                        }
                    }
                }
            }

            panel1.Visible = false;
            updateLbls();
            panel2.Visible = true;           
            panel2.Top = 0;
            panel2.Left = 0;
            panel2.Dock = DockStyle.Fill;
            //pictureBox1.Dock = DockStyle.Top;            
            comboBox1.DropDownHeight = 200;
            comboBox1.DroppedDown = true;
            comboBox1.DropDownStyle = ComboBoxStyle.Simple;
            comboBox1.Size = new System.Drawing.Size(cbwidth, cbheight);
        }

        public void PlayMp3FromUrl(string url)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {                    
                    waveoutevent.Init(blockAlignedStream);
                    waveoutevent.Play();
                    while (waveoutevent.PlaybackState == PlaybackState.Playing)
                    {
                    }
                }
            }
        }

        private void startMp3Task()
        {
            Task.Factory.StartNew(() => PlayMp3FromUrl("https:" + previewUrls[currentSong]));
        }

        private async void loadImg()
        {
             pictureBox1.Load(coverImgs[currentSong]);
        }

        private void btnPlay_Click_1(object sender, EventArgs e)
        {
            startMp3Task();
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.Text.Length != 0 && !e.KeyCode.Equals(Keys.Up) && !e.KeyCode.Equals(Keys.Down))
            {
                int selectionStart = comboBox1.SelectionStart;
                comboBox1.Items.Clear();
                for (var i = 0; i < titleArtist.Count - 1; i++)
                {
                    var artist = titleArtist[i];
                    if (artist.ToLower().Contains(comboBox1.Text.ToLower()))
                    {
                        comboBox1.Items.Add(artist);
                    }
                }
                comboBox1.SelectionStart = selectionStart;
                comboBox1.SelectionLength = 0;
                comboBox1.SelectedIndex = -1;
                comboBox1.Size = new System.Drawing.Size(cbwidth, cbheight*10);
            }
            else
            {
                if (comboBox1.Text.Length == 0)
                {
                    comboBox1.Items.Clear();
                    comboBox1.Size = new System.Drawing.Size(cbwidth, cbheight);
                }
                else
                {

                }
            }
            if (e.KeyCode.Equals(Keys.Return))
            {
                comboBox1.Size = new System.Drawing.Size(cbwidth, cbheight);
            }
        }

        private void updateLbls()
        {
            lblCounter.Text = "Current Song: " + (currentSong+1).ToString() + "/" + (titleArtist.Count+1).ToString();
            lblScore.Text = "Score: " + (currentScore).ToString() + "/" + currentSong.ToString();
            //lblScore;

        }
        private void btnSkip_Click(object sender, EventArgs e)
        {
            lblGuess.Text = "You skipped!\nThe beatmap was " + titleArtist[currentSong].ToString();
            lblGuess.ForeColor = Color.Red;
            goesToNextSong();
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            string guess = comboBox1.Text;
            if (guess.ToLower().Equals(titleArtist[currentSong].ToLower()))
            {
                lblGuess.Text = "You got it right!\nThe beatmap was " + titleArtist[currentSong].ToString();
                lblGuess.ForeColor = Color.Green;
                currentScore++;
                goesToNextSong();
            }
            else
            {
                lblGuess.Text = "You got it wrong!\nThe beatmap was " + titleArtist[currentSong].ToString();
                lblGuess.ForeColor = Color.Red;
                goesToNextSong();
            }
            
        }

        private async void goesToNextSong()
        {
            waveoutevent.Stop();
            pictureBox1.Visible = true;
            pictureBox1.Dock = DockStyle.Fill;
            lblScore.Visible = false;
            lblCounter.Visible = false;
            btnGuess.Visible = false;
            btnSkip.Visible = false;
            btnPlay.Visible = false;
            lblGuess.Visible = true;
            comboBox1.Visible = false;
            await (Task.Factory.StartNew(() => pictureBox1.Load(coverImgs[currentSong])));                        
            await (Task.Factory.StartNew(() => PlayMp3FromUrl("https:" + previewUrls[currentSong])));
           // Thread.Sleep(10000);
            comboBox1.Text = "";
            comboBox1.Size = new System.Drawing.Size(cbwidth, cbheight);            
            currentSong++;
            lblScore.Visible = true;
            lblCounter.Visible = true;
            btnGuess.Visible = true;
            btnSkip.Visible = true;
            btnPlay.Visible = true;
            pictureBox1.Visible = false;
            lblGuess.Visible = false;
            comboBox1.Visible = true;
            updateLbls();
            waveoutevent.Stop();
            startMp3Task();           
            
        }

        private void ShowsImgPlaysSong()
        {
            
            panel2.Visible = false;                   
            pictureBox1.Visible = true;
            startMp3Task();
            Thread.Sleep(10000);
        }
    }
    public static class ListExtension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}