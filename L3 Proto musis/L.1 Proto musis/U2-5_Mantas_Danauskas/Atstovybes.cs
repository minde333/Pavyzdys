using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    /// <summary>
    /// Klasėje saugoma informacija apie atstovybę
    /// </summary>
    class Atstovybes
    {
        public string atstovybesPav { get; private set; }
        public Konteineris Klausimai { get; private set; }

        public Atstovybes()
        {
            
        }

        public Atstovybes(string atstovybe, int maxKiekis)
        {
            atstovybesPav = atstovybe;
            Klausimai = new Konteineris(maxKiekis);
        }
    }
}
