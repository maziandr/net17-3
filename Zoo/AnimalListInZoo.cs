using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Zoo
{
    class AnimalListInZoo
    {
        public List<Animal> Records = null;

        public AnimalListInZoo()
        { 
            Records = new List<Animal>();

        }


        public bool AddRecord(string nname , Kind kind)
        {
            try
            {
                Animal newAnimal = new Animal( nname , kind);
                string message = newAnimal.ToString();
                Records.Add(newAnimal);
                Console.WriteLine("Animal added: "+message);
            }
            catch {
                Console.WriteLine("Failed to create new animal in Zoo: ( [{0}], [{1}]) " ,nname, kind);
                return false;
            }
            return true;
        }

        public bool AddRecord(string nname, string skind)
        {
            Kind kind;
            try
            {
                kind = (Kind)Enum.Parse(typeof(Kind), skind, true);
                Animal newAnimal = new Animal(nname, kind);
                string message = newAnimal.ToString();
                Records.Add(newAnimal);
                Console.WriteLine("Animal added: " + message);
            }
            catch
            {
                Console.WriteLine("Failed to create new animal in Zoo: ( [{0}], [{1}]) ", nname, skind);
                return false;
            }
            return true;
        }

        public bool FeedAnimal(string nname)
        {
            foreach (Animal a in Records)
            {
                if( String.Compare(a.NickName, nname, true)==0)
                {
                    if (a.State == State.Hungry)
                    {
                        a.State = State.Full;
                        Console.WriteLine("Animal was eating: " + a.ToString());
                        return true;
                    }
                    else 
                    {
                        Console.WriteLine("Animal wasn't eating, it's not hungry: " + a.ToString());
                        return false;
                    }

                }
            }
            Console.WriteLine("Animal not found " + nname);
            return false;
        }

        public bool CureAnimal(string nname)
        {
            foreach (Animal a in Records)
            {
                if (String.Compare(a.NickName, nname, true) == 0 && a.State == State.Ill)
                {
                    if(++a.Health >= Animal.MaxHealth(a.Kind))
                    {
                        a.Health = Animal.MaxHealth(a.Kind);
                        a.State = State.Hungry;
                    }
                    Console.WriteLine("Animal was cured: " + a.ToString());
                    return true;
                }
                if (String.Compare(a.NickName, nname, true) == 0)
                {
                    Console.WriteLine("Animal wasn't cured, because it wasn't ill: " + a.ToString());
                    return false;
                }
            }
            Console.WriteLine("Animal not found " + nname);
            return false;
        }

        public bool StarveAnimal(string nname)
        {
            foreach (Animal a in Records)
            {
                if (String.Compare(a.NickName, nname, true) == 0 )
                {
                    switch(a.State)
                    {
                        case State.Full:
                            a.State = State.Hungry;
                            a.Health = Animal.MaxHealth(a.Kind);
                            break;
                        case State.Hungry:
                            a.State = State.Ill;
                            a.Health = Animal.MaxHealth(a.Kind);
                            break;
                        case State.Ill:
                            if(--a.Health <= 0)
                            {
                                a.State = State.Dead;
                            }
                            break;
                        case State.Dead:
                            EraseAnimalIfDead(a.NickName);
                            // Console.WriteLine("Animal was removed: " + a.ToString());
                            return true;
                        default:
                            Console.WriteLine("nothing to do with: " + a.ToString());
                            return false;
                    }
                    Console.WriteLine("Animal's condicion worsened: " + a.ToString());
                    return true;
                }
                if (String.Compare(a.NickName, nname, true) == 0)
                {
                    Console.WriteLine("Animal wasn't cured, because it wasn't ill: " + a.ToString());
                    return false;
                }
            }
            Console.WriteLine("Animal not found " + nname);
            return false;
        }

        public bool EraseAnimalIfDead(string nname)
        {
            // foreach (Animal a in Records)
            for (var i = Records.Count-1; i >= 0; i-- )
            {
                Animal a = Records[i];
                if (String.Compare(a.NickName, nname, true) == 0 && a.State == State.Dead)
                {
                    //    list.Remove(item);
                    Records.Remove(a);
                    Console.WriteLine("Animal was removed: " + a.ToString());
                    return true;
                }
                if (String.Compare(a.NickName, nname, true) == 0)
                {
                    Console.WriteLine("Animal wasn't removed, because it wasn't dead: " + a.ToString());
                    return false;
                }
            }
            Console.WriteLine("Animal wasn't removed, because not found " + nname);
            return false;
        }

        public void DestroyZooRandom()
        {
            Random rand = new Random();
            int idxRnd = 0;

            for (var i = 1; Records.Count > 0; i++)
            {
                idxRnd = rand.Next(Records.Count);
                Console.WriteLine("\n\n======= Iteration No: {0}, element No:{1}/{2}=======", i, idxRnd, Records.Count);
                Thread.Sleep(5000);
                StarveAnimal(Records[idxRnd].NickName);
                OutputFullList();
                Console.WriteLine("======{0}==========", DateTime.Now.ToString());
            }
        }

        public void OutputFullList()
        {
            Console.WriteLine("\n********* OutputFullList List ********");
            foreach (Animal a in Records)
                Console.WriteLine(a.ToString());
        }

        public void OutputFullList(IEnumerable<Object> animals, string s1)
        {
            Console.WriteLine("\n********* {0} ********", s1);
            if (animals != null)
            {
                foreach (var a in animals)
                    Console.WriteLine(a.ToString());
            }
            else {
                Console.WriteLine("<<<empty>>>");
            }
            Console.WriteLine("*****************");
        }
    }
}
