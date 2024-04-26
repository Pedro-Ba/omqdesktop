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
        List<Song> topSongList = new List<Song>();
        List<string> topSongRandomTitleArtist = new List<string>();
        List<Song> mostPlayedSongList = new List<Song>();
        List<string> mostPlayedRandomTitleArtist = new List<string>();
        List<Song> gameSongList = new List<Song>();
        List<string> gameRandomTitleArtistList = new List<string>();
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
            waveoutevent.Volume = 0.15F;
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
            label2.Text = "This will get the top 100 maps in pp and in plays from the user ID's default gamemode.\nThis can potentially take some time.";

        }

        private async void btnGetTops_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 200;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            string user_id = textBox1.Text + "/";
            string type = "best/";
            string ArtistTitle;
            string urlPreview;
            string urlImage;

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + api_key);

            string urlTopSongs = "https://osu.ppy.sh/api/v2/users/" + user_id + "scores/" + type + "?limit=100";

            using (HttpResponseMessage res = await client.GetAsync(urlTopSongs))
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
                                ArtistTitle = i["beatmapset"]["artist"].ToString() + " - " + i["beatmapset"]["title"].ToString();
                                topSongRandomTitleArtist.Add(i["beatmapset"]["artist"].ToString() + " - " + i["beatmapset"]["title"].ToString());
                                urlPreview = i["beatmapset"]["preview_url"].ToString();
                                urlImage = i["beatmapset"]["covers"]["list@2x"].ToString();
                                topSongList.Add(new Song(ArtistTitle, urlPreview, urlImage));
                                progressBar1.Value++;
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine("NO Data----------");
                    }
                }
            }

            string urlMostPlayed = "https://osu.ppy.sh/api/v2/users/" + user_id + "beatmapsets/most_played/?limit=100";
            using (HttpResponseMessage res = await client.GetAsync(urlMostPlayed))
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
                                ArtistTitle = i["beatmapset"]["artist"].ToString() + " - " + i["beatmapset"]["title"].ToString();
                                mostPlayedRandomTitleArtist.Add(i["beatmapset"]["artist"].ToString() + " - " + i["beatmapset"]["title"].ToString());
                                urlPreview = i["beatmapset"]["preview_url"].ToString();
                                urlImage = i["beatmapset"]["covers"]["list@2x"].ToString();
                                mostPlayedSongList.Add(new Song(ArtistTitle, urlPreview, urlImage));
                                progressBar1.Value++;
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine("NO Data----------");
                    }
                }
            }
            //shuffle
            topSongRandomTitleArtist.Shuffle();
            topSongList.Shuffle();
            mostPlayedRandomTitleArtist.Shuffle();
            mostPlayedSongList.Shuffle();

            panel1.Visible = false;
            panelMainMenu.Visible = true;
            panelMainMenu.Top = 0;
            panelMainMenu.Left = 0;
            panelMainMenu.Dock = DockStyle.Fill;
        }

        public void PlayMp3FromUrl(string url)
        {
            using (Stream ms = new MemoryStream())
            {
                try
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
                }catch(Exception e)
                {
                    MessageBox.Show("mp3 probably got nuked lmao");
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
            Task.Factory.StartNew(() => PlayMp3FromUrl("https:" + gameSongList[currentSong].previewUrl));
        }

        private async void loadImg()
        {
            pictureBox1.Load(gameSongList[currentSong].coverImg);
        }

        private void btnPlay_Click_1(object sender, EventArgs e)
        {
            startMp3Task();
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.Text.Length != 0 && !e.KeyCode.Equals(Keys.Up) && !e.KeyCode.Equals(Keys.Down) && comboBox1.Text != " " && comboBox1.Text != "-" && comboBox1.Text != " -")
            {
                int selectionStart = comboBox1.SelectionStart;
                comboBox1.Items.Clear();
                for (var i = 0; i < gameRandomTitleArtistList.Count; i++)
                {
                    var artist = gameRandomTitleArtistList[i];
                    if (artist.ToLower().Contains(comboBox1.Text.ToLower()))
                    {
                        comboBox1.Items.Add(artist);
                    }
                }
                comboBox1.SelectionStart = selectionStart;
                comboBox1.SelectionLength = 0;
                comboBox1.SelectedIndex = -1;
                comboBox1.Size = new System.Drawing.Size(cbwidth, cbheight * 10);
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
            lblCounter.Text = "Current Song: " + (currentSong + 1).ToString() + "/" + (gameSongList.Count).ToString();
            lblScore.Text = "Score: " + (currentScore).ToString() + "/" + currentSong.ToString();
        }
        private void btnSkip_Click(object sender, EventArgs e)
        {
            lblGuess.Text = "You skipped!\nThe beatmap was " + gameSongList[currentSong].titleArtist;
            lblGuess.ForeColor = Color.Red;
            goesToNextSong();
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            string guess = comboBox1.Text;
            if (guess.ToLower().Equals(gameSongList[currentSong].titleArtist.ToLower()))
            {
                lblGuess.Text = "You got it right!\nThe beatmap was " + gameSongList[currentSong].titleArtist;
                lblGuess.ForeColor = Color.Green;
                currentScore++;
                goesToNextSong();
            }
            else
            {
                lblGuess.Text = "You got it wrong!\nThe beatmap was " + gameSongList[currentSong].titleArtist;
                lblGuess.ForeColor = Color.Red;
                goesToNextSong();
            }

        }

        private async void goesToNextSong()
        {
            waveoutevent.Stop();
            try
            {
                await (Task.Factory.StartNew(() => pictureBox1.Load(gameSongList[currentSong].coverImg)));
            }
            catch (Exception e)
            {
                MessageBox.Show("You somehow have a map so old, it doesn't even have a background image. Congrats.");
            }
            pictureBox1.Visible = true;
            pictureBox1.Dock = DockStyle.Fill;
            lblScore.Visible = false;
            lblCounter.Visible = false;
            btnGuess.Visible = false;
            btnSkip.Visible = false;
            btnPlay.Visible = false;
            lblGuess.Visible = true;
            comboBox1.Visible = false;
            try
            {
                await (Task.Factory.StartNew(() => PlayMp3FromUrl("https:" + gameSongList[currentSong].previewUrl)));
            }
            catch (Exception e)
            {
                MessageBox.Show("mp3 probably got nuked lmao");
            }
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

        private void btnTopPlay_Click(object sender, EventArgs e)
        {
            //Since it's top play, then:
            gameSongList = topSongList.GetRange(0, (int)(numericUpDown1.Value));
            gameRandomTitleArtistList = topSongRandomTitleArtist;
            transitionToGameState();
        }

        private void btnMostPlayed_Click(object sender, EventArgs e)
        {            
            //since it's most played, then:
            gameSongList = mostPlayedSongList.GetRange(0, (int)(numericUpDown1.Value));
            gameRandomTitleArtistList = mostPlayedRandomTitleArtist;
            transitionToGameState();
        }

        public void transitionToGameState()
        {
            panelMainMenu.Visible = false;
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
    public class Song
    {
        public string titleArtist;
        public string previewUrl;
        public string coverImg;

        public Song(string title, string url, string cover)
        {
            this.titleArtist = title;
            this.previewUrl = url;
            this.coverImg = cover;
        }
    }
}