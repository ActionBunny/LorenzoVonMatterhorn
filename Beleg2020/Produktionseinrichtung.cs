using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    public abstract class Produktionseinrichtung
    {
        protected string _Name;
        protected List<Tuple<Verarbeitungsschritt, int>> _ListeAusfuehrbarerVerarbeitungsschritte;

        public abstract Status BerechneStatus();

        public string GetName() {
            return _Name;
        }

        public List<Tuple<Verarbeitungsschritt, int>> GetAusfuehrbareVerarbeitungsschritteUndDauer() {
            return _ListeAusfuehrbarerVerarbeitungsschritte;
        }

    }
}