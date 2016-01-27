using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Forms;
using ProjetGenetique.Genetic;
using ProjetGenetique.GenerationMIDI;

namespace ProjetGenetique
{
    public partial class ProjetGenetique : Form
    {
        private Population _population;
        private MediaPlayer _mplayer;
        private Boolean _isPlaying;

        public ProjetGenetique()
        {
            InitializeComponent();

            _population = new Population();

            createMidis();

            // Initialisation du lecteur
            _mplayer = new MediaPlayer();
            _mplayer.MediaEnded += mplayer_MediaEnded;
            _isPlaying = false;
        }

        private void createMidis()
        {
            stopMusic();

            if (!Directory.Exists("./midis")) {
                Directory.CreateDirectory("./midis");
                System.Threading.Thread.Sleep(1000);
            }

            for (int i = 0; i < _population.nbPerson; i++) {
                MIDISong song = new MIDISong();

                song.AddTrack("Midi " + i.ToString());
                song.SetTimeSignature(0, 4, 4);
                song.SetTempo(0, 150);
                song.SetChannelInstrument(0, 0, _population.persons[i].instrument);

                for (int j = 0; j < _population.nbNote; j++) {
                    song.AddNote(0, 0, _population.persons[i].notes[j], 12);
                }

                MemoryStream ms = new MemoryStream();
                song.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                byte[] src = ms.GetBuffer();
                byte[] dst = new byte[src.Length];

                for (int y = 0; y < src.Length; y++) {
                    dst[y] = src[y];
                }

                ms.Close();

                string file   = "./midis/Midi " + i.ToString() + ".mid";
                FileStream objWriter = File.Create(file);

                objWriter.Write(dst, 0, dst.Length);
                objWriter.Close();
                objWriter.Dispose();
                objWriter = null;
            }
        }

        private void deleteMidis()
        {
            stopMusic();

            var files = Directory.EnumerateFiles("./midis/", "Midi *.mid");

            foreach (string file in files) {
                File.Delete(file);
            }

            Directory.Delete("./midis");
            System.Threading.Thread.Sleep(1000);
        }

        //close gently the mediaPlayer when it ends playing
        void mplayer_MediaEnded(object sender, EventArgs e)
        {
            stopMusic();
        }

        private void playMusic(string file)
        {
            _mplayer.Open(new Uri(file, UriKind.Relative));
            _isPlaying = true;
            _mplayer.Play();
        }

        private void saveMusic(int id)
        {
            if (!Directory.Exists("./saves")) {
                Directory.CreateDirectory("./saves");
            }
            
            DateTime date       = DateTime.Now;
            string dateFormated = date.ToString().Replace("/", "");
            dateFormated        = dateFormated.Replace(" ", "");
            dateFormated        = dateFormated.Replace(":", "");

            File.Move("./midis/Midi " + id.ToString() + ".mid", "./saves/Midi" + dateFormated + ".mid");
        }

        private void stopMusic()
        {
            if (_isPlaying) {
                _mplayer.Stop();
                _mplayer.Close();
                _isPlaying = false;
            }
        }

        private void suite_Click(object sender, EventArgs e)
        {
            //get and set all the fitnesses

            if (note1.Text != "")
            {
                _population.persons[0].fitness = int.Parse(note1.Text);
            }

            if (note2.Text != "")
            {
                _population.persons[1].fitness = int.Parse(note2.Text);
            }

            if (note3.Text != "")
            {
                _population.persons[2].fitness = int.Parse(note3.Text);
            }

            if (note4.Text != "")
            {
                _population.persons[3].fitness = int.Parse(note4.Text);
            }

            if (note5.Text != "")
            {
                _population.persons[4].fitness = int.Parse(note5.Text);
            }
            if (note6.Text != "")
            {
                _population.persons[5].fitness = int.Parse(note6.Text);
            }
            if (note7.Text != "")
            {
                _population.persons[6].fitness = int.Parse(note7.Text);
            }
            if (note8.Text != "")
            {
                _population.persons[7].fitness = int.Parse(note8.Text);
            }
            if (note9.Text != "")
            {
                _population.persons[8].fitness = int.Parse(note9.Text);
            }
            if (note10.Text != "")
            {
                _population.persons[9].fitness = int.Parse(note10.Text);
            }

            //reinitialise the fitness of all the select inputs
            note1.Text = "";
            note2.Text = "";
            note3.Text = "";
            note4.Text = "";
            note5.Text = "";
            note6.Text = "";
            note7.Text = "";
            note8.Text = "";
            note9.Text = "";
            note10.Text = "";
            
            //get a new generation
            _population.newGeneration();
            deleteMidis();
            createMidis();
        }

        
        private void rec1_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(0);
        }

        
        private void play1_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play1.Text = "Lancer la mélodie";
                stopMusic();
            } else {
                //play number - 1
                play1.Text = "Stopper la mélodie";
                playMusic("midis/Midi 0.mid");
            }
        }

        private void rec2_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(1);
        }


        private void play2_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play2.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play2.Text = "Stopper la mélodie";
                playMusic("midis/Midi 1.mid");
            }
        }

        private void rec3_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(2);
        }


        private void play3_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play3.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play3.Text = "Stopper la mélodie";
                playMusic("midis/Midi 2.mid");
            }
        }

        private void rec4_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(3);
        }


        private void play4_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play4.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play4.Text = "Stopper la mélodie";
                playMusic("midis/Midi 3.mid");
            }
        }

        private void rec5_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(4);
        }


        private void play5_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play5.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play5.Text = "Stopper la mélodie";
                playMusic("midis/Midi 4.mid");
            }
        }

        private void rec6_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(5);
        }


        private void play6_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play6.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play6.Text = "Stopper la mélodie";
                playMusic("midis/Midi 5.mid");
            }
        }

        private void rec7_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(6);
        }


        private void play7_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play7.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play7.Text = "Stopper la mélodie";
                playMusic("midis/Midi 6.mid");
            }
        }

        private void rec8_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(7);
        }


        private void play8_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play8.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play8.Text = "Stopper la mélodie";
                playMusic("midis/Midi 7.mid");
            }
        }

        private void rec9_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(8);
        }


        private void play9_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play9.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play9.Text = "Stopper la mélodie";
                playMusic("midis/Midi 8.mid");
            }
        }

        private void rec10_Click(object sender, EventArgs e)
        {
            //rec number - 1
            saveMusic(9);
        }


        private void play10_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                play10.Text = "Lancer la mélodie";
                stopMusic();
            }
            else
            {
                //play number - 1
                play10.Text = "Stopper la mélodie";
                playMusic("midis/Midi 9.mid");
            }
        }

        private void ProjetGenetique_FormClosing(object sender, FormClosingEventArgs e)
        {
            deleteMidis();
        }
    }
}
