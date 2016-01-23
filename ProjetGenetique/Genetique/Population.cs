using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetGenetique.Genetique
{
    class Population
    {
        private double _mutationRate; 
        private int _nbNote           = 16;
        private int _nbPerson         = 10;
        private double _survivalRate  = 2.5;
        private double _crossoverRate = 0.6;

        private Person[] _persons;

        public Population()
        {
            _persons      = new Person[_nbPerson];
            Random random = new Random();
            _mutationRate = 1 / _nbNote;

            for (int i = 0; i < _nbPerson; i++) {
                _persons[i] = new Person(_nbNote, random.Next(1,129));
                _persons[i].generateRandomNotes(random);
            }
        }

        //get access to the person list from the UI
        public Person[] persons { get; set; }

        private Person cross(Person parent1, Person parent2)
        {
            int nbSeq        = (int)(_nbNote * new Random().NextDouble());
            int[] aNote      = new int[_nbNote]; 
            Person newPerson = new Person(_nbNote);
            
            for (int i = 0; i < _nbNote; i++) {
                if (i < nbSeq) {
                    aNote[i] = parent1.notes[i];
                }
                else {
                    aNote[i] = parent2.notes[i];
                }
            }

            newPerson.notes = aNote;

            if (parent1.fitness >= parent2.fitness) {
                newPerson.instrument = parent1.instrument;
            }
            else {
                newPerson.instrument = parent2.instrument;
            }

            return newPerson;
        }

        private Person mutation(Person parent1)
        {
            Random random = new Random();

            for (int i = 0; i < _nbNote; i++) {
                if (random.NextDouble() <= _mutationRate) {
                    parent1.notes[i] = random.Next(0,127);
                }
            }

            return parent1;
        }

        private Person selection()
        {
            Person person;
            Random random = new Random();
            int parent1   = random.Next(0, 10);
            int parent2   = random.Next(0, 10);

            if(_persons[parent1].fitness <= _persons[parent2].fitness) {
                person = _persons[parent2];
            }
            else {
                person = _persons[parent1];
            }

            return person;
        }

        public void newGeneration()
        {
            Person person1 = selectBest();
            Person person2;

            int i                  = 0;
            Random random          = new Random(); 
            Person[] newPopulation = new Person[_nbPerson];

            if (person1 != null) {
                newPopulation[i] = person1;
                i++;
            }

            for(; i < _nbPerson; i++) {
                person1 = selection();
                
                if (_crossoverRate > random.NextDouble()) {
                    person2 = selection();
                    person1 = cross(person1, person2);
                }

                person1          = mutation(person1);
                newPopulation[i] = person1;
            }

            _persons = newPopulation;
        }

        //survive if sum of fitness >= survivalRate
        private Person selectBest()
        {
            int sumFitness = 0; 
            Person best  = new Person(_nbNote);
            
            for (int i = 0; i < _nbPerson; i++) {
                sumFitness += _persons[i].fitness;

                if (_persons[i].fitness > best.fitness) {
                    best = _persons[i];
                }
            }

            if (sumFitness / _nbPerson < _survivalRate)
            {
                best = null;
            }

            return best;
        }
    }
}
