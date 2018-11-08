using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    /// <summary>
    /// Klasėje saugomi autoriai, kiekvienoje atstovybėje parašę daugiausią klausimų
    /// </summary>
    class DaugiausiaKlausimuAtskirai
    {
        public string [] Autoriai { get; private set; }
        public int autoriuKiekis { get; private set; }

        public DaugiausiaKlausimuAtskirai(int maxKiekis)
        {
            Autoriai = new string[maxKiekis];
            autoriuKiekis = 0;
        }
        
        public void PridetiAutoriu(string autoriausVardas)
        {
            Autoriai[autoriuKiekis++] = autoriausVardas;
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
    }
}
