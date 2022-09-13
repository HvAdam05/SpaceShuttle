using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpaceShuttle
{
    internal class Program
    {
        struct Ursiklo
        {
            public string Kod;
            public string Datum;
            public string Nev;
            public int Nap;
            public int Ora;
            public string Land;
            public int Legenyseg;

            public Ursiklo(string sor)
            {
                var dbok = sor.Split(';');
                this.Kod = dbok[0];
                this.Datum = dbok[1];
                this.Nev = dbok[2];
                this.Nap = int.Parse(dbok[3]);
                this.Ora = int.Parse(dbok[4]);
                this.Land = dbok[5];
                this.Legenyseg = int.Parse(dbok[6]);
            }
        }

        static List<Ursiklo> Ursiklo_List = new List<Ursiklo>();
        static void Main(string[] args)
        {
            Feladat23();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
            Feladat8();
            Feladat9();
            Feladat10();
            Console.ReadKey();
        }
        private static void Feladat10()
        {
            List<string> Ursiklonev_list = new List<string>() { };
            foreach (var U in Ursiklo_List)
            {
                if (!Ursiklonev_list.Contains(U.Nev))
                {
                    Ursiklonev_list.Add(U.Nev);
                }
            }
            double[] Ursiklonap_list = new double[Ursiklonev_list.Count];
            for (int i = 0; i < Ursiklo_List.Count; i++)
            {
                for (int j = 0; j < Ursiklonev_list.Count; j++)
                {
                    if (Ursiklo_List[i].Nev == Ursiklonev_list[j])
                    {
                        Ursiklonap_list[j] += Ursiklo_List[i].Nap + (double)Ursiklo_List[i].Ora / 24;
                    }
                }
            }

            var sw = new StreamWriter(@"ursiklok.txt", false, Encoding.UTF8);
            for (int i = 0; i < Ursiklonev_list.Count; i++)
            {
                sw.WriteLine("{0}\t{1:0.00}", Ursiklonev_list[i], Ursiklonap_list[i]);
            }
            sw.Close();

        }

        private static void Feladat9()
        {
            Console.WriteLine("9. feladat");
            double Landolt = 0;
            foreach (var u in Ursiklo_List)
            {
                if (u.Land == "Kennedy")
                {
                    Landolt++;
                }
            }

            double Teljesitetszazalek =Landolt / Ursiklo_List.Count  *100;
            Console.WriteLine("\tA Küldetések {0:0.00}%-a fejeződött be a Kennedy űrközpontban.",Teljesitetszazalek);

        }

        private static void Feladat8()
        {
            int Evszam;
            Console.WriteLine("8. feladat");
            do
            {
                Console.Write("\tÉvszám:");
                Evszam = int.Parse(Console.ReadLine());

            } while (Evszam < 1980 && Evszam < 2012);
            int Szaml = 0, Szamlalo = 0;
            while (Szamlalo < Ursiklo_List.Count)
            {
                if (int.Parse(new string(Ursiklo_List[Szamlalo].Datum.Take(4).ToArray())) == Evszam)
                {
                    Szaml++;
                }
                Szamlalo++;
            }
            Console.WriteLine("\t Ebben az évben {0} küldetés volt.", Szaml);
        }

        private static void Feladat7()
        {
            Console.WriteLine("7. feladat");
            List<int> KuldHoss_l = new List<int>();
            foreach (var u in Ursiklo_List)
            {
                KuldHoss_l.Add(u.Nap * 24 + u.Ora);
            }
            int MaxKuld = KuldHoss_l.Max();
            for (int i = 0; i < KuldHoss_l.Count; i++)
            {
                if (MaxKuld == KuldHoss_l[i])
                {
                    Console.WriteLine("\tA lehgosszabb ideig a {0} volt az űrben, {1} volt a sikló kódja , ennyi ideig volt az űrben {2} óra", Ursiklo_List[i].Nev, Ursiklo_List[i].Kod, MaxKuld);
                }
            }

        }

        private static void Feladat6()
        {
            Console.WriteLine("6. feladat:");
            int Szamlalo = 0;
            while (Szamlalo < Ursiklo_List.Count && Ursiklo_List[Szamlalo].Land != "nem landolt" || Ursiklo_List[Szamlalo].Nev.ToLower() != "columbia")
            {
                Szamlalo++;
            }
            Console.WriteLine("\t{0} asztronauta volt a Columbia fedélzetén, annak utolsó útján", Ursiklo_List[Szamlalo].Legenyseg);
        }

        private static void Feladat5()
        {
            Console.WriteLine("5. feladat:");
            int OtKissebb = 0;
            foreach (var U in Ursiklo_List)
            {
                if (U.Legenyseg < 5)
                {
                    OtKissebb++;
                }
            }
            Console.WriteLine("\tÖsszesen {0} alaklommal küldtek 5-nél kevesebb embert.", OtKissebb);
        }

        private static void Feladat4()
        {
            Console.WriteLine("4. feladat:");
            int OszUtas = 0;
            foreach (var u in Ursiklo_List)
            {
                OszUtas += u.Legenyseg;
            }
            Console.WriteLine("\t{0} utas indult az űrbe Összesen.", OszUtas);
        }

        private static void Feladat23()
        {
            Console.WriteLine("3. feladat:");
            var sr = new StreamReader(@"kuldetesek.csv", Encoding.UTF8);
            int SZ = 0;
            while (!sr.EndOfStream)
            {
                Ursiklo_List.Add(new Ursiklo(sr.ReadLine()));
                SZ++;
            }
            sr.Close();
            Console.WriteLine("\tAz összes {0} alkalommal indítottak úrhajót. ", SZ);
        }
    }
}
