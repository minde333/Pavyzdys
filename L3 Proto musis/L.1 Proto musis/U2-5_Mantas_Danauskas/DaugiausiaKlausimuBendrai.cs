using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    /// <summary>
    /// Klasėje saugomas autorius, parašęs daugiausia klausimų
    /// </summary>
    class DaugiausiaKlausimuBendrai
    {
        public string[] Autoriai { get; private set; }
        public int autoriuKiekis { get; private set; }

        public DaugiausiaKlausimuBendrai(int maxKiekis)
        {
            Autoriai = new string[maxKiekis];
            autoriuKiekis = 0;
        }

        public void PridetiAutoriu(string vardas)
        {
            Autoriai[autoriuKiekis++] = vardas;
        }

        public string GautiAutoriu(int indeksas)
        {
            return Autoriai[indeksas];
        }

        public void IstrintiAutorius()
        {
            for (int i = 0; i < autoriuKiekis; i++)
            {
                Autoriai[i] = null;
            }

            autoriuKiekis = 0;
        }

        /// <summary>
        /// metodas grąžina didžiausią klausimų skaičių iš visų atsovybių
        /// </summary>
        /// <param name="atstovybe"></param>
        /// <param name="klausimuSkaicius"></param>
        /// <returns></returns>
        public int DaugiausiaiAtstovybejPaieska(Atstovybes atstovybe, int klausimuSkaicius)
        {
            int laikinasKlausimuSkaicius;
            string vardas;

            for (int i = 0; i < atstovybe.Klausimai.kiekKlausimu; i++)
            {
                laikinasKlausimuSkaicius = 1;
                vardas = atstovybe.Klausimai.GautiKlausima(i).Autorius;

                for (int j = i + 1; j < atstovybe.Klausimai.kiekKlausimu; j++)
                {
                    if (vardas == atstovybe.Klausimai.GautiKlausima(j).Autorius)
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
