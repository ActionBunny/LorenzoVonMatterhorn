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

        public override Status BerechneStatus()
        {
            return Status.EMPFANGSBEREIT;
        }
        
        private void GibHistorieAus(Teil t)
        {
            string serienNummer = t.GetSeriennummer();
            foreach (Tuple<Verarbeitungsschritt, string> historienEintrag in t.LiefereHistorie())
            {
                Console.WriteLine("Teil mit der Seriennummer" + serienNummer + "wurde mit Schritt " + historienEintrag.Item1 + "bearbeitet von Maschine " + historienEintrag.Item2);
            }
        }

        public void TeilFuerDenVersandEmpfangen(Teil t)
        {
            throw new NotImplementedException();
        }
    }
}