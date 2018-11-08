//Arminas Marozas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    class Program
    {
        const int maxAtstovybiuKiekis = 10; //didžiausias atstovybių kiekis
        
        static void Main(string[] args)
        {
            int atstovybiuKiekis = 0; //Kintamasis, kuris nurodo, kiek yra atstovybių
            int skaicius = 0; //Istorinių klausimų kiekis

            Console.OutputEncoding = Encoding.UTF8; //Konsolėje rašomos lietuviškos raidės
            Program p = new Program(); //Program klasės objektas, kad galima būtų prieiti prie metodų
            Atstovybe[] atstovybes = new Atstovybe[maxAtstovybiuKiekis];   //objekto masyve saugomi atstovybių pavadinimai
           
            p.Skaitymas(ref atstovybes, ref atstovybiuKiekis);//Skaitymo metodas

            if (atstovybes[0].AtstovybesPav == null)
            {
                Console.WriteLine("Duomenų failuose nėra");
                //Jeigu duomenų faile nėra, rezultatų failus palieka tuščius
                string[] istoriniai = new string[1000];
                p.IstoriniuKlausimuSpausdinimasFaile(istoriniai, skaicius);
                KlausimuKonteineris visiParasytiKlausimai = new KlausimuKonteineris();
                p.VisuKlausimuSpausdinimasFaile(visiParasytiKlausimai);
                p.DuomenuPateikimasLenteleje(atstovybes, atstovybiuKiekis);
            }
            else
            {

                DaugiausiaKlausimuBendrai autoriaiBendr = new DaugiausiaKlausimuBendrai(); //Iš visų atstovybių populiariausių autorių objektas
                p.DaugiausiaKlausimuIsVisoIsvedimas(atstovybes, autoriaiBendr, atstovybiuKiekis);//populiariausio autoriaus apskritai išvedimo metodas

                Console.WriteLine(new String('-', 100)); //Atskiria rezultatus
                Console.WriteLine("");

                DaugiausiaKlausimuAtskirai autoriaiAts = new DaugiausiaKlausimuAtskirai(); //Skirtingų atstovybių populiariausių autorių objektas
                p.DaugiausiaiKlausimuAtstovybeseIsvedimas(atstovybes, autoriaiAts, atstovybiuKiekis);//populiariausio autoriaus kiekvienoje atstovybėje paieškos metodas

                Console.WriteLine(new String('-', 100)); //Atskiria rezultatus
                Console.WriteLine("");

                DaugiausiaKlausimuAtskirai muzAutoriaiAts = new DaugiausiaKlausimuAtskirai(); //Skirtingų atstovybių daugiausiai muzikinių klausimų uždavusių autorių objektas
                p.DaugiausiaiMuzikiniuKlausimuAtstovybeseIsvedimas(atstovybes, muzAutoriaiAts, atstovybiuKiekis); //Autorių, daugiausiai uždavusių muzikinių klausimų, išvedimas

                Console.WriteLine(new String('-', 100)); //Atskiria rezultatus
                Console.WriteLine("");

                KlausimuKonteineris visiParasytiKlausimai = new KlausimuKonteineris(); //Visų klausimų objektas
                visiParasytiKlausimai = p.VisiParasytiKlausimai(atstovybes, atstovybiuKiekis); //Suranda visus užduotus klausimus
                visiParasytiKlausimai.RikiuotiKlausimus(); //Surikiuoja visų klausimų sąrašą
                p.VisuKlausimuSpausdinimasFaile(visiParasytiKlausimai); //Atspausdina faile visus klausimus

                string[] istoriniai = new string[1000]; //Istorinių klausimų objektas
                istoriniai = p.IstoriniaiKlausimai(atstovybes, atstovybiuKiekis, ref skaicius); //Suranda visus istorinius klausimus
                if (skaicius == 0)
                {
                    Console.WriteLine("Istorinių klausimų nėra");
                    Console.WriteLine("");
                }
                p.IstoriniuKlausimuSpausdinimasFaile(istoriniai, skaicius); //Atspausdina faile visus istorinius klausimus

                p.DuomenuPateikimasLenteleje(atstovybes, atstovybiuKiekis); //Duomenys pateikiami lentele
            }

        }

        /// <summary>
        /// Metodas skirtas nuskaityti duomenis
        /// </summary>
        /// <param name="atstovybes">Atstovybės</param>
        /// <param name="atstovybiuKiekis">Atstovybių kiekis</param>
        public void Skaitymas(ref Atstovybe[] atstovybes, ref int atstovybiuKiekis)
        {
            string[] DuomenuVieta = Directory.GetFiles(@"C:\Users\Vartotojas\Desktop\L3 Proto musis\L.1 Proto musis\U3-5_Arminas_Marozas", "duomenys*.csv");

            foreach(string failas in DuomenuVieta)
            {
                using (StreamReader skaityti = new StreamReader(@failas))
                {
                    string eilute = skaityti.ReadLine();
                    if (eilute == "")
                    {
                        Atstovybe atstovybee = new Atstovybe(eilute);
                        atstovybes[atstovybiuKiekis++] = atstovybee;
                        break;
                    }
                    Atstovybe atstovybe = new Atstovybe(eilute);

                    while ((eilute = skaityti.ReadLine()) != null)
                    {   
                        if (eilute.Count(x => x == ',') == 6) //Patikrina, ar eilutėje yra 6 kableliai, jeigu ne, vadinasi klausimas yra be variantų
                        {
                            if(eilute.Count(x => x == '-') >= 1) //Jeigu yra brūkšnių, vadinasi yra atsakymų variantų(vienas nuo kito atskirti - ), jeigu ne, čia muzikinis klausimas.
                            {
                                string[] dalys = eilute.Split(',');
                                string Tema = dalys[0];
                                int Sudetingumas = int.Parse(dalys[1]);
                                string Autorius = dalys[2];
                                string KlausimoTekstas = dalys[3];
                                string Variantai = dalys[4];
                                string TeisingasAtsakymas = dalys[5];
                                int Balai = int.Parse(dalys[6]);
                                KlausimasSuVariantais klausimas = new KlausimasSuVariantais(Tema, Sudetingumas, Autorius, KlausimoTekstas, Variantai, TeisingasAtsakymas, Balai);
                                atstovybe.KlausimaiSuVariantais.PridetiKlausima(klausimas);
                                atstovybe.VisiKlausimai.PridetiKlausima(klausimas);
                            }
                            else
                            {
                                string[] dalys = eilute.Split(',');
                                string Tema = dalys[0];
                                int Sudetingumas = int.Parse(dalys[1]);
                                string Autorius = dalys[2];
                                string KlausimoTekstas = dalys[3];
                                string TeisingasAtsakymas = dalys[4];
                                int Balai = int.Parse(dalys[5]);
                                string FailoPavadinimas = dalys[6];
                                MuzikinisKlausimas klausimas = new MuzikinisKlausimas(Tema, Sudetingumas, Autorius, KlausimoTekstas, TeisingasAtsakymas, Balai, FailoPavadinimas);
                                atstovybe.MuzikiniaiKlausimai.PridetiKlausima(klausimas);
                                atstovybe.VisiKlausimai.PridetiKlausima(klausimas);
                            }
                        }
                        else
                        {
                            string[] dalys = eilute.Split(',');
                            string Tema = dalys[0];
                            int Sudetingumas = int.Parse(dalys[1]);
                            string Autorius = dalys[2];
                            string KlausimoTekstas = dalys[3];
                            string TeisingasAtsakymas = dalys[4];
                            int Balai = int.Parse(dalys[5]);

                            Klausimas klausimas = new Klausimas(Tema, Sudetingumas, Autorius, KlausimoTekstas, TeisingasAtsakymas, Balai);
                            atstovybe.Klausimai.PridetiKlausima(klausimas);
                            atstovybe.VisiKlausimai.PridetiKlausima(klausimas);
                        }
                    }
                    atstovybes[atstovybiuKiekis++] = atstovybe;
                }
            }
        }

        /// <summary>
        /// Aktyviausio autoriaus klausimų skaičiaus gražinimo metodas
        /// </summary>
        /// <param name="atstovybes">Atsovybės</param>
        /// <param name="autoriaiBendr">Autoriai bendrai</param>
        /// <param name="atstovybiuKiekis">Atsovybių kiekis</param>
        /// <returns>Metodas gražina aktyviausio autoriaus klausimų kiekį</returns>
        public int DaugiausiaKlausimuIsViso(Atstovybe[] atstovybes, DaugiausiaKlausimuBendrai autoriaiBendr, int atstovybiuKiekis)
        {
            int klausimuSkaicius = 0;

            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                klausimuSkaicius = autoriaiBendr.DaugiausiaiAtstovybejPaieska(atstovybes[i], klausimuSkaicius);
            }
                       
            return klausimuSkaicius;
        }

        /// <summary>
        /// Išveda autorius, kurie uždavė daugiausiai klausimų
        /// </summary>
        /// <param name="atstovybes">Atsovybės</param>
        /// <param name="autoriaiBendr">Autoriai bendrai</param>
        /// <param name="atstovybiuKiekis">Atstovybių kiekis</param>
        public void DaugiausiaKlausimuIsVisoIsvedimas(Atstovybe[] atstovybes, DaugiausiaKlausimuBendrai autoriaiBendr, int atstovybiuKiekis)
        {
            int klausimuKiekis = DaugiausiaKlausimuIsViso(atstovybes, autoriaiBendr, atstovybiuKiekis);

            Console.WriteLine("Daugiausia klausimų sukūrė: ");

            for (int i = 0; i < autoriaiBendr.AutoriuKiekis; i++)
            {
                Console.WriteLine("{0} : {1}", autoriaiBendr.GautiAutoriu(i), klausimuKiekis);
            }

            Console.WriteLine();
        }
        

        /// <summary>
        /// Aktyviausio autoriaus klausimų kiekio metodas
        /// </summary>
        /// <param name="atstovybes">Atstovybės</param>
        /// <param name="autoriaiAts">Autoriai atskirai</param>
        /// <returns>metodas grąžina iš kiekvienos atstovybės aktyviausio autoriaus klausimų kiekį</returns>
        public int DaugiausiaiKlausimuAtstovybese(Atstovybe atstovybes, DaugiausiaKlausimuAtskirai autoriaiAts)
        {
            int klausimuskaicius = 0;
            int laikinasklausimuskaicius;
            string laikinasvardas = atstovybes.VisiKlausimai.GautiKlausima(0).Autorius;

            for (int i = 0; i < atstovybes.VisiKlausimai.Kiekis; i++)
            {
                laikinasvardas = atstovybes.VisiKlausimai.GautiKlausima(i).Autorius;
                laikinasklausimuskaicius = 1;

                for (int j = i + 1; j < atstovybes.VisiKlausimai.Kiekis; j++)
                {
                    if (atstovybes.VisiKlausimai.GautiKlausima(j).Autorius == laikinasvardas)
                        laikinasklausimuskaicius++;
                }

                if (laikinasklausimuskaicius > klausimuskaicius)
                {
                    klausimuskaicius = laikinasklausimuskaicius;

                    autoriaiAts.IstrintiAutorius();
                    autoriaiAts.PridetiAutoriu(laikinasvardas);
                }
                
                if(laikinasklausimuskaicius == klausimuskaicius && !autoriaiAts.Autoriai.Contains(laikinasvardas))
                {
                    autoriaiAts.PridetiAutoriu(laikinasvardas);
                }
            }

            return klausimuskaicius;
        }

        /// <summary>
        /// Autoriaus iš kiekvienos atstovybės, parašiusio daugiausia klausimų, vardo ir klausimų kiekio išvedimas
        /// </summary>
        /// <param name="atstovybes">Atsovybės</param>
        /// <param name="autoriaiAts">Autoriai atskirai</param>
        /// <param name="atstovybiuKiekis">Atstovybių kiekis</param>
        public void DaugiausiaiKlausimuAtstovybeseIsvedimas(Atstovybe[] atstovybes, DaugiausiaKlausimuAtskirai autoriaiAts, int atstovybiuKiekis)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                Console.WriteLine("Didziausias užduotų klausimų kiekis iš vieno žmogaus {0} atstovybėje yra {1}", atstovybes[i].AtstovybesPav,
                                    DaugiausiaiKlausimuAtstovybese(atstovybes[i], autoriaiAts));

                Console.WriteLine("Klausimus uždavė: ");
                Console.WriteLine(new string('-', 26));

                for (int j = 0; j < autoriaiAts.AutoriuKiekis; j++)
                {
                    Console.WriteLine("| {0, -22} |", autoriaiAts.GautiAutoriu(j));
                }

                Console.WriteLine(new string('-', 26));
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Metodas, surandantis kiek daugiausiai muzikinių klausimų buvo uždutoa
        /// </summary>
        /// <param name="atstovybes">Atstovybės</param>
        /// <param name="autoriaiAts">Autoriai atskirai</param>
        /// <returns></returns>
        public int DaugiausiaiMuzikiniuKlausimuAtstovybese(Atstovybe atstovybe, DaugiausiaKlausimuAtskirai autoriaiAts)
        {
            int klausimuskaicius = 0;
            int laikinasklausimuskaicius;
            if (atstovybe.MuzikiniaiKlausimai.Kiekis == 0)
            {
                klausimuskaicius = 0;
            }
            else
            {
                string laikinasvardas = atstovybe.MuzikiniaiKlausimai.GautiKlausima(0).Autorius;

                for (int i = 0; i < atstovybe.MuzikiniaiKlausimai.Kiekis; i++)
                {
                    laikinasvardas = atstovybe.MuzikiniaiKlausimai.GautiKlausima(i).Autorius;
                    laikinasklausimuskaicius = 1;

                    for (int j = i + 1; j < atstovybe.MuzikiniaiKlausimai.Kiekis; j++)
                    {
                        if (atstovybe.MuzikiniaiKlausimai.GautiKlausima(j).Autorius == laikinasvardas)
                            laikinasklausimuskaicius++;
                    }

                    if (laikinasklausimuskaicius > klausimuskaicius)
                    {
                        klausimuskaicius = laikinasklausimuskaicius;

                        autoriaiAts.IstrintiAutorius();
                        autoriaiAts.PridetiAutoriu(laikinasvardas);
                    }

                    if (laikinasklausimuskaicius == klausimuskaicius && !autoriaiAts.Autoriai.Contains(laikinasvardas))
                    {
                        autoriaiAts.PridetiAutoriu(laikinasvardas);
                    }
                }
            }

            return klausimuskaicius;
        }


        /// <summary>
        /// Autorių, kurie uždavė daugiausiai muzikinių klausimų, išvedimas
        /// </summary>
        /// <param name="atstovybes">Atstovybės</param>
        /// <param name="autoriaiAts">Autoriai atskirai</param>
        /// <param name="atstovybiuKiekis">Atsovybių kiekis</param>
        public void DaugiausiaiMuzikiniuKlausimuAtstovybeseIsvedimas(Atstovybe[] atstovybes, DaugiausiaKlausimuAtskirai autoriaiAts, int atstovybiuKiekis)
        {
            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                Console.WriteLine("Didziausias užduotų muzikinių klausimų kiekis iš vieno žmogaus {0} atstovybėje yra {1}", atstovybes[i].AtstovybesPav,
                                    DaugiausiaiMuzikiniuKlausimuAtstovybese(atstovybes[i], autoriaiAts));

                if (DaugiausiaiMuzikiniuKlausimuAtstovybese(atstovybes[i], autoriaiAts) == 0)
                {
                    Console.WriteLine("Šioje atstovybėje muzikinių klausimų nėra");
                }
                else
                {
                    Console.WriteLine("Klausimus uždavė: ");
                    Console.WriteLine(new string('-', 26));

                    for (int j = 0; j < autoriaiAts.AutoriuKiekis; j++)
                    {
                        Console.WriteLine("| {0, -22} |", autoriaiAts.GautiAutoriu(j));
                    }
                }
                

                Console.WriteLine(new string('-', 26));
                Console.WriteLine();

            }
        }

        /// <summary>
        /// Metodas, surandantis visus klausimus
        /// </summary>
        /// <param name="atstovybes">Atstovybės</param>
        /// <param name="atstovybiuKiekis">Atstovybių kiekis</param>
        /// <returns>Visų klausimų sąrašas</returns>
        KlausimuKonteineris VisiParasytiKlausimai(Atstovybe[] atstovybes, int atstovybiuKiekis)
        {
            KlausimuKonteineris visiParasytiKlausimai = new KlausimuKonteineris();
            int Count = 0;
            for(int i = 0; i < atstovybiuKiekis; i++)
            {
                for(int g = 0; g < atstovybes[i].VisiKlausimai.Kiekis;g++)
                {
                    for(int h = 0; h < visiParasytiKlausimai.Kiekis; h++)
                    {
                        if(visiParasytiKlausimai.GautiKlausima(h).Equals(atstovybes[i].VisiKlausimai.GautiKlausima(g)))
                        {
                            Count++;
                        }
                    }
                    if (Count == 0)
                    {
                        visiParasytiKlausimai.PridetiKlausima(atstovybes[i].VisiKlausimai.GautiKlausima(g));
                    }
                    Count = 0;
                }
            }
            return visiParasytiKlausimai;
        }
        
        /// <summary>
        /// Visų klausimų spausdinimas faile
        /// </summary>
        /// <param name="visiParasytiKlausimai">Visi klausimai</param>
        void VisuKlausimuSpausdinimasFaile(KlausimuKonteineris visiParasytiKlausimai)
        {
            using (StreamWriter rasyti = new StreamWriter(@"../../Klausimai.csv", false, Encoding.UTF8))
            {
                rasyti.WriteLine("Klausimas, Tema, Sudėtingumas");
                rasyti.WriteLine("");
                for (int i = 0; i < visiParasytiKlausimai.Kiekis; i++)
                {                 
                    rasyti.WriteLine("{0}, {1}, {2}",visiParasytiKlausimai.GautiKlausima(i).KlausimoTekstas, visiParasytiKlausimai.GautiKlausima(i).Tema, visiParasytiKlausimai.GautiKlausima(i).Sudetingumas);
                }
            }
        }
        
        /// <summary>
        /// Surandami visi istoriniai klausimai
        /// </summary>
        /// <param name="atstovybes">Atstovybės</param>
        /// <param name="atstovybiuKiekis">Atstovybių kiekis</param>
        /// <param name="skaicius">Istorinių klausimų kiekis</param>
        /// <returns>Istorinių klausimų sąrašas</returns>
        string[] IstoriniaiKlausimai(Atstovybe[] atstovybes, int atstovybiuKiekis, ref int skaicius)
        {
            string[] istoriniaiKlausimai = new string[1000];
            for(int i = 0; i < atstovybiuKiekis; i++)
            {
                for (int g = 0; g < atstovybes[i].VisiKlausimai.Kiekis; g++)
                {
                    if (atstovybes[i].VisiKlausimai.GautiKlausima(g).Tema == "Istorija")
                    {
                        if (!istoriniaiKlausimai.Contains(atstovybes[i].VisiKlausimai.GautiKlausima(g).KlausimoTekstas))
                        {
                            istoriniaiKlausimai[skaicius++] = atstovybes[i].VisiKlausimai.GautiKlausima(g).KlausimoTekstas;
                        }
                    }
                }
            }
            return istoriniaiKlausimai;
        }

        /// <summary>
        /// Spausdina istorinius klausimus faile
        /// </summary>
        /// <param name="istoriniai">Istoriniai klausimai</param>
        /// <param name="skaicius">Istorinių klausimų kiekis</param>
        void IstoriniuKlausimuSpausdinimasFaile(string[] istoriniai, int skaicius)
        {
            using (StreamWriter rasyti = new StreamWriter(@"../../Istoriniai.csv", false, Encoding.UTF8))
            {   
                rasyti.WriteLine("Klausimas");
                rasyti.WriteLine("");
                for (int i = 0; i < skaicius; i++)
                {
                    rasyti.WriteLine(istoriniai[i]);
                }
            }
        }

        /// <summary>
        /// Duomenų spausdinimas lentelėje
        /// </summary>
        /// <param name="atstovybes">Atstovybės</param>
        /// <param name="atstovybiuKiekis">Atstovybių kiekis</param>
        void DuomenuPateikimasLenteleje(Atstovybe[] atstovybes, int atstovybiuKiekis)
        {
            using (StreamWriter rasyti = new StreamWriter(@"../../Duomenųlentelė.txt"))
            {
                rasyti.WriteLine("Duomenys apie klausimus:");

                rasyti.WriteLine(new String('-', 383));
                rasyti.WriteLine("| {0, -24} | {1, 12} | {2, -23} | {3, -149} | {4, -90} | {5, -35} | {6} | {7, -20} |",
                                           "Tema", "Sudėtingumas", "Klausimo autorius", "Klausimo tekstas",
                                           "Atsakymo variantai", "Teisingas atsakymas", "Balai", "Failo Pavadinimas");
                rasyti.WriteLine(new String('-', 383));

                for (int i = 0; i < atstovybiuKiekis; i++)
                {
                    rasyti.WriteLine(atstovybes[i].AtstovybesPav);
                    rasyti.WriteLine(new String('-', 383));

                    for (int j = 0; j < atstovybes[i].VisiKlausimai.Kiekis; j++)
                    {
                        rasyti.WriteLine(atstovybes[i].VisiKlausimai.GautiKlausima(j));
                    }

                    rasyti.WriteLine(new String('-', 383));
                }
            }
        } 


    }
}
