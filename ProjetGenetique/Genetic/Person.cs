using System;

namespace ProjetGenetique.Genetic
{
    class Person
    {
        private int    _instrument; 
        private int    _fitness;
        private int[]  _notes;
        
        public Person(int nbNote, int randomInstrument = 0)
        {
            _instrument = randomInstrument;
            _fitness    = 0;
            _notes      = new int[nbNote];
        }

        public int instrument
        {
            get
            {
                return _instrument;
            }
            set
            {
                _instrument = value;
            }
        }

        public int[] notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
            }
        }
        
        public int fitness
        {
            get
            {
                return _fitness;
            }
            set
            {
                _fitness = value;
            }
        }

        //generate a random song
        public void generateRandomNotes(Random random)
        {
            for (int i = 0; i < _notes.Length; i++){
                _notes[i] = random.Next(0, 128);
            }
        }
    }
}
