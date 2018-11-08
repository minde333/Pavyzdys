using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_5_Mantas_Danauskas
{
    /// <summary>
    /// Klasėje saugoma informacija apie skirtingas temas
    /// </summary>
    class Temos
    {
        public int TemuKiekis { get; private set; }
        public string TemosPav { get; private set; }

        public Temos(string TemosPav, int TemuKiekis)
        {
            this.TemuKiekis = TemuKiekis;
            this.TemosPav = TemosPav;
        }
    }
}
