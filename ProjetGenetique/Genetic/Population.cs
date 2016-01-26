using System;

namespace ProjetGenetique.Genetic
{
    class Population
    {
        private Person[] _persons;

        private int _nbNote           = 16;
        private int _nbPerson         = 10;
        private double _survivalRate  = 2.5;
        private double _crossoverRate = 0.6;
        private double _mutationRate;

        //get access to the person list from the UI
        public Person[] persons
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
            }
        }

        public int nbNote
        {
            get
            {
                return _nbNote;
            }
            set
            {
                _nbNote = value;
            }
        }

        public int nbPerson
        {
            get
            {
                return _nbPerson;
            }
            set
            {
                _nbPerson = value;
            }
        }

        public Population()
        {
            Random random = new Random();
            _mutationRate = 1 / _nbNote;
            _persons      = new Person[_nbPerson];

            for (int i = 0; i < _nbPerson; i++) {
                _persons[i] = new Person(_nbNote, random.Next(1,129));
                _persons[i].generateRandomNotes();
            }
        }

        private Person cross(Person parent1, Person parent2)
        {
            int nbSeq        = (int)(_nbNote * new Random().NextDouble());
            int[] aNote      = new int[_nbNote]; 
            Person newPerson = new Person(_nbNote);
            
            for (int i = 0; i < _nbNote; i++) {
                if (i > nbSeq) {
                    aNote[i] = parent2.notes[i];
                }
                else {
                    aNote[i] = parent1.notes[i];
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

        private Person mutation(Person person1)
        {
            Random random = new Random();

            for (int i = 0; i < _nbNote; i++) {
                if (random.NextDouble() <= _mutationRate) {
                    person1.notes[i] = random.Next(0,127);
                }
            }

            return person1;
        }

        public void newGeneration()
        {
            int i                  = 0;
            Random random          = new Random();
            Person bestPerson      = selectBest();
            Person[] newPopulation = new Person[_nbPerson];

            if (bestPerson != null) {
                Person person     = new Person(_nbNote);
                person.notes      = bestPerson.notes;
                person.instrument = bestPerson.instrument;
                
                newPopulation[i] = person;
                i++;
            }

            for (; i < _nbPerson; i++) {
                Person person = selection();

                if (_crossoverRate > random.NextDouble()) {
                    Person crossPerson = selection();
                    person             = cross(person, crossPerson);
                }

                person           = mutation(person);
                newPopulation[i] = person;
            }

            _persons = newPopulation;
        }

        //survive if sum of fitness >= survivalRate
        private Person selectBest()
        {
            int sumFitness = 0;
            Person best    = new Person(_nbNote);

            for (int i = 0; i < _nbPerson; i++) {
                sumFitness += _persons[i].fitness;

                if (_persons[i].fitness > best.fitness) {
                    best         = _persons[i];
                    best.fitness = 0;
                }
            }

            if ((sumFitness / _nbPerson) < _survivalRate) {
                best = null;
            }

            return best;
        }

        private Person selection()
        {
            Person person;
            Random random = new Random();
            int parent1   = random.Next(0, 10);
            int parent2   = random.Next(0, 10);

            if(_persons[parent1].fitness <= _persons[parent2].fitness) {
                person         = _persons[parent2];
                person.fitness = 0;
            }
            else {
                person         = _persons[parent1];
                person.fitness = 0;
            }

            return person;
        }
    }
}
