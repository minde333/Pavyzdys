using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    /// <summary>
    /// klasė, kurioje yra sukurti parametrai apibūdinantis klausimą
    /// </summary>
    class Klausimas
    {
        public string Tema { get; set; }
        public int Sudėtingumas { get; set; }
        public string Autorius { get; set; }
        public string KlausimoTekstas { get; set; }
        public string AtsakymoVariantai { get; set; }
        public string TeisingasAtsakymas { get; set; }
        public int Balai { get; set; }

        /// <summary>
        /// Konstruktorius
        /// </summary>
        /// <param name="tema">Klausimo tema</param>
        /// <param name="sudėtingumas">Klausimo sudėtingumas</param>
        /// <param name="autorius">Klausimo autorius</param>
        /// <param name="klausimoTekstas">Klausimas</param>
        /// <param name="atsakymoVariantai">Galimi klausimo atsakymai</param>
        /// <param name="teisingasAtsakymas">Teisingas klausimo atsakymas</param>
        /// <param name="balai">Balai skiriami už teisinga atsakyma į klausimą</param>
        public Klausimas(string tema, int sudėtingumas, string autorius, string klausimoTekstas, string atsakymoVariantai, string teisingasAtsakymas, int balai)
        {
            Tema = tema;
            Sudėtingumas = sudėtingumas;
            Autorius = autorius;
            KlausimoTekstas = klausimoTekstas;
            AtsakymoVariantai = atsakymoVariantai;
            TeisingasAtsakymas = teisingasAtsakymas;
            Balai = balai;
        }
        
        //Equals užklojimas
        public bool Equals(Klausimas kitas)
        {
            return KlausimoTekstas == kitas.KlausimoTekstas;
        }

        public override string ToString()
        {
            return String.Format("| {0, -22} | {1, -12} | {2, -23} | {3, -150} | {4, -90} | {5, -35} | {6, -6} |",
                                Tema, Sudėtingumas, Autorius, KlausimoTekstas, AtsakymoVariantai, TeisingasAtsakymas, Balai);
        }
    }
}
