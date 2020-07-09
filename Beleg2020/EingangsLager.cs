using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Beleg2020
{
    public class EingangsLager : Produktionseinrichtung
    {
        List<Teil> _BestandZubearbeiten = new List<Teil>();

        public EingangsLager(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten) {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }

        public bool InitialisiereTeile(string pfadZuCSV) {
            Console.WriteLine("Einganglager: Einlesen gestartet!");
            var lines = File.ReadLines(pfadZuCSV);
            bool firstLine = true;
            foreach (var line in lines) {
                if (firstLine) { firstLine = false; continue; } // einfach nur die Kopfzeile überspringen

                var worte = line.Split(';');  // teile zeile in worte
                //erstelle teil infos
                var seriennummer = worte[0];
                List<Verarbeitungsschritt> rezept = new List<Verarbeitungsschritt>();

                for (int i = 1; i <= 4; i++) {
                    if (worte[i] != "") {
                        rezept.Add((Verarbeitungsschritt)Enum.Parse(typeof(Verarbeitungsschritt), worte[i]));
                    }
                }

                // erstelle objekt "Teil"
                Teil teil = new Teil(rezept, seriennummer);
                // für teil teile liste hinzu
                _BestandZubearbeiten.Add(teil);
            }
            Console.WriteLine("Einganglager: " + _BestandZubearbeiten.Count + " Teile eingelesen.");
            return true;
        }

        public Teil TeilAusgeben() {
            if (_BestandZubearbeiten.Count > 0) {
                Teil tempTeil = _BestandZubearbeiten[0];
                _BestandZubearbeiten.RemoveAt(0);
                return tempTeil;
            }
            Console.WriteLine("!!! Einganslager: leer.");
            return null;
        }

        public void TeilZwischenlagern(Teil t) {
            _BestandZubearbeiten.Add(t);
           // Console.WriteLine("Einganglager: " + t.GetSeriennummer() + " zwischengelagert.");
        }

        public override Status BerechneStatus() {
            if (_BestandZubearbeiten.Count == 0) {
        //        Console.WriteLine("!!! Einganslager: EMPFANGSBEREIT.");
                return Status.EMPFANGSBEREIT;
            }
        //    Console.WriteLine("Einganslager: ABHOLBEREIT.");
            return Status.ABHOLBEREIT;
        }
    }
}