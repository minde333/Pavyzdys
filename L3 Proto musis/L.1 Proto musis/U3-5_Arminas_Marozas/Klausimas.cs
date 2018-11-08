using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    /// <summary>
    /// Klasė, kurioje yra sukurti parametrai apibūdinantis klausimą be variantų
    /// </summary>
    class Klausimas
    {
        public string Tema { get; set; } //Klausimo tema
        public int Sudetingumas { get; set; } //Klausimo sudėtingumas
        public string Autorius { get; set; } //Klausimo autorius
        public string KlausimoTekstas { get; set; } //Klausimo tekstas
        public string TeisingasAtsakymas { get; set; } //Teisingas atsakymas į klausimą
        public int Balai { get; set; } //Gaunami balai

        /// <summary>
        /// Konstruktorius
        /// </summary>
        /// <param name="tema">Klausimo tema</param>
        /// <param name="sudetingumas">Klausimo sudėtingumas</param>
        /// <param name="autorius">Klausimo autorius</param>
        /// <param name="klausimoTekstas">Klausimas</param>
        /// <param name="teisingasAtsakymas">Teisingas klausimo atsakymas</param>
        /// <param name="balai">Balai skiriami už teisinga atsakyma į klausimą</param>
        public Klausimas(string tema, int sudetingumas, string autorius, string klausimoTekstas, string teisingasAtsakymas, int balai)
        {
            Tema = tema;
            Sudetingumas = sudetingumas;
            Autorius = autorius;
            KlausimoTekstas = klausimoTekstas;
            TeisingasAtsakymas = teisingasAtsakymas;
            Balai = balai;
        }
        
        /// <summary>
        /// Equals užklojimas
        /// </summary>
        /// <param name="kitas">Lyginamasis klausimas be variantų</param>
        /// <returns>Pakeistas palyginimas</returns>
        public bool Equals(Klausimas kitas)
        {
            return KlausimoTekstas == kitas.KlausimoTekstas;
        }

        /// <summary>
        /// ToString užklojimas
        /// </summary>
        /// <returns>Pakeistas šablonas</returns>
        public override string ToString()
        {
            return String.Format("| {0, -24} | {1, -12} | {2, -23} | {3, -149} | {4, -90} | {5, -35} | {6, -5} | {7, -20} |",
                                Tema, Sudetingumas, Autorius, KlausimoTekstas, "" , TeisingasAtsakymas, Balai, "");
        }
    }
}
