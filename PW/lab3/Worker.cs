
        public class Worker 
        {
            public string Name { get; set; }

            public int Wiek { get; set; }

            public override string ToString()
            {
                char position = 'N';

                switch (this)
                {
                    case Supervisor: position = 'S'; break;
                    case Manager: position = 'M'; break;
                    case Worker: position = 'W'; break;
                }
                return $"{position}: {Name} # {Wiek}";
            }
        }

        public class Manager : Worker
        {
        }

        public class Supervisor : Manager
        {
        }

/*
  new Worker { ID=1, Name="Jan Kowalski", Wiek=50}, 
  new Supervisor { ID = 2, Name="Marian Nowak", Wiek = 33}, 
  new Supervisor { ID = 3, Name= "Alicja Kowalska", Wiek=48 }, 
  new Manager { ID = 4, Name="Grażyna Wiśniewska", Wiek=50 }, 
  new Manager { ID = 5, Name= "Janusz Wójcik", Wiek=37 }, 
  new Worker { ID = 6, Name="Mariusz Mariusz", Wiek=22 }
*/