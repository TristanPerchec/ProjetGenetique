using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGenetique.Genetique
{
    class Person
    {
        private int    _instrument; 
        private int    _fitness;
        private int[]  _notes;
        
        public Person(int nbNotes, int randomInstrument = 0)
        {
            _instrument = randomInstrument;
            _fitness = 0;
            _notes = new int[nbNotes];
        }

        public int instrument { get; set; }

        public int[] notes { get; set; }

        //user rating
        public int fitness { get; set; }

        //generate a random song
        public void generateRandomNotes(Random random)
        {
            for(int i = 0; i < _notes.Length; i++)
            {
                _notes[i] = random.Next(0, 128);
            }
        }
    }
}
