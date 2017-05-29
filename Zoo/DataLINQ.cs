using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    public class DataLINQ
    {
        private static DataLINQ instance;

        private DataLINQ()
        { }

        public static DataLINQ getInstance()
        {
            if (instance == null)
                instance = new DataLINQ();
            return instance;
        }

        public IEnumerable<Animal> AllByKind( IEnumerable<Animal> animals) 
        {
            return animals.OrderByDescending(a => a.Kind);
        }

        public IEnumerable<Animal> AllInState(IEnumerable<Animal> animals, string strState)
        {
            IEnumerable<Animal> result = null;
            try
            {
                var state = (State)Enum.Parse(typeof(State), strState, true);
                result = animals.Where(a => (a.State == state));
            }
            catch
            {
                
            }
            return result;
        }

        public IEnumerable<Animal> IllTigers(IEnumerable<Animal> animals)
        {
            return  animals.Where(a => (a.State == State.Ill && a.Kind == Kind.Tiger));
        }
    
        public IEnumerable<Animal> NickElephant(IEnumerable<Animal> animals, string nick)
        {
            return animals.Where(a => ((String.Compare(a.NickName, nick, true) == 0) && a.Kind == Kind.Elephant));
        }

        public IEnumerable<Object> HungryNicks(IEnumerable<Animal> animals)
        {
            return animals.Where(a => (a.State == State.Hungry)).Select(a => a.NickName);
        }

        public IEnumerable<Object> KindHealthiest(IEnumerable<Animal> animals)
        {
            var result = animals.GroupBy(a => a.Kind);
            List<Object> result2 = new List<Object>();

            foreach (var b in result)
            {
                var result1 = animals.Where(x => (b.Key == x.Kind)).OrderByDescending(x => x.Health).First();
                result2.Add(result1);
            }
            return result2;
        }


        public void KindDead(IEnumerable<Animal> animals, string s1)
        {
            var result = animals.GroupBy(a => a.Kind);
            Console.WriteLine("\n********* {0} ********", s1);
            foreach (var b in result)
            {
                var result1 = b.Count(x => (x.State == State.Dead));
                Console.WriteLine("Kind: {0}, count: {1}",b.Key.ToString(), result1);
            }
            Console.WriteLine("*****************");
            return;
        }

        public IEnumerable<Object> HealthyWolfsBears(IEnumerable<Animal> animals)
        {
            return animals.Where(a => ((a.Health > 3) && (a.Kind == Kind.Bear || a.Kind == Kind.Wolf))).Select(x => x);
        }

        public void HealthiestSickest(IEnumerable<Animal> animals, string s1)
        {
            var result = animals.OrderByDescending(x => x.Health).ThenBy(x => x.NickName);
            Console.WriteLine("\n********* {0} ********", s1);
            Console.WriteLine(result.First());
            Console.WriteLine(result.Last());
            Console.WriteLine("*****************");
            return;
        }

        public void AvgHealth(IEnumerable<Animal> animals, string s1)
        {
            var result = animals.Average(a => a.Health); ;
            Console.WriteLine("\n********* {0} ********", s1);
            Console.WriteLine(result);
            Console.WriteLine("*****************");
            return;
        }
    }
}
