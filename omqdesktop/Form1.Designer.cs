namespace omqdesktop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnStart = new Button();
            panel1 = new Panel();
            label2 = new Label();
            progressBar1 = new ProgressBar();
            btnGetTops = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            panel2 = new Panel();
            lblGuess = new Label();
            pictureBox1 = new PictureBox();
            lblScore = new Label();
            lblCounter = new Label();
            btnSkip = new Button();
            btnGuess = new Button();
            comboBox1 = new ComboBox();
            btnPlay = new Button();
            bindingSource1 = new BindingSource(components);
            panelMainMenu = new Panel();
            numericUpDown1 = new NumericUpDown();
            label3 = new Label();
            btnMostPlayed = new Button();
            btnTopPlay = new Button();
            label4 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            panelMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(353, 134);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start!";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(progressBar1);
            panel1.Controls.Add(btnGetTops);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(803, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(785, 426);
            panel1.TabIndex = 3;
            panel1.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 80);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 6;
            label2.Text = "label2";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(325, 35);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(125, 29);
            progressBar1.TabIndex = 5;
            // 
            // btnGetTops
            // 
            btnGetTops.Location = new Point(215, 35);
            btnGetTops.Name = "btnGetTops";
            btnGetTops.Size = new Size(94, 29);
            btnGetTops.TabIndex = 4;
            btnGetTops.Text = "Get Tops";
            btnGetTops.UseVisualStyleBackColor = true;
            btnGetTops.Click += btnGetTops_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 38);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 1;
            label1.Text = "User ID:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(84, 35);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.BackgroundImage = Properties.Resources.bg;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(lblGuess);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(lblScore);
            panel2.Controls.Add(lblCounter);
            panel2.Controls.Add(btnSkip);
            panel2.Controls.Add(btnGuess);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(btnPlay);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(826, 882);
            panel2.TabIndex = 4;
            panel2.Visible = false;
            // 
            // lblGuess
            // 
            lblGuess.AutoSize = true;
            lblGuess.BackColor = SystemColors.ActiveCaptionText;
            lblGuess.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblGuess.Location = new Point(160, 44);
            lblGuess.Name = "lblGuess";
            lblGuess.Size = new Size(0, 20);
            lblGuess.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(128, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(503, 98);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            pictureBox1.WaitOnLoad = true;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.BackColor = Color.Transparent;
            lblScore.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblScore.Location = new Point(351, 241);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(80, 20);
            lblScore.TabIndex = 5;
            lblScore.Text = "Score: 0/0";
            // 
            // lblCounter
            // 
            lblCounter.AutoSize = true;
            lblCounter.BackColor = Color.Transparent;
            lblCounter.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCounter.ForeColor = SystemColors.ControlText;
            lblCounter.Location = new Point(318, 205);
            lblCounter.Name = "lblCounter";
            lblCounter.Size = new Size(152, 20);
            lblCounter.TabIndex = 4;
            lblCounter.Text = "Current Song: 1/100";
            // 
            // btnSkip
            // 
            btnSkip.Location = new Point(341, 318);
            btnSkip.Name = "btnSkip";
            btnSkip.Size = new Size(94, 29);
            btnSkip.TabIndex = 3;
            btnSkip.Text = "Skip";
            btnSkip.UseVisualStyleBackColor = true;
            btnSkip.Click += btnSkip_Click;
            // 
            // btnGuess
            // 
            btnGuess.Location = new Point(578, 362);
            btnGuess.Name = "btnGuess";
            btnGuess.Size = new Size(94, 29);
            btnGuess.TabIndex = 2;
            btnGuess.Text = "Guess";
            btnGuess.UseVisualStyleBackColor = true;
            btnGuess.Click += btnGuess_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.Simple;
            comboBox1.Location = new Point(189, 363);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(383, 28);
            comboBox1.TabIndex = 1;
            comboBox1.KeyUp += comboBox1_KeyUp;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(341, 274);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(94, 29);
            btnPlay.TabIndex = 0;
            btnPlay.Text = "Play Song";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click_1;
            // 
            // panelMainMenu
            // 
            panelMainMenu.AutoSize = true;
            panelMainMenu.BackgroundImage = Properties.Resources.bg;
            panelMainMenu.BackgroundImageLayout = ImageLayout.Stretch;
            panelMainMenu.Controls.Add(label4);
            panelMainMenu.Controls.Add(numericUpDown1);
            panelMainMenu.Controls.Add(label3);
            panelMainMenu.Controls.Add(btnMostPlayed);
            panelMainMenu.Controls.Add(btnTopPlay);
            panelMainMenu.Location = new Point(2, 207);
            panelMainMenu.Name = "panelMainMenu";
            panelMainMenu.Size = new Size(788, 457);
            panelMainMenu.TabIndex = 5;
            panelMainMenu.Visible = false;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(328, 321);
            numericUpDown1.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 9;
            numericUpDown1.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ActiveCaptionText;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(160, 44);
            label3.Name = "label3";
            label3.Size = new Size(0, 20);
            label3.TabIndex = 8;
            // 
            // btnMostPlayed
            // 
            btnMostPlayed.Location = new Point(427, 158);
            btnMostPlayed.Name = "btnMostPlayed";
            btnMostPlayed.Size = new Size(119, 29);
            btnMostPlayed.TabIndex = 3;
            btnMostPlayed.Text = "Most Played";
            btnMostPlayed.UseVisualStyleBackColor = true;
            btnMostPlayed.Click += btnMostPlayed_Click;
            // 
            // btnTopPlay
            // 
            btnTopPlay.Location = new Point(244, 158);
            btnTopPlay.Name = "btnTopPlay";
            btnTopPlay.Size = new Size(120, 29);
            btnTopPlay.TabIndex = 0;
            btnTopPlay.Text = "Top Plays";
            btnTopPlay.UseVisualStyleBackColor = true;
            btnTopPlay.Click += btnTopPlay_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(251, 283);
            label4.Name = "label4";
            label4.Size = new Size(295, 20);
            label4.TabIndex = 10;
            label4.Text = "Please choose how many songs to guess:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 753);
            Controls.Add(panelMainMenu);
            Controls.Add(panel2);
            Controls.Add(btnStart);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "omqzin";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            panelMainMenu.ResumeLayout(false);
            panelMainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnStart;
        private Panel panel1;
        private Button btnGetTops;
        private Label label1;
        private TextBox textBox1;
        private ProgressBar progressBar1;
        private BindingSource bindingSource1;
        private Panel panel2;
        private Button btnPlay;
        private Label label2;
        private ComboBox comboBox1;
        private Button btnSkip;
        private Button btnGuess;
        private Label lblScore;
        private Label lblCounter;
        private PictureBox pictureBox1;
        private Label lblGuess;
        private Panel panelMainMenu;
        private Label label3;
        private Button btnMostPlayed;
        private Button btnTopPlay;
        private NumericUpDown numericUpDown1;
        private Label label4;
    }
}