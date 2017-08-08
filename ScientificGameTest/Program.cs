using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificGameTest
{

    class MyData
    {
        private int[] births = null;
        private int[] deaths = null;
        private Random rd = new Random();

        public int[] Births
        {
            get
            {
                return births;
            }
        }

        public int[] Deaths
        {
            get
            {
                return deaths;
            }            
        }

        public MyData(int numberOfPeople,int minAge,int maxAge)
        {
            births = new int[numberOfPeople];
            deaths = new int[numberOfPeople];

            //Generate info
            for (int i = 0; i < numberOfPeople; i++)
            {
                int d = GetDeathYear(minAge);
                int b = GetBirthYear(d,minAge,maxAge);
                deaths[i] = d;
                births[i] = b;
            }
        }
        int GetDeathYear(int minAge)
        {
            int rs = -1;
            do
            {
                rs = rd.Next(1900 + minAge, 2000);
            } while (rs - 1900 < 0);
            
            return rs;
        }
        int GetBirthYear(int deadYear,int minAge,int maxAge)
        {
            int rs = -1;
            do
            {
                int age = rd.Next(minAge, maxAge);
                rs = deadYear - age + 1;
            } while (rs - 1900 < 0);
            
            
            return rs;
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {

            //We can change the data for generator here
            MyData d0 = new MyData(500, 1, 90);
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < d0.Births.Length; i++)
            {
                sb.Append(d0.Births[i] + " - " + d0.Deaths[i] + "\n");
                Console.WriteLine(d0.Births[i] + " - " + d0.Deaths[i] );
            }

            //Run function to get the result
            sb.Append("max year = " + maxYear(d0.Births, d0.Deaths));
            
            Console.WriteLine(sb.ToString());
            Console.ReadLine();

            //The output data
            File.WriteAllText("data.txt", sb.ToString());
        }

        

        public static int maxYear(int[] births, int[] deaths)
        {
            int[] count = new int[101];
            for(int i = 0; i < births.Length; i++)
            {
                int b = births[i];
                count[b - 1900]++;
            }
            for (int i = 0; i < deaths.Length; i++)
            {
                int d = deaths[i];
                count[d - 1900]--;
            }

            int maxPopulation = count[0];
            int year = 0;
            int population = count[0];
            for(int i = 1; i < count.Length; i++)
            {
                population += count[i];
                if (population > maxPopulation)
                {
                    maxPopulation = population;
                    year = i;
                }
            }
            return year + 1900;
        }
    }
}
