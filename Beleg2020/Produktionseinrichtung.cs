using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    class Produktionseinrichtung
    {
        private String _Name;
        private List<Tuple<Verarbeitungsschritt, int>> _ListeAusfuehrbarerVerarbeitungsschritte;

        public String GetName(){
            return _Name;
        }

        public List<Tuple<Verarbeitungsschritt, int>> GetAusfuehrbareVerarbeitungsschritteUndDauer(){
            return _ListeAusfuehrbarerVerarbeitungsschritte;
        }
        public Status BerechneStatus(){
            //Status zurückgeben
        }
    }
}