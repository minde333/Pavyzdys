using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L._1_Protų_mūšis
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
    }
}
