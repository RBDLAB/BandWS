using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using System.IO;
using System.Diagnostics;

namespace BandWithoutSoloist
{
    public partial class BWS : System.Windows.Forms.Form
    {
        //Initialize gloabal members
        double maxRate = 4;
        double minRate = 0;
        int prevRate = 0;
        static string text = "";
        Boolean isFirstBeat = true;
        double prevBeat = -1;
        int interval = 0;
        Boolean speakIsOn = false;
        int counterBeat = 0;
        string ibtPath = "";
        Process process;
        Boolean startSpeakBtnIsClicked = false;

        //this file hold the paths for next time
        string curFile = @"saveData.txt";

        //define the required methods from DLL file
        //necessary  to speech
        [DllImport("SapiVoice.dll")]
        public static extern void initSpeech();
        [DllImport("SapiVoice.dll")]
        public static extern void changeRate(int rate);
        [DllImport("SapiVoice.dll")]
        public static extern void setText(string text);
        [DllImport("SapiVoice.dll")]
        public static extern void startSpeak();
        [DllImport("SapiVoice.dll")]
        public static extern void setNowPause(int value);
        [DllImport("SapiVoice.dll")]
        public static extern int getNowPause();
        [DllImport("SapiVoice.dll")]
        public static extern void setGender(string value);
        [DllImport("SapiVoice.dll")]
        public static extern void pauseSpeak();

        //constructor
        public BWS()
        {
            InitializeComponent();
            pauseSpeakBtn.Enabled = false;
            
            //checks if the saved data file is exist
            if (File.Exists(curFile))
            {
                string line1 = File.ReadLines(curFile).Skip(0).Take(1).First();
                string line2 = File.ReadLines(curFile).Skip(1).Take(1).First();

                if(line1 != "")
                {
                    lyricsPath.Text = line1;
                    readLyrics(line1);
                }

                if (line2 != "")
                {
                    IBTFolderTxt.Text = line2;
                    ibtPath = line2;
                }

            }
        }

        //This method starts to listen to microphone stream
        public void startButton2(object sender, EventArgs e)
        {
            var watch = new System.IO.FileSystemWatcher();
            watch.Path = ibtPath;
            watch.Filter = "mic.txt";
            watch.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite; //more options
            watch.Changed += new FileSystemEventHandler(OnChanged);
            watch.EnableRaisingEvents = true;

            //determine the singer gender
            if (gender.Text == "Male")
            {
                setGender("Gender=Male;");
            }
            else
            {
                setGender("Gender=Female;");
            }

            minRate = Convert.ToDouble(minRateText.Text);
            maxRate = Convert.ToDouble(maxRateText.Text);

            //min BPM is 81
            //max BPM is 160
            interval = Convert.ToInt32((160 - 81) / (maxRate - minRate));

            //start the IBT process
            using (process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                process.StartInfo.WorkingDirectory = @"C:\";
                process.StartInfo.FileName = Path.Combine(Environment.SystemDirectory, "cmd.exe");

                // Redirects the standard input so that commands can be sent to the shell.
                process.StartInfo.RedirectStandardInput = true;
                // Runs the specified command and exits the shell immediately.
                //process.StartInfo.Arguments = @"/c ""dir""";

                process.OutputDataReceived += ProcessOutputDataHandler;
                process.ErrorDataReceived += ProcessErrorDataHandler;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Send a directory command and an exit command to the shell
                process.StandardInput.WriteLine("cd " + ibtPath);
                process.StandardInput.WriteLine("ibt -mic");
            }

            //change the window title
            Console.WriteLine("Band Without Soloist - Running...");
            BWS.setText("Band Without Soloist - Running...");

            startButton.Enabled = false;
        }

        //calc the linear mapping fom bpm scale to rate scale
        public double getRateVal(double bpm)
        {
            double a = (maxRate - minRate) / (160 - 81);
            double b = (minRate - (a * 81));

            return (a * bpm + b);

        }

        delegate void SetTextCallback(string text);

        public void SetBPMtext(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.liveBPMtext.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetBPMtext);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.liveBPMtext.Text = text;
            }
        }

        public void SetRateText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.rateText.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetRateText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.rateText.Text = text;
            }
        }

        //This method will monitor the mic.txt when its changed in real-time.
        //Calc the appropriate rate based on the BPM input.
        //call to change rate function (DLL external file) in order to chnage the speak rate.
        public void OnChanged(object source, FileSystemEventArgs e)
        {
            //Get the updated last line from mic output.
            string lastLine = File.ReadLines(e.FullPath).Last();
            string beatString = lastLine.Substring(lastLine.IndexOf(' ') + 1);
            double beat = Convert.ToDouble(beatString);
            SetBPMtext(beatString);
            Console.WriteLine(beat);

            //Checks if the current beat is the "One" and resume the speak if needed.
            if(counterBeat % 4 == 0 && getNowPause() == 1)
            {
                Console.WriteLine("Start Sentence");
                startSpeak();
                setNowPause(0);
            }

            //Avoid from rate exceeded
            Int32 currentCalcRate = Convert.ToInt32(getRateVal(beat));

            if (currentCalcRate > maxRate)
            {
                currentCalcRate = Convert.ToInt32(maxRate);
            }
            else if (currentCalcRate < minRate)
            {
                currentCalcRate = Convert.ToInt32(minRate);
            }

            //Checks if the first beat flag is ON.
            if (isFirstBeat)
            {
                //Set the first rate.
                isFirstBeat = false;
                prevRate = currentCalcRate;
                if (speakIsOn && text != "")
                    changeRate(currentCalcRate);

                SetRateText(currentCalcRate.ToString());
            }
            else
            {
                //Checks if change rate is needed.
                if (prevRate != currentCalcRate && Math.Abs(prevBeat - beat) >= 7)
                {
                    //Change the rate
                    if (speakIsOn && text != "")
                        changeRate(currentCalcRate);

                    //Display the current rate to the user
                    SetRateText(currentCalcRate.ToString());
                    prevRate = currentCalcRate;
                }
            }
           
            prevBeat = beat;
            counterBeat++;
        }

        //This funtion will load the lyrics txt file into the app.
        public void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                        lyricsPath.Text = string.Format(openFileDialog1.FileName);

                         readLyrics(openFileDialog1.FileName);
                }

                if (File.Exists(curFile))
                {
                    if (openFileDialog1.FileName != "")
                    {
                        string[] lines = System.IO.File.ReadAllLines(curFile);
                        lines[0] = openFileDialog1.FileName;
                        System.IO.File.WriteAllLines(curFile, lines);
                    }

                }
                else
                {
                    if (openFileDialog1.FileName != "")
                    {
                        string[] line = { openFileDialog1.FileName, ""};
                        System.IO.File.WriteAllLines(curFile, line);
                    }
                }

            }

        }

        //read all text from lyrics file
        void readLyrics(string txt)
        {
            string tmpTxt = File.ReadAllText(txt);
            text = tmpTxt.Replace(System.Environment.NewLine, " ");
        }

        //Start speak
        public void startSpeakBtn_Click(object sender, EventArgs e)
        {
        
            if (!startSpeakBtnIsClicked)
            {
                if (text != "")
                {
                    setText(text);
                    initSpeech();
                }
                speakIsOn = true;
                startSpeakBtn.Enabled = false;
                pauseSpeakBtn.Enabled = true;
                startSpeakBtnIsClicked = true;
            }
            else
            {
                startSpeak();
                startSpeakBtn.Enabled = false;
                pauseSpeakBtn.Enabled = true;
            }
            
        }

        //get the ibt folder
        private void ibtFolderClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    IBTFolderTxt.Text = fbd.SelectedPath;
                    ibtPath = fbd.SelectedPath;
                }

                if (File.Exists(curFile))
                {
                    if (fbd.SelectedPath != "")
                    {
                        string[] lines = System.IO.File.ReadAllLines(curFile);
                        lines[1] = fbd.SelectedPath;
                        System.IO.File.WriteAllLines(curFile, lines);
                    }

                }
                else
                {
                    if (fbd.SelectedPath != "")
                    {
                        string[] line = { "", fbd.SelectedPath };
                        System.IO.File.WriteAllLines(curFile, line);
                    }
                }
            }
        }


        //echo ibt
        public static void ProcessOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            Console.WriteLine(outLine.Data);
        }

        public static void ProcessErrorDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            Console.WriteLine(outLine.Data);
        }

        //pause the speak
        private void pauseSpeakBtn_Click(object sender, EventArgs e)
        {
            pauseSpeak();
            startSpeakBtn.Enabled = true;
            pauseSpeakBtn.Enabled = false;
        }
    }
}