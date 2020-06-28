using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Beleg2020
{
    public class Fertigungsinsel : Produktionseinrichtung
    {
        private DateTime _BelegtBis;
        private Teil _aktuellesTeil;
        private Status _status;

        public Fertigungsinsel(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten)
        {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }

        public override Status BerechneStatus()
        {
            if (_aktuellesTeil == null)
            {
                return Status.EMPFANGSBEREIT;
            }
            else
            {
                if (DateTime.Now > _BelegtBis)
                {
                    return Status.ABHOLBEREIT;
                }
                else
                {
                    return Status.BELEGT;
                }
            }
        }

        public void TeilEntgegennehmen(Teil t)
        {
            BerechneStatus();
            if (_status == Status.EMPFANGSBEREIT)
            {
                _aktuellesTeil = t;
                // setze belegt bis
                Verarbeitungsschritt naechsterSchritt = xxx _aktuellesTeil.GetNaechsterSchritt();
                int dauer = GetBearbeitungsdauerFuerSchritt(naechsterSchritt);
                _BelegtBis = DateTime.Now.AddSeconds(dauer);
            }
            else
            {
                Console.WriteLine("!! Fertigungsinsel belegt. Teil kann nicht angenommen werden.");
            }
        }

        public Teil TeilZurueckgeben()
        {
            BerechneStatus();
            if (_status == Status.ABHOLBEREIT)
            {
                Teil tempTeil = _aktuellesTeil;
                _aktuellesTeil = null;
                Console.WriteLine("Teil zurueck gegeben. Seriennummer: " + tempTeil.GetSeriennummer());
                return tempTeil;
            }
            else
            {
                Console.WriteLine("!! Teil nicht abholbereit. Teil kann nicht zurueck gegeben werden.");
                return null;
            }
        }
        
        public int GetBearbeitungsdauerFuerSchritt(Verarbeitungsschritt schritt) {
            int dauer = 0;
            foreach (Tuple<Verarbeitungsschritt, int> tuple in _ListeAusfuehrbarerVerarbeitungsschritte)
            {
                if (tuple.Item1 == schritt)
                {
                    dauer = tuple.Item2;
                }
            }
            return dauer;
        }
    }
}