using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    /// <summary>
    /// Konteineris, į kurį dedame informaciją apie visus klausimus
    /// </summary>
    class KlausimuKonteineris
    {
        public const int maxKiekis = 1000; 
        public Klausimas[] Klausimai { get; private set; }
        public int Kiekis {get; private set;}
        
        public KlausimuKonteineris()
        {
            Klausimai = new Klausimas[maxKiekis];
            Kiekis = 0;
        }

        public void PridetiKlausima(Klausimas duomenys)
        {
            Klausimai[Kiekis++] = duomenys;
        }

        public Klausimas GautiKlausima(int indeksas)
        {
            return Klausimai[indeksas];
        }

        public void RikiuotiKlausimus()
        {
            Klausimas temp;
            for(int i = 0; i < Kiekis-1; i++)
            {
                for(int j = i + 1; j < Kiekis; j++)
                {
                    if(Klausimai[i].Tema.CompareTo(Klausimai[j].Tema) > 0 || Klausimai[i].Tema.CompareTo(Klausimai[j].Tema) == 0 && Klausimai[i].Sudetingumas > Klausimai[j].Sudetingumas)
                    {
                        temp = Klausimai[i];
                        Klausimai[i] = Klausimai[j];
                        Klausimai[j] = temp;
                    }
                }
            }
        }

       
    }
}
