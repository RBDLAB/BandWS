namespace BandWithoutSoloist
{
    partial class BWS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BWS));
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lyricsPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IBTFolderTxt = new System.Windows.Forms.TextBox();
            this.ibtFolderBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.gender = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maxRateText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minRateText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rateText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.liveBPMtext = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.startSpeakBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pauseSpeakBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(140, 187);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 39);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Listening";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton2);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lyrics:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(85, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lyricsPath
            // 
            this.lyricsPath.Location = new System.Drawing.Point(164, 30);
            this.lyricsPath.Name = "lyricsPath";
            this.lyricsPath.Size = new System.Drawing.Size(192, 20);
            this.lyricsPath.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IBTFolderTxt);
            this.groupBox1.Controls.Add(this.ibtFolderBtn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.startButton);
            this.groupBox1.Controls.Add(this.gender);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maxRateText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.minRateText);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lyricsPath);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 240);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // IBTFolderTxt
            // 
            this.IBTFolderTxt.Location = new System.Drawing.Point(163, 65);
            this.IBTFolderTxt.Name = "IBTFolderTxt";
            this.IBTFolderTxt.Size = new System.Drawing.Size(192, 20);
            this.IBTFolderTxt.TabIndex = 11;
            // 
            // ibtFolderBtn
            // 
            this.ibtFolderBtn.Location = new System.Drawing.Point(85, 62);
            this.ibtFolderBtn.Name = "ibtFolderBtn";
            this.ibtFolderBtn.Size = new System.Drawing.Size(73, 25);
            this.ibtFolderBtn.TabIndex = 10;
            this.ibtFolderBtn.Text = "Browse";
            this.ibtFolderBtn.UseVisualStyleBackColor = true;
            this.ibtFolderBtn.Click += new System.EventHandler(this.ibtFolderClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "IBT Diractory:";
            // 
            // gender
            // 
            this.gender.FormattingEnabled = true;
            this.gender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.gender.Location = new System.Drawing.Point(85, 153);
            this.gender.Name = "gender";
            this.gender.Size = new System.Drawing.Size(83, 21);
            this.gender.TabIndex = 6;
            this.gender.Text = "Male";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Gender:";
            // 
            // maxRateText
            // 
            this.maxRateText.Location = new System.Drawing.Point(85, 125);
            this.maxRateText.Name = "maxRateText";
            this.maxRateText.Size = new System.Drawing.Size(39, 20);
            this.maxRateText.TabIndex = 7;
            this.maxRateText.Text = "4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Max Rate:";
            // 
            // minRateText
            // 
            this.minRateText.Location = new System.Drawing.Point(85, 99);
            this.minRateText.Name = "minRateText";
            this.minRateText.Size = new System.Drawing.Size(39, 20);
            this.minRateText.TabIndex = 5;
            this.minRateText.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Min Rate:";
            // 
            // rateText
            // 
            this.rateText.Location = new System.Drawing.Point(82, 56);
            this.rateText.Name = "rateText";
            this.rateText.Size = new System.Drawing.Size(59, 20);
            this.rateText.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Current Rate:";
            // 
            // liveBPMtext
            // 
            this.liveBPMtext.Location = new System.Drawing.Point(82, 22);
            this.liveBPMtext.Name = "liveBPMtext";
            this.liveBPMtext.Size = new System.Drawing.Size(59, 20);
            this.liveBPMtext.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Live BPM:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // startSpeakBtn
            // 
            this.startSpeakBtn.Location = new System.Drawing.Point(410, 199);
            this.startSpeakBtn.Name = "startSpeakBtn";
            this.startSpeakBtn.Size = new System.Drawing.Size(100, 39);
            this.startSpeakBtn.TabIndex = 5;
            this.startSpeakBtn.Text = "Start Speak";
            this.startSpeakBtn.UseVisualStyleBackColor = true;
            this.startSpeakBtn.Click += new System.EventHandler(this.startSpeakBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.rateText);
            this.groupBox2.Controls.Add(this.liveBPMtext);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(421, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 87);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Live Output";
            // 
            // pauseSpeakBtn
            // 
            this.pauseSpeakBtn.Location = new System.Drawing.Point(524, 199);
            this.pauseSpeakBtn.Name = "pauseSpeakBtn";
            this.pauseSpeakBtn.Size = new System.Drawing.Size(100, 39);
            this.pauseSpeakBtn.TabIndex = 10;
            this.pauseSpeakBtn.Text = "Pause Speak";
            this.pauseSpeakBtn.UseVisualStyleBackColor = true;
            this.pauseSpeakBtn.Click += new System.EventHandler(this.pauseSpeakBtn_Click);
            // 
            // BWS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 271);
            this.Controls.Add(this.pauseSpeakBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.startSpeakBtn);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "BWS";
            this.Text = "Band Without Soloist";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox lyricsPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxRateText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox minRateText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox gender;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox liveBPMtext;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox rateText;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button startSpeakBtn;
        private System.Windows.Forms.TextBox IBTFolderTxt;
        private System.Windows.Forms.Button ibtFolderBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button pauseSpeakBtn;
    }
}

