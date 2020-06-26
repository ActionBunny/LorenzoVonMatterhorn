using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    public class Fertigungsinsel : Produktionseinrichtung
    {
        private DateTime _BelegtBis;

        public Fertigungsinsel(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten)
        {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }
        /*
        public void TeilEntgegennehmen(Teil t){

        }
        
        public Teil TeilZurueckgeben(){

        }

        public int GetBearbeitungsdauerFuerSchritt(Verarbeitungsschritt schritt){

        }
        public Status BerechneStatus(){
            //Status zurückgeben
        }*/
    }
}