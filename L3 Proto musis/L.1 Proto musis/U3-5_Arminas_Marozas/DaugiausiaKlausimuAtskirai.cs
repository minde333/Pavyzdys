using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    /// <summary>
    /// Klasėje saugomi autoriai, kiekvienoje atstovybėje parašę daugiausią klausimų
    /// </summary>
    class DaugiausiaKlausimuAtskirai
    {
        public const int maxKiekis = 1000;  //Didžiausias autorių kiekis
        public string [] Autoriai { get; private set; } //Autorių masyvas
        public int AutoriuKiekis { get; private set; } //Kintamasis, nurodantis autorių kiekį masyve

        /// <summary>
        /// Klasės konstruktorius
        /// </summary>
        public DaugiausiaKlausimuAtskirai()
        {
            Autoriai = new string[maxKiekis];
            AutoriuKiekis = 0;
        }
        
        /// <summary>
        /// Prideda autorių prie sąrašo
        /// </summary>
        /// <param name="autoriausVardas">Autoriaus vardas</param>
        public void PridetiAutoriu(string autoriausVardas)
        {
            Autoriai[AutoriuKiekis++] = autoriausVardas;
        }

        /// <summary>
        /// Paima autorių iš sąrašo
        /// </summary>
        /// <param name="indeksas">Konkreti vieta masyve</param>
        /// <returns></returns>
        public string GautiAutoriu(int indeksas)
        {
            return Autoriai[indeksas];
        }
        
        /// <summary>
        /// Ištrina visus autorius iš masyvo
        /// </summary>
        public void IstrintiAutorius()
        {
            for (int i = 0; i < AutoriuKiekis; i++)
            {
                Autoriai[i] = null;
            }

            AutoriuKiekis = 0;
        }
    }
}
