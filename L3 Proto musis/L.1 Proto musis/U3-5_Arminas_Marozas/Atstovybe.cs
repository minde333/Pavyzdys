using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    /// <summary>
    /// Klasėje saugoma informacija apie atstovybę
    /// </summary>
    class Atstovybe
    {
        public string AtstovybesPav { get; private set; }  //Atstovybės pavadinimas
        public KlausimuKonteineris Klausimai { get; private set; } //Klausimų be variantų sąrašas
        public KlausimuKonteineris KlausimaiSuVariantais { get; set; } //Klausimų su variantais sąrašas
        public KlausimuKonteineris MuzikiniaiKlausimai { get; set; }  //Muzikinių klausimų sąrašas
        public KlausimuKonteineris VisiKlausimai { get; set; } //Visų bendrai klausimų sąrašas

        /// <summary>
        /// Tuščias atstovybės konstruktorius
        /// </summary>
        public Atstovybe()
        {
            
        }
        
        /// <summary>
        /// Atstovybės konstruktorius
        /// </summary>
        /// <param name="atstovybe">Atstovybės pavadinimas</param>
        public Atstovybe(string atstovybe)
        {
            AtstovybesPav = atstovybe;
            Klausimai = new KlausimuKonteineris();
            KlausimaiSuVariantais = new KlausimuKonteineris();
            MuzikiniaiKlausimai = new KlausimuKonteineris();
            VisiKlausimai = new KlausimuKonteineris();
        }

    }
}
