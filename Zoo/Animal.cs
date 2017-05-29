using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    public class Animal
    {

        public Kind Kind { get; set; }
        public int Health { get; set; }
        public State State { get; set; }
        public string NickName { get; set; }


        public Animal(string nname = "", Kind kind = Kind.Unknown)
        {
            NickName = nname;
            Kind = kind;
            Health = MaxHealth(Kind);
            State = State.Full;

        }

        public override string ToString()
        {
            return string.Format("[nick:{0}, Kind of Animal: {1}, health:{2}, state:{3}]",NickName, Kind, Health, State);
        }

        public static int MaxHealth(Kind kind)
        {
            switch (kind)
            {
                case Kind.Lion: return 5;
                case Kind.Tiger: return 4;
                case Kind.Elephant: return 7;
                case Kind.Wolf: return 4;
                case Kind.Bear: return 6;
                case Kind.Fox: return 3;
                default: throw new NotImplementedException("Unkown kind of animal. This implementation not processed");
            }
        }
    }

    public enum State
    {
        Full,
        Hungry,
        Ill,
        Dead
    }

    public enum Kind
    {
        Lion, Tiger, Elephant, Bear, Wolf, Fox, Unknown
    }

 

    
}
