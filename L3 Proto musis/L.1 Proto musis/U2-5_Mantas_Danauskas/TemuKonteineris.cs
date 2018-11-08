using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    /// <summary>
    /// Konteineris, į kurį dedame informaciją apie visas skirtingas temas
    /// </summary>
    class TemuKonteineris
    {
        public Temos[] TemuInformacija { get; private set; }
        public int temuKiekis { get; private set; }

        public TemuKonteineris(int maxKiekis)
        {
            TemuInformacija = new Temos[maxKiekis];
            temuKiekis = 0;
        }

        public void PridetiTema(Temos tema)
        {
            TemuInformacija[temuKiekis++] = tema;
        }

        public Temos GautiTema(int indeksas)
        {
            return TemuInformacija[indeksas];
        }

        public bool ArYra(string pav)
        {
            for (int i = 0; i < temuKiekis; i++)
            {
                if (pav == TemuInformacija[i].TemosPav)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// metode sukūriamas objektas ir į jį įdedama informacija apie nesikartojančias temas
        /// </summary>
        /// <param name="AtstovybiuPav"></param>
        /// <param name="temos"></param>
        /// <param name="laikinasPav"></param>
        /// <param name="temosIndeksas"></param>
        /// <param name="atstovybesIndeksas"></param>
        /// <param name="atstovybiuKiekis"></param>
        public void TemuKiekiuPaieska(Atstovybes[] AtstovybiuPav, TemuKonteineris temos, string laikinasPav, int temosIndeksas, int atstovybesIndeksas, int atstovybiuKiekis)
        {
            int laikinasKiekis = 0;
            
            for (int i = atstovybesIndeksas; i < atstovybiuKiekis; i++)
            {
                if (atstovybesIndeksas != i)
                    temosIndeksas = 0;

                for (int j = temosIndeksas; j < AtstovybiuPav[i].Klausimai.kiekKlausimu; j++)
                {
                    if (laikinasPav == AtstovybiuPav[i].Klausimai.GautiKlausima(j).Tema)
                    {
                        laikinasKiekis++;
                    } 
                }
            }
            
            if (!ArYra(laikinasPav))
            {
                Temos t = new Temos(laikinasPav, laikinasKiekis);
                temos.PridetiTema(t);
            }
        }
    }
}
