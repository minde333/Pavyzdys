using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    /// <summary>
    /// Klasė, kuri paveldi Klausimas klasę, ir yra skirta klausimui su variantais apibūdinti
    /// </summary>
    class KlausimasSuVariantais : Klausimas
    {
        public string AtsakymoVariantai { get; set; } //Atsakymo variantai
        
        /// <summary>
        /// KlausimoSuVariantais konstruktorius
        /// </summary>
        /// <param name="tema">Tema</param>
        /// <param name="sudetingumas">Sudėtingumas</param>
        /// <param name="autorius">Autorius</param>
        /// <param name="klausimoTekstas">Klausimo Tekstas</param>
        /// <param name="atsakymoVariantai">Atsakymo variantai</param>
        /// <param name="teisingasAtsakymas">Teisingas atsakymas</param>
        /// <param name="balai"></param>
        public KlausimasSuVariantais(string tema, int sudetingumas, string autorius, string klausimoTekstas, string atsakymoVariantai, string teisingasAtsakymas, int balai) : base(
                tema, sudetingumas, autorius, klausimoTekstas, teisingasAtsakymas, balai)
        {
            AtsakymoVariantai = atsakymoVariantai;
        }
        
        /// <summary>
        /// Equals užklojimas
        /// </summary>
        /// <param name="kitas">Lyginamasis klausimas su variantais</param>
        /// <returns>Pakeistas objektų palyginimas</returns>
        public bool Equals(KlausimasSuVariantais kitas)
        {
            return KlausimoTekstas == kitas.KlausimoTekstas;
        }

        /// <summary>
        /// Užklojamas ToString
        /// </summary>
        /// <returns>Pakeistas šablonas</returns>
        public override string ToString()
        {
            return String.Format("| {0, -24} | {1, -12} | {2, -23} | {3, -149} | {4, -90} | {5, -35} | {6, -5} | {7, -20} |",
                                Tema, Sudetingumas, Autorius, KlausimoTekstas, AtsakymoVariantai, TeisingasAtsakymas, Balai, "");
        }
    }
}
