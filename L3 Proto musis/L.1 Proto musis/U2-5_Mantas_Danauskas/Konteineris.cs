using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    /// <summary>
    /// Konteineris, į kurį dedame informaciją apie visus klausimus
    /// </summary>
    class Konteineris
    {
        public Klausimas[] Klausimai { get; private set; }
        public int kiekKlausimu {get; private set;}
        
        public Konteineris(int maxKiekis)
        {
            Klausimai = new Klausimas[maxKiekis];
            kiekKlausimu = 0;
        }

        public void PridetiKlausima(Klausimas duomenys)
        {
            Klausimai[kiekKlausimu++] = duomenys;
        }

        public Klausimas GautiKlausima(int indeksas)
        {
            return Klausimai[indeksas];
        }

        /// <summary>
        /// Funkcija grąžina sutampančio klausimo tekstą
        /// </summary>
        /// <param name="kitas"></param>
        /// <param name="vienodiKlaus"></param>
        /// <param name="kiekis"></param>
        /// <returns></returns>
        public string[] VienodiKlausimai(Atstovybes kitas,ref string[] vienodiKlaus, ref int kiekis)
        {
            for (int i = 0; i < kiekKlausimu; i++)
            {
                for (int j = 0; j < kitas.Klausimai.kiekKlausimu; j++)
                {
                    if (GautiKlausima(i).Equals(kitas.Klausimai.GautiKlausima(j)) && !vienodiKlaus.Contains(GautiKlausima(i).KlausimoTekstas))
                        vienodiKlaus[kiekis++] = GautiKlausima(i).KlausimoTekstas;
                }
            }

            return vienodiKlaus;
        }
    }
}
