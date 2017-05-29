using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    class MainMenu
    {
        private static MainMenu instance;
        private DataLINQ data;

            private MainMenu()
            {
                data = DataLINQ.getInstance();
            }

            public static MainMenu getInstance()
            {
                if (instance == null)
                    instance = new MainMenu();
                return instance;
            }

            public bool Processed(AnimalListInZoo animals)
            {

                Console.WriteLine("\n" + "\n" + "###commands:(kindOfAnimal must be Lion, Tiger, Elephant, Bear, Wolf or Fox)###");
                Console.WriteLine("Add new: [add <nickName> <kindOfAnimal>]");
                Console.WriteLine("Give food: [feed <nickName>]");
                Console.WriteLine("Provide medical care: [cure <nickName>]");
                Console.WriteLine("Remove from zoo: [erase <nickName>]");
                Console.WriteLine("Make state worse: [starve <nickName>]");
                Console.WriteLine("\nAll, grouped by kind of animal: [allkind]");
                Console.WriteLine("All, in some state(Full, Hungry, Ill, Dead): [allstate <state>]");
                Console.WriteLine("Ill Tigers: [illtigers]");
                Console.WriteLine("Elephant with nickname: [nickel <nickName>]");
                Console.WriteLine("Nicknames of Hungry animals: [hungryn]");
                Console.WriteLine("Healthiest animals of each kind(only one): [healthk]");
                Console.WriteLine("Count of dead animals of each kind: [deadk]");
                Console.WriteLine("Wolfs and Bears with>3 health: [hwb]");
                Console.WriteLine("Healthiest and Sickest animals: [minmax]");
                Console.WriteLine("Average health in zoo: [avg]");
                Console.WriteLine("\ndestroy");
                Console.WriteLine("exit"); 
                Console.Write("\n" + "Enter need command: ");
 
                string strIn = Console.ReadLine();
                string[] command = strIn.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if(command.Length > 0 )
                {
                    switch (command[0].ToLower())
                    {
                        case "add":
                            if (command.Length > 2 && animals.AddRecord(command[1], command[2]))
                            {
                                return true;
                            }
                            break;
                        case "feed":
                            if (command.Length > 1 && animals.FeedAnimal(command[1]))
                            {
                                return true;
                            }
                            break;
                        case "cure":
                            if (command.Length > 1 && animals.CureAnimal(command[1]))
                            {
                                return true;
                            }
                            break;
                        case "erase":
                            if (command.Length > 1 && animals.EraseAnimalIfDead(command[1]))
                            {
                                return true;
                            }
                            break;
                        case "starve":
                            if (command.Length > 1 && animals.StarveAnimal(command[1]))
                            {
                                return true;
                            }
                            break;
                        case "destroy":
                            Console.WriteLine("End of zoo...!!!");
                            animals.DestroyZooRandom();
                            return false;
                        case "exit":
                            Console.WriteLine("End of changing zoo...");
                            return false;
                        case "allkind":
                            animals.OutputFullList(data.AllByKind(animals.Records), "All, grouped by kind of animal:");
                            return true;
                        case "allstate":
                            animals.OutputFullList((command.Length > 1) ? data.AllInState(animals.Records, command[1]) : null, 
                                (command.Length > 1) ? "Animals in state: " + command[1] : "Wrong format command");
                                return true;
                        case "illtigers":
                                animals.OutputFullList(data.IllTigers(animals.Records), "Ill Tigers:");
                                return true;
                        case "nickel":
                                animals.OutputFullList((command.Length > 1) ? data.NickElephant(animals.Records, command[1]) : null,
                                    (command.Length > 1) ? "Elephant with nickname: " + command[1] : "Wrong format command");
                                return true;
                        case "hungryn":
                                animals.OutputFullList(data.HungryNicks(animals.Records), "Nicknames of Hungry animals:");
                                return true;
                        case "healthk":
                                animals.OutputFullList(data.KindHealthiest(animals.Records), "Healtiest animals of each kind(only one):");
                                return true;
                        case "deadk":
                                data.KindDead(animals.Records, "Count of dead animals of each kind:");
                                return true;
                        case "hwb":
                                animals.OutputFullList(data.HealthyWolfsBears(animals.Records), "Wolfs and Bears with>3 health: ");
                                return true;
                        case "minmax":
                                data.HealthiestSickest(animals.Records, "Healthiest and Sickest animals:");
                                return true;
                        case "avg":
                                data.AvgHealth(animals.Records, "Average health in zoo:");
                                return true;

                        default:
                            Console.WriteLine("Unrecognized command");
                            return true;
                    }                    
                }
                return true;
            }
    }
}

