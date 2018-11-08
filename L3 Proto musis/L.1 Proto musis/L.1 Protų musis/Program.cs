//Martynas Rišys
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace L._1_Protų_mūšis
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> daugiausiaiklausimuuzdaveautoriai = new List<string>();
            int rinkiniuskaicius = 0;
            int klausimuskaicius = 0;

            Program p = new Program();
            List<Klausimas> klausimas = p.Skaitymas(); //Nuskaitomi duomenys iš duomenų failo
            List<string> temupavadinimai = p.TemuFiltravimas(klausimas); //Sukuriamas sąrašas kuriame temos nesikartoja
            p.DaugiausiaiKlausimuIsvedimas(klausimas, daugiausiaiklausimuuzdaveautoriai);// Autoriu uždaviusiu daugiausiai klausimų išvedimas
            List<string> sudetingiausiostemos = p.SunkiausiosTemos(klausimas, temupavadinimai);//Sukuriamas sąrašas kuriame laikomi sudėtingiausiu temu pavadinimai
            p.SudetingiausiuTemuIsvedimas(klausimas, sudetingiausiostemos); //Sudetingiausiu temų išvedimas
            p.NereikalinguFailuNaikinimas();//Sunaikinami rezultatu failai kurie buvo sukurti paleidus programa anksčiau
            p.AtsitiktiniuRinkiniuSpausdinimas(klausimas, rinkiniuskaicius, klausimuskaicius);//Sukuriami .csv failai kuriuose yra įkelti klausymai ir duomenys apie juos
            p.DuomenuPateikimasLenteleje(klausimas);// Pateikiami pradiniai duomenys lentelėje

        }
        /// <summary>
        /// Skaitomi duomenys iš tekstinio failo
        /// </summary>
        /// <returns>Gražina klausimų sąrašą</returns>
        List<Klausimas> Skaitymas()
        {
            List<Klausimas> klausimas = new List<Klausimas>();

            string[] lines = File.ReadAllLines(@"L1-5a.csv", Encoding.UTF8);

            foreach (string line in lines.Skip(1))
            {
                string[] values = line.Split(';');
                string Tema = values[0];
                int Sudėtingumas = int.Parse(values[1]);
                string Autorius = values[2];
                string KlausimoTekstas = values[3];
                string AtsakymoVariantai = values[4];
                string TeisingasAtsakymas = values[5];
                int Balai = int.Parse(values[6]);

                Klausimas K = new Klausimas(Tema, Sudėtingumas, Autorius, KlausimoTekstas, AtsakymoVariantai, TeisingasAtsakymas, Balai);
                klausimas.Add(K);
            }
            return klausimas;
        }
        /// <summary>
        /// Filtruoja viso sąrašo temas tam, kad jos nesikartotu
        /// </summary>
        /// <param name="klausimas"></param>
        /// <returns>Gražina sąrašą nesikartojančiu temų</returns>
        List<string> TemuFiltravimas(List<Klausimas> klausimas)
        {
            List<string> temupavadinimai = new List<string>();

            for (int i = 0; i < klausimas.Count; i++)
            {
                if (!temupavadinimai.Contains(klausimas[i].Tema)) //Tikrinama ar sąraše yra toks vardas, jei ne jis yra įkeliamas
                {
                    temupavadinimai.Add(klausimas[i].Tema);
                }
            }
            return temupavadinimai;
        }
        /// <summary>
        /// Ieškoma sunkiausia tema(-os) lyginant jų sudėtingumus
        /// </summary>
        /// <param name="klausimas"></param>
        /// <param name="temupavadinimai"></param> nesikartojančiu temų sąrašas
        /// <returns>Gražina sunkiausia temą(-as)</returns>
        List<string> SunkiausiosTemos(List<Klausimas> klausimas, List<string> temupavadinimai)
        {
            List<string> sudetingiausiostemos = new List<string>();

            double didziausiasvidurkis = 0;
            int laikinabalusuma;
            int temospasikartojimokiekis;
            double laikinasvidurkis = 0;

            for (int i = 0; i < temupavadinimai.Count; i++) //Ciklas kartojamas tiek kartu, kiek yra skirtingu temų
            {
                temospasikartojimokiekis = 0;
                laikinabalusuma = 0;

                for (int j = 0; j < klausimas.Count; j++) //Ciklas kartojamas tiek kartu, kiek yra skirtingu klausimų
                {
                    if (temupavadinimai[i] == klausimas[j].Tema)
                    {
                        laikinabalusuma += klausimas[j].Sudėtingumas;
                        temospasikartojimokiekis++;
                    }
                }
                laikinasvidurkis = (double)laikinabalusuma / temospasikartojimokiekis;

                if (didziausiasvidurkis < laikinasvidurkis) // Rastas naujas didžiausias vidurkis
                {
                    sudetingiausiostemos.Clear(); // Ištrinami visi duomenys iš sąrašo nes atrasta tema, kuri yra sunkesnė
                    didziausiasvidurkis = laikinasvidurkis;
                    sudetingiausiostemos.Add(temupavadinimai[i]);
                }
                else if (didziausiasvidurkis == laikinasvidurkis)
                {
                    sudetingiausiostemos.Add(temupavadinimai[i]);
                }
            }
            return sudetingiausiostemos;
        }
        /// <summary>
        /// Ieškoma autoriaus(-ų), kurie uždavė daugiausiai klausimų ir kiek jų uždavė
        /// </summary>
        /// <param name="klausimas"></param>
        /// <param name="daugiausiaiklausimuuzdaveautoriai"></param> Sąrašas, kuriame talpinami autoriu vardai, kurie uždavė daugiausiai klausimų
        /// <returns>Gražinamas vieno autoriaus didžiausias paklaustų klausimų kiekis</returns>
        int DaugiausiaiKlausimu(List<Klausimas> klausimas, List<string> daugiausiaiklausimuuzdaveautoriai)
        {
            int klausimuskaicius = 0;
            int laikinasklausimuskaicius;
            string laikinasvardas;

            for (int i = 0; i < klausimas.Count; i++)
            {
                laikinasvardas = klausimas[i].Autorius;
                laikinasklausimuskaicius = 0;

                for (int j = 0; j < klausimas.Count(); j++)
                {
                    if (laikinasvardas == klausimas[j].Autorius)
                    {
                        laikinasklausimuskaicius++;
                    }
                }
                if (laikinasklausimuskaicius > klausimuskaicius) // Rastas naujas autorius, kuris uždavė daugiausiai klausimų
                {
                    daugiausiaiklausimuuzdaveautoriai.Clear(); // Panaikinami visi duomenys iš sąrašo nes atrastas autorius, kuris uždavė daugiau klausimų
                    klausimuskaicius = laikinasklausimuskaicius;
                    daugiausiaiklausimuuzdaveautoriai.Add(laikinasvardas);
                }
                if (laikinasklausimuskaicius == klausimuskaicius && !daugiausiaiklausimuuzdaveautoriai.Contains(laikinasvardas))
                {
                    daugiausiaiklausimuuzdaveautoriai.Add(laikinasvardas);
                }
            }
            return klausimuskaicius;
        }
        /// <summary>
        /// Panaikinami visi rezultatų failai, kurie galėjo būti sukurti, kai programa buvo paleista anksčiau
        /// </summary>
        void NereikalinguFailuNaikinimas()
        {
            string failopavadinimas;
            string failonumeris;

            int laikinaskintamasis = 1;
            failonumeris = laikinaskintamasis.ToString();
            failopavadinimas = $"Klausimai{failonumeris}.csv";

            while (File.Exists(failopavadinimas))
            {
                File.Delete($"Klausimai{failonumeris}.csv");
                laikinaskintamasis++;
                failonumeris = laikinaskintamasis.ToString();
                failopavadinimas = $"Klausimai{failonumeris}.csv";
            }
        }
        /// <summary>
        /// Sukuriamas masyvas, kuris yra pripildytas atsitiktinių skaičių
        /// </summary>
        /// <param name="klausimas"></param>
        /// <param name="rinkiniuskaicius"></param>Kiek skirtingu rezultatų failų yra sukuriama, šis skaičius yra įvedamas klaviatura
        /// <param name="klausimuskaicius"></param>Kiek klausimų yra kiekviename rezultatų faile, šis skaičius yra įvedamas klaviatura
        /// <returns>Gražinamas masyvas, kuris yra pripildytas atsitiktinių skaičių</returns>
        int[] AtsitiktiniaiRinkiniai(List<Klausimas> klausimas, out int rinkiniuskaicius, out int klausimuskaicius)
        {
            Console.WriteLine("Kiek skirtingu rinkiniu norite sudaryti: ");
            rinkiniuskaicius = int.Parse(Console.ReadLine());
            Console.WriteLine($"Kiek klausimų norite, kad rinkinys turėtu (nuo 0 iki {klausimas.Count}): ");
            klausimuskaicius = int.Parse(Console.ReadLine());

            Random atsitiktinisskaicius = new Random();
            int[] atsitiktiniuskaiciumasyvas = new int[rinkiniuskaicius * klausimuskaicius]; // Sukuriamas masyvas, kuris 
            var sarasoklonas = new List<Klausimas>(klausimas); // Sukuriamas toks pats sąrašas
            int laikinaskintamasis = 0;

            for (int i = 0; i < rinkiniuskaicius; i++)
            {
                for (int j = 0; j < klausimuskaicius; j++)
                {
                    atsitiktiniuskaiciumasyvas[laikinaskintamasis] = atsitiktinisskaicius.Next(0, sarasoklonas.Count - 1);
                    sarasoklonas.RemoveAt(atsitiktiniuskaiciumasyvas[laikinaskintamasis]);
                    laikinaskintamasis++;
                }
                sarasoklonas = new List<Klausimas>(klausimas);
            }
            return atsitiktiniuskaiciumasyvas;
        }
        /// <summary>
        /// Sukuriami .csv tipo failai, ir juose patalpinami duomenys apie atsitiktinai išrinktus klausimus
        /// </summary>
        /// <param name="klausimas"></param>
        /// <param name="rinkiniuskaicius"></param>Kiek skirtingu rezultatų failų yra sukuriama
        /// <param name="klausimuskaicius"></param>Kiek klausimų yra rezultatų faile
        void AtsitiktiniuRinkiniuSpausdinimas(List<Klausimas> klausimas, int rinkiniuskaicius, int klausimuskaicius)
        {
            int[] atsitiktiniuskaiciumasyvas = AtsitiktiniaiRinkiniai(klausimas, out rinkiniuskaicius, out klausimuskaicius);
            string failopavadinimas;
            string failonumeris;
            int laikinaskintamasis = 0;
            var sarasoklonas = new List<Klausimas>(klausimas); // Sukuriamas toks pats sąrašas

            for (int i = 1; i < rinkiniuskaicius + 1; i++)
            {
                failonumeris = i.ToString();
                failopavadinimas = $"Klausimai{failonumeris}.csv";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(failopavadinimas))
                {
                    file.WriteLine("sep=;");// Naudojama tam, kad perkeltu i kitą langelį csv faile
                    for (int j = 0; j < klausimuskaicius; j++)
                    {
                        
                        file.WriteLine("{0};{1};{2}", sarasoklonas[atsitiktiniuskaiciumasyvas[laikinaskintamasis]].Tema,
                                       sarasoklonas[atsitiktiniuskaiciumasyvas[laikinaskintamasis]].KlausimoTekstas,
                                       sarasoklonas[atsitiktiniuskaiciumasyvas[laikinaskintamasis]].Balai.ToString());
                        sarasoklonas.RemoveAt(atsitiktiniuskaiciumasyvas[laikinaskintamasis]);
                        laikinaskintamasis++;
                    }
                }
                sarasoklonas = new List<Klausimas>(klausimas);
            }
        }
        /// <summary>
        /// Sukuriama duomenų lentelė, kuri yra išsaugojama .txt tipu
        /// </summary>
        /// <param name="klausimas"></param>
        void DuomenuPateikimasLenteleje(List<Klausimas> klausimas)
        {
            using (StreamWriter failopavadinimas = new StreamWriter("L1Duomenųlentelė.txt"))
            {
                failopavadinimas.WriteLine("Duomenys apie klausymus:");
                failopavadinimas.WriteLine(new String('-', 360));
                failopavadinimas.WriteLine("| {0, -22} | {1, 12} | {2, -23} | {3, -150} | {4, -90} | {5, -35} | {6, 6} |",
                                           "Tema", "Sudėtingumas", "Klausimo autorius", "Klausimo tekstas",
                                           "Atsakymo variantai", "Teisingas atsakymas", "Balai");
                failopavadinimas.WriteLine(new String('-', 360));

                for (int i = 0; i < klausimas.Count; i++)
                {
                    failopavadinimas.WriteLine("| {0, -22} | {1, -12} | {2, -23} | {3, -150} | {4, -90} | {5, -35} | {6, -6} |",
                                               klausimas[i].Tema, klausimas[i].Sudėtingumas, klausimas[i].Autorius,
                                               klausimas[i].KlausimoTekstas, klausimas[i].AtsakymoVariantai,
                                               klausimas[i].TeisingasAtsakymas, klausimas[i].Balai);
                    failopavadinimas.WriteLine(new String('-', 360));
                }
            }
        }
        /// <summary>
        /// Gauti rezultatai spausdinami konsolėje
        /// </summary>
        /// <param name="klausimas"></param>
        /// <param name="daugiausiaiklausimuuzdaveautoriai"></param>Sąrašas, kuriame talpinami autoriu vardai, kurie uždavė daugiausiai klausimų
        void DaugiausiaiKlausimuIsvedimas(List<Klausimas> klausimas, List<string> daugiausiaiklausimuuzdaveautoriai)
        {
            Console.WriteLine("Daugiausiai užduota klausimų: {0}", DaugiausiaiKlausimu(klausimas, daugiausiaiklausimuuzdaveautoriai));
            Console.Write("Daugiausiai klausimų uždavė: ");
            for (int i = 0; i < daugiausiaiklausimuuzdaveautoriai.Count; i++)
            {
                Console.Write("{0};", daugiausiaiklausimuuzdaveautoriai[i]);
            }

            
        }
        /// <summary>
        /// Autoriu uždaviusiu daugiausiai klausimų išvedimas
        /// </summary>
        /// <param name="klausimas"></param>
        /// <param name="sudetingiausiostemos"></param>Sąrašas, kuriame talpinami sudėtingiausiu temu pavadinimai
        void SudetingiausiuTemuIsvedimas(List<Klausimas> klausimas, List<string> sudetingiausiostemos)
        {

            Console.WriteLine();
            Console.Write("Sudėtingiausios(-a) temos(-a): ");
            for (int i = 0; i < sudetingiausiostemos.Count; i++)
            {
                Console.Write("{0};", sudetingiausiostemos[i]);
            }
            Console.WriteLine();

        }
    }
}
