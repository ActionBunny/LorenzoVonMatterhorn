using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    public class AusgangsLager : Produktionseinrichtung
    {
        List<Teil> _Bestand = new List<Teil>();

        public AusgangsLager(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten) {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }

        public override Status BerechneStatus() {
            return Status.EMPFANGSBEREIT;
        }

        private void GibHistorieAus(Teil t) {
            string serienNummer = t.GetSeriennummer();

            if (!t.SelbstTestTeil()) {
                Console.WriteLine("!!! Ausganglager: Teil mit der Seriennummer " + serienNummer + " nicht fertiggestellt.");
                return;
            }

            foreach (Tuple<Verarbeitungsschritt, string> historienEintrag in t.LiefereHistorie()) {
                Console.WriteLine("Ausganglager: Teil mit der Seriennummer " + serienNummer + " wurde mit Schritt " + historienEintrag.Item1 + "bearbeitet von Maschine " + historienEintrag.Item2);
            }
        }

        public void TeilFuerDenVersandEmpfangen(Teil t) {
            _Bestand.Add(t);
            Console.WriteLine("Ausganglager: Teil mit der Seriennummer " + t.GetSeriennummer() + " empfangen.");

            if (!t.SelbstTestTeil()) {
                Console.WriteLine("!!!  Ausganglager: Teil mit der Seriennummer " + t.GetSeriennummer() + " nicht fertiggestellt.");
            }
        }
    }
}