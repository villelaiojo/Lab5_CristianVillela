using System;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Runtime.CompilerServices;
using static Lab3_Crisitan_Villela.Program;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Lab3_Crisitan_Villela
{
    class Program
    {
        public class User
        {
            public long dpi { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Job { get; set; }
            public string PlaceJob { get; set; }
            public int Salary
            {
                get; set;
            }
        }

        public class Customer
        {
                public long dpi { get; set; }
                public int Budget { get; set; }
                public DateTime Date { get; set; }

        }

        public class Acciones
        {
            public string? Property { get; set; }
            public List<Customer>? Customers { get; set; }
            public int Rejection { get; set; }
        }

        static void Main(string[] args)
        {
            List<User> Usuario = new List<User>();
            Acciones Actions;

            string p = "C:\\Users\\Cristia Villela\\OneDrive\\Escritorio\\Lab3_Walter_Villatoro\\bin\\Debug\\netcoreapp3.1\\input_customer_challenge_lab_3.jsonl";
            using (StreamReader leer = new StreamReader(p))
            {
                while (!leer.EndOfStream)
                {
                    string linea = leer.ReadLine();
                    User persona = new User();
                    persona = JsonConvert.DeserializeObject<User>(linea);
                    Usuario.Add(persona);
                }
            }
            string p1 = "C:\\Users\\Cristia Villela\\OneDrive\\Escritorio\\Lab3_Walter_Villatoro\\bin\\Debug\\netcoreapp3.1\\Costumer.JSONL";
            using (StreamReader leer = new StreamReader(p1))
            {
                string linea = leer.ReadToEnd();
                Actions = JsonConvert.DeserializeObject<Acciones>(linea);
            }

            long[] DPI = Enumerable.Repeat(0L, Actions.Customers.Count).ToArray();
            int[] BUDGETS = Actions.Customers.Select(c => c.Budget).ToArray();
            int posicion = 0;

            foreach (var item in Actions.Customers)
            {
                DPI[posicion] = item.dpi;
                posicion++;
            }
            posicion = 0;
            foreach (var item in Actions.Customers)
            {
                BUDGETS[posicion] = item.Budget;
                posicion++;
            }
            for (int i = 0; i < DPI.Length - 1; i++)
            {
                for (int j = 0; j < DPI.Length - (1 + i); j++)
                {
                    if (BUDGETS[j] < BUDGETS[j + 1])
                    {

                        int tempBudget = BUDGETS[j];
                        BUDGETS[j] = BUDGETS[j + 1];
                        BUDGETS[j + 1] = tempBudget;


                        long tempDPI = DPI[j];
                        DPI[j] = DPI[j + 1];
                        DPI[j + 1] = tempDPI;
                    }
                }
            }
            long Ganador = DPI[Actions.Rejection];
            int budgetGanador = default;
            DateTime FechaGanador = new DateTime();
            foreach (var item in Actions.Customers)
            {
                if (Ganador == item.dpi)
                {
                    budgetGanador = item.Budget;
                    FechaGanador = item.Date;
                    break;
                }
            }
            foreach (var item in Usuario)
            {
                if (Ganador == item.dpi)
                {

                   
                    using (var sha256 = SHA384.Create())
                    {
                        byte[] hash = sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes(item.PlaceJob));
                        Console.WriteLine("{" + "\"dpi\":" + item.dpi + ",\"budget\":" + budgetGanador + ",\"date\":" + "\"" + FechaGanador + "\",\"firstName\":\"" + item.FirstName + "\",\"lastName:\"" + item.LastName + "\",\"birthDate\":\"" + item.BirthDate + "\",\"job\":\"" + item.Job + "\",\"placeJob\":\"" + item.PlaceJob + "\",\"salary\":" + item.Salary + ",\"property\":\"" + Actions.Property + "\",\"signature\":\"" + Convert.ToBase64String(hash) + "\"}");
                        break;

                    }

                }

            }
            Console.ReadKey();


        }
    }
}
