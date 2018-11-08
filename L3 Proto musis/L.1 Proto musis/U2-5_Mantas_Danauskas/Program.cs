//Mantas Danauskas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    class Program
    {
        const int maxKlausimuKiekis = 100;  //didžiausias duomenų kiekis
        const int maxAtstovybiuKiekis = 10; //didžiausias atstovybių kiekis
        
        static void Main(string[] args)
        {
            Program p = new Program();
            Atstovybes[] AtstovybiuPav = new Atstovybes[maxAtstovybiuKiekis];   //objekto masyve saugomi atstovybių pavadinimai
            int atstovybiuKiekis = 0;

            DaugiausiaKlausimuBendrai autoriaiBendr = new DaugiausiaKlausimuBendrai(maxKlausimuKiekis); //Iš visų atstovybių popuiariausių autorių objektas
            DaugiausiaKlausimuAtskirai autoriaiAts = new DaugiausiaKlausimuAtskirai(maxKlausimuKiekis); //Skirtingų atstovybių populiariausių autorių objektas
            TemuKonteineris temos = new TemuKonteineris(maxKlausimuKiekis); //Nesikartojančių temų objektas

            p.Skaitymas(ref AtstovybiuPav, maxKlausimuKiekis, ref atstovybiuKiekis);//Skaitymo metodas
            p.TemuKiekis(AtstovybiuPav, temos, atstovybiuKiekis);   //metode kaupiamas nesikartojančių temų pavadinimas ir kiekis
            p.DaugiausiaKlausimuIsVisoIsvedimas(AtstovybiuPav, autoriaiBendr, atstovybiuKiekis);//populiariausio autoriaus apskritai išvedimo metodas
            p.DaugiausiaiKlausimuAtstovybeseIsvedimas(AtstovybiuPav, autoriaiAts, atstovybiuKiekis);//populiariausio autoriaus kiekvienoje atstovybėje paieškos metodas
            p.SkirtinguTemuIsvedimas(temos);//nesikartojančių temų ir jų kiekių išvedimas
            p.VienoduKlausimuIsvedimas(AtstovybiuPav, atstovybiuKiekis);//pasikartojančių klausimų keliose atstovybėse išvedimas
            p.DuomenuPateikimasLenteleje(AtstovybiuPav, atstovybiuKiekis);  //duomenų lentelės .txt kūrimo metodas
        }

        /// <summary>
        /// metodas skirtas nuskaityti duomenis
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="maxKlausimuKiekis"></param>
        /// <param name="atstovybiuKiekis"></param>
        public void Skaitymas(ref Atstovybes[] AtstovybiuPav, int maxKlausimuKiekis, ref int atstovybiuKiekis)
        {
            string[] DuomenuVieta = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            foreach(string failas in DuomenuVieta)
            {
                using(StreamReader skaityti = new StreamReader(@failas, Encoding.GetEncoding(1257)))
                {
                   string eilute = skaityti.ReadLine();
                   Atstovybes atstovybes = new Atstovybes(eilute, maxKlausimuKiekis);

                    while((eilute = skaityti.ReadLine()) != null)
                    {
                        string[] dalys = eilute.Split(';');

                        string Tema = dalys[0];
                        int Sudėtingumas = int.Parse(dalys[1]);
                        string Autorius = dalys[2];
                        string KlausimoTekstas = dalys[3];
                        string AtsakymoVariantai = dalys[4];
                        string TeisingasAtsakymas = dalys[5];
                        int Balai = int.Parse(dalys[6]);

                        Klausimas klausimas = new Klausimas(Tema, Sudėtingumas, Autorius, KlausimoTekstas, AtsakymoVariantai, TeisingasAtsakymas, Balai);
                        atstovybes.Klausimai.PridetiKlausima(klausimas);
                    }
                    AtstovybiuPav[atstovybiuKiekis++] = atstovybes;
                }     
            }
        }

        /// <summary>
        /// aktyviausio autoriaus klausimų skaičiaus gražinimo metodas
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="autoriaiBendr"></param>
        /// <param name="atstovybiuKiekis"></param>
        /// <returns>metodas gražina aktyviausio autoriaus klausimų kiekį</returns>
        public int DaugiausiaKlausimuIsViso(Atstovybes[] AtstovybiuPav, DaugiausiaKlausimuBendrai autoriaiBendr, int atstovybiuKiekis)
        {
            int klausimuSkaicius = 0;

            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                klausimuSkaicius = autoriaiBendr.DaugiausiaiAtstovybejPaieska(AtstovybiuPav[i], klausimuSkaicius);
            }
                       
            return klausimuSkaicius;
        }

        /// <summary>
        /// aktyviausio autoriaus klausimų kiekio metodas
        /// </summary>
        /// <param name="klausimas"></param>
        /// <param name="DaugiausiaiKlausimuAtstovybese"></param>
        /// <returns>metodas grąžina iš kiekvienos atstovybės aktyviausio autoriaus klausimų kiekį</returns>
        public int DaugiausiaiKlausimuAtstovybese(Atstovybes AtstovybiuPav, DaugiausiaKlausimuAtskirai autoriaiAts)
        {
            int klausimuskaicius = 0;
            int laikinasklausimuskaicius;
            string laikinasvardas = AtstovybiuPav.Klausimai.GautiKlausima(0).Autorius;

            for (int i = 0; i < AtstovybiuPav.Klausimai.kiekKlausimu; i++)
            {
                laikinasvardas = AtstovybiuPav.Klausimai.GautiKlausima(i).Autorius;
                laikinasklausimuskaicius = 1;

                for (int j = i + 1; j < AtstovybiuPav.Klausimai.kiekKlausimu; j++)
                {
                    if (AtstovybiuPav.Klausimai.GautiKlausima(j).Autorius == laikinasvardas)
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
        /// metodas skirtas atrinkti visas temas, jog nesikartotų ir surasti jų kiekius
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="temos"></param>
        /// <param name="atstovybiuKiekis"></param>
        public void TemuKiekis(Atstovybes[] AtstovybiuPav, TemuKonteineris temos, int atstovybiuKiekis)
        {
            int temosIndeksas = 0;
            int atstovybesIndeksas = 0;
            string laikinasPav = AtstovybiuPav[atstovybesIndeksas].Klausimai.GautiKlausima(temosIndeksas).Tema;

            temos.TemuKiekiuPaieska(AtstovybiuPav, temos, laikinasPav, temosIndeksas, atstovybesIndeksas, atstovybiuKiekis);

            while (temosIndeksas != AtstovybiuPav[atstovybiuKiekis - 1].Klausimai.kiekKlausimu - 1)
            {
                while(temosIndeksas != AtstovybiuPav[atstovybesIndeksas].Klausimai.kiekKlausimu - 1)
                {
                    temosIndeksas++;
                    laikinasPav = AtstovybiuPav[atstovybesIndeksas].Klausimai.GautiKlausima(temosIndeksas).Tema;
                    temos.TemuKiekiuPaieska(AtstovybiuPav, temos, laikinasPav, temosIndeksas, atstovybesIndeksas, atstovybiuKiekis);
                    
                }

                if (atstovybesIndeksas + 1 != atstovybiuKiekis)
                {
                    temosIndeksas = 0;
                    atstovybesIndeksas++;
                    temos.TemuKiekiuPaieska(AtstovybiuPav, temos, laikinasPav, temosIndeksas, atstovybesIndeksas, atstovybiuKiekis);
                }
            }
        }

        /// <summary>
        /// metode gaunamas klausimas, pasikartojęs keliose atstovybėse
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="atstovybiuKiekis"></param>
        /// <returns>pasikartojantį klausimą</returns>
        public string[] VienodiKlausimai(Atstovybes[] AtstovybiuPav, int atstovybiuKiekis)
        {
            string[] klausimas = new string[maxKlausimuKiekis];
            int kiekis = 0;

            for (int i = 0; i < atstovybiuKiekis; i++)
            {
                for (int j = i + 1; j < atstovybiuKiekis; j++)
                {
                    klausimas = AtstovybiuPav[i].Klausimai.VienodiKlausimai(AtstovybiuPav[j], ref klausimas, ref kiekis);
                }
            }

            return klausimas;
        }

        /// <summary>
        /// autoriaus, parašiusio daugiausia klausimų, vardo ir klausimų kiekio išvedimas
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="autoriaiBendr"></param>
        /// <param name="atstovybiuKiekis"></param>
        public void DaugiausiaKlausimuIsVisoIsvedimas(Atstovybes[] AtstovybiuPav, DaugiausiaKlausimuBendrai autoriaiBendr, int atstovybiuKiekis)
        {
            int klausimuKiekis = DaugiausiaKlausimuIsViso(AtstovybiuPav, autoriaiBendr, atstovybiuKiekis);

            Console.WriteLine("Daugiausia klausimų sukūrė: ");

            for (int i = 0; i < autoriaiBendr.autoriuKiekis; i++)
            {
                Console.WriteLine("{0} : {1}", autoriaiBendr.GautiAutoriu(i), klausimuKiekis);
            }

            Console.WriteLine();
        }

        /// <summary>
        ///  autoriaus iš kiekvienos atstovybės, parašiusio daugiausia klausimų, vardo ir klausimų kiekio išvedimas
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="autoriaiAts"></param>
        /// <param name="atstovybiuKiekis"></param>
        public void DaugiausiaiKlausimuAtstovybeseIsvedimas(Atstovybes[] AtstovybiuPav, DaugiausiaKlausimuAtskirai autoriaiAts, int atstovybiuKiekis)
        {
            for(int i = 0; i < atstovybiuKiekis; i++)
            {
                Console.WriteLine("Didziausias užduotų klausimų kiekis iš vieno žmogaus {0} atstovybėje yra {1}", AtstovybiuPav[i].atstovybesPav,
                                    DaugiausiaiKlausimuAtstovybese(AtstovybiuPav[i], autoriaiAts));

                Console.WriteLine("Klausimus uždavė: ");
                Console.WriteLine(new string('-', 26));

                for (int j = 0; j < autoriaiAts.autoriuKiekis; j++)
                {
                    Console.WriteLine("| {0, -22} |", autoriaiAts.GautiAutoriu(j));
                }

                Console.WriteLine(new string('-', 26));
                Console.WriteLine();
            }
        }
        
       /// <summary>
       /// nesikartojančių temų ir jų kiekių išvedimas
       /// </summary>
       /// <param name="temos"></param>
        public void SkirtinguTemuIsvedimas(TemuKonteineris temos)
        {
            string failoVardas = @"C:\Users\Armis\Desktop\L.1 Proto musis\U2-5_Mantas_Danauskas/TemųSkaičius.csv";

            using (StreamWriter rasyti = new StreamWriter(failoVardas))
            {
                for (int i = 0; i < temos.temuKiekis; i++)
                {
                    rasyti.WriteLine("{0};{1}", temos.GautiTema(i).TemosPav, temos.GautiTema(i).TemuKiekis);
                }
            }
        }

        /// <summary>
        /// klausimų, pasikartojusių keliose atstovybėse, išvedimas
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="atstovybiuKiekis"></param>
        public void VienoduKlausimuIsvedimas(Atstovybes[] AtstovybiuPav, int atstovybiuKiekis)
        {
            string [] VienoduKlausimuSarasas = VienodiKlausimai(AtstovybiuPav, atstovybiuKiekis);
            string failoVardas = @"C:\Users\Armis\Desktop\L.1 Proto musis\U2-5_Mantas_Danauskas/VienodiKlausimai.csv";
            int i = 0;

            using (StreamWriter irasyti = new StreamWriter(failoVardas))
            {
                if (VienoduKlausimuSarasas[0] == null)
                    irasyti.WriteLine("Pasikartojančių klausimų nėra");
                else
                    while (VienoduKlausimuSarasas[i] != null)
                    {
                        irasyti.WriteLine(VienoduKlausimuSarasas[i]);
                        i++;
                    }
            }
        }

        /// <summary>
        /// Sukuriama duomenų lentelė, kuri yra išsaugojama .txt tipu
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="atstovybiuKiekis"></param>
        void DuomenuPateikimasLenteleje(Atstovybes[] AtstovybiuPav, int atstovybiuKiekis)
        {
            using (StreamWriter failopavadinimas = new StreamWriter("Duomenųlentelė.txt"))
            {
                failopavadinimas.WriteLine("Duomenys apie klausimus:");

                failopavadinimas.WriteLine(new String('-', 360));
                failopavadinimas.WriteLine("| {0, -22} | {1, 12} | {2, -23} | {3, -150} | {4, -90} | {5, -35} | {6, 6} |",
                                           "Tema", "Sudėtingumas", "Klausimo autorius", "Klausimo tekstas",
                                           "Atsakymo variantai", "Teisingas atsakymas", "Balai");
                failopavadinimas.WriteLine(new String('-', 360));

                for (int i = 0; i < atstovybiuKiekis; i++)
                {
                    failopavadinimas.WriteLine(AtstovybiuPav[i].atstovybesPav);
                    failopavadinimas.WriteLine(new String('-', 360));

                    for (int j = 0; j < AtstovybiuPav[i].Klausimai.kiekKlausimu; j++)
                    {
                        failopavadinimas.WriteLine(AtstovybiuPav[i].Klausimai.GautiKlausima(j));
                    }

                    failopavadinimas.WriteLine(new String('-', 360));
                }
            }
        }
    }
}
