using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    public class EingangsLager : Produktionseinrichtung{

        public EingangsLager(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten)
        {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }

        /*
        private bool InitialisiereTeil(string pfadZuCSV){

        }

        public Teil TeilAusgeben() {
            //Status zurückgeben
        }

        public void TeilZwischenlagern(Teil t) {

        }

        public Status BerechneStatus() {
            //Status zurückgeben
        }
        
         */

    }
}