using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Lab2_Cristian_Villela
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\Cristia Villela\OneDrive\Escritorio\Lab5_CristianVillela\Lab2_Cristian_Villela\bin\Debug\netcoreapp3.1\input_challenge_lab_2.jsonl"))
            {
                InputLab input = JsonSerializer.Deserialize<InputLab>(line);

                var algo = input.input1;
                var algo1 = input.input2;

                Dictionary<string, double> sortApartments = new Dictionary<string, double>();
                Dictionary<string, double> sortHouses = new Dictionary<string, double>();
                Dictionary<string, double> sortPremises = new Dictionary<string, double>();

                if (input.input2.typeBuilder == "Apartments")
                {
                    foreach (var itemApartments in input.input1)
                    {
                        if (itemApartments.builds.Apartments != null)
                        {
                            foreach (var item1Apartments in itemApartments.builds.Apartments)
                            {
                                if (input.input2.wannaPetFriendly == item1Apartments.isPetFriendly)
                                {
                                    if (input.input2.wannaPetFriendly == true) 
                                    {
                                        if (item1Apartments.price < input.input2.budget)
                                        {
                                            sortApartments.Add(item1Apartments.id, item1Apartments.price);
                                        }
                                        else { }
                                    }
                                    else  
                                    {
                                        if (item1Apartments.price < input.input2.budget)
                                        {
                                            sortApartments.Add(item1Apartments.id, item1Apartments.price);
                                        }
                                        else { }
                                    }
                                }
                            }
                        }
                    }

                    var itemsApartments = from pair in sortApartments orderby pair.Value ascending select pair;

                    foreach (var itemSorted in itemsApartments)
                    {
                        Console.Write("[\"" + itemSorted.Key + "\"],");

                    }

                    Console.Write("\n");

                }
               

            }

        }
    }
}
