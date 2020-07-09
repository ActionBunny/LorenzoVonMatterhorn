using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    public class AusgangsLager : Produktionseinrichtung
    {
        private List<Teil> _Bestand = new List<Teil>();

        public AusgangsLager(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten) {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }

        public override Status BerechneStatus() {
            return Status.EMPFANGSBEREIT;
        }

        private void GibHistorieAus(Teil t) {
            string serienNummer = t.GetSeriennummer();
            
            Console.Write("LagerAus: "+serienNummer + " ");
            foreach (Tuple<Verarbeitungsschritt, string> historienEintrag in t.LiefereHistorie()) {
                Console.Write("( " + historienEintrag.Item1 + " - " + historienEintrag.Item2 + " );");
            }
            Console.Write("\n");
        }

        public void TeilFuerDenVersandEmpfangen(Teil t) {
            _Bestand.Add(t);
            GibHistorieAus(t);
        }

        public List<Teil> GetBestand() {
            return _Bestand;
        }
    }
}