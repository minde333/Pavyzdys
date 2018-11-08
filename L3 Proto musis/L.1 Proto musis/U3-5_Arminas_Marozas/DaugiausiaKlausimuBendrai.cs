using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    /// <summary>
    /// Klasėje saugomas autorius, parašęs daugiausia klausimų
    /// </summary>
    class DaugiausiaKlausimuBendrai
    {
        public const int maxKiekis = 1000; //Didžiausias autorių skaičius
        public string[] Autoriai { get; private set; } //Autorių masyvas
        public int AutoriuKiekis { get; private set; } //Kintamasis, nurodantis, kiek yra autorių masyve

        /// <summary>
        /// Klasės konstruktorius
        /// </summary>
        public DaugiausiaKlausimuBendrai()
        {
            Autoriai = new string[maxKiekis];
            AutoriuKiekis = 0;
        }
        
        /// <summary>
        /// Prideda autorių į masyvą
        /// </summary>
        /// <param name="vardas">Autoriaus vardas</param>
        public void PridetiAutoriu(string vardas)
        {
            Autoriai[AutoriuKiekis++] = vardas;
        }

        /// <summary>
        /// Paima autorių iš masyvo
        /// </summary>
        /// <param name="indeksas">Konkreti autoriaus vieta masyve</param>
        /// <returns></returns>
        public string GautiAutoriu(int indeksas)
        {
            return Autoriai[indeksas];
        }

        /// <summary>
        /// Ištrina autorius iš sąrašo
        /// </summary>
        public void IstrintiAutorius()
        {
            for (int i = 0; i < AutoriuKiekis; i++)
            {
                Autoriai[i] = null;
            }

            AutoriuKiekis = 0;
        }

        /// <summary>
        /// metodas grąžina didžiausią klausimų skaičių iš visų atstovybių
        /// </summary>
        /// <param name="atstovybe">Atsovybė</param>
        /// <param name="klausimuSkaicius">Klausimų skaičius</param>
        /// <returns></returns>
        public int DaugiausiaiAtstovybejPaieska(Atstovybe atstovybe, int klausimuSkaicius)
        {
            int laikinasKlausimuSkaicius;
            string vardas;

            for (int i = 0; i < atstovybe.VisiKlausimai.Kiekis; i++)
            {
                laikinasKlausimuSkaicius = 1;
                vardas = atstovybe.VisiKlausimai.GautiKlausima(i).Autorius;

                for (int j = i + 1; j < atstovybe.VisiKlausimai.Kiekis; j++)
                {
                    if (vardas == atstovybe.VisiKlausimai.GautiKlausima(j).Autorius)
                        laikinasKlausimuSkaicius++;
                }

                if(laikinasKlausimuSkaicius > klausimuSkaicius)
                {
                    klausimuSkaicius = laikinasKlausimuSkaicius;
                    IstrintiAutorius();
                    PridetiAutoriu(vardas);
                }
                else if(laikinasKlausimuSkaicius == klausimuSkaicius && !Autoriai.Contains(vardas))
                {
                    PridetiAutoriu(vardas);
                }
            }

            return klausimuSkaicius;
        }

      


    }
}
