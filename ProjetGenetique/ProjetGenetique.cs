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
        private MediaPlayer _player;
        private Boolean _isPlaying;

        public ProjetGenetique()
        {
            InitializeComponent();

            _population = new Population();

            createMidis();

            // Initialisation du lecteur
            _player = new MediaPlayer();
            _player.MediaEnded += player_MediaEnded;
            _isPlaying = false;
        }

        private void createMidis()
        {
            stopMusic();

            if (!Directory.Exists("./midis")) {
                Directory.CreateDirectory("./midis");
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

                string strFileName   = "./midis/File" + i.ToString() + ".mid";
                FileStream objWriter = File.Create(strFileName);

                objWriter.Write(dst, 0, dst.Length);
                objWriter.Close();
                objWriter.Dispose();
                objWriter = null;
            }
        }

        private void deleteMidis()
        {
            stopMusic();

            var files = Directory.EnumerateFiles("./midis/", "File*.mid");

            foreach (string file in files) {
                File.Delete(file);
            }

            Directory.Delete("./midis");
        }

        //close gently the mediaPlayer when it ends playing
        void player_MediaEnded(object sender, EventArgs e)
        {
            stopMusic();
        }

        private void playMusic(string strFileName)
        {
            _player.Open(new Uri(strFileName, UriKind.Relative));
            _isPlaying = true;
            _player.Play();
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

            File.Move("./midis/File" + id.ToString() + ".mid", "./saves/File" + dateFormated + ".mid");
        }

        private void stopMusic()
        {
            if (_isPlaying) {
                _player.Stop();
                _player.Close();
                _isPlaying = false;
            }
        }

        private void suite_Click(object sender, EventArgs e)
        {
            //@todo get all the fitnesses

            //@todo reinitialise the fitness of all the select inputs

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
            if (_isPlaying) {
                stopMusic();
            } else {
                //play number - 1
                playMusic("File0.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File1.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File2.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File3.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File4.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File5.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File6.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File7.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File8.mid");
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
                stopMusic();
            }
            else
            {
                //play number - 1
                playMusic("File9.mid");
            }
        }

        private void ProjetGenetique_FormClosing(object sender, FormClosingEventArgs e)
        {
            deleteMidis();
        }
    }
}
