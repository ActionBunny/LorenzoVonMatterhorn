using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    public class AusgangsLager : Produktionseinrichtung
    {
        public AusgangsLager(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten)
        {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }
    
        /*
        private void GibHistorieAus(){
            
        }

        public void TeilFuerDenVersandEmpfangen(Teil t){

        }

        public Status BerechneStatus(){
            //Status zurückgeben
        }*/
    }
}