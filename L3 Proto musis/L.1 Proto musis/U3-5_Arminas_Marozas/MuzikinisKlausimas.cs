using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3_5_Arminas_Marozas
{
    /// <summary>
    /// Klasė, kuri paveldi Klausimas klasė, ir yra skirta muzikiniam klausimui apibūdinti
    /// </summary>
    class MuzikinisKlausimas : Klausimas
    {   
        public string FailoPavadinimas { get; set; } //Failo pavadinimas

        /// <summary>
        /// Muzikinio klausimo konstruktorius
        /// </summary>
        /// <param name="tema">Tema</param>
        /// <param name="sudetingumas">Sudėtingumas</param>
        /// <param name="autorius">Autorius</param>
        /// <param name="klausimoTekstas">Klausimo tekstas</param>
        /// <param name="teisingasAtsakymas">Teisingas atsakymas</param>
        /// <param name="balai">Gaunami balai</param>
        /// <param name="failoPavadinimas">Failo pavadinimas</param>
        public MuzikinisKlausimas(string tema, int sudetingumas, string autorius, string klausimoTekstas, string teisingasAtsakymas, int balai, string failoPavadinimas) : base(
                tema, sudetingumas, autorius, klausimoTekstas, teisingasAtsakymas, balai)
        {
            FailoPavadinimas = failoPavadinimas;
        }

        /// <summary>
        /// Užklojamas Equals metodas
        /// </summary>
        /// <param name="kitas">Lyginamasis muzikinis klausimas</param>
        /// <returns>Pakeistas palyginimas</returns>
        public bool Equals(MuzikinisKlausimas kitas)
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
                                Tema, Sudetingumas, Autorius, KlausimoTekstas, "", TeisingasAtsakymas, Balai, FailoPavadinimas);
        }

    }
}
