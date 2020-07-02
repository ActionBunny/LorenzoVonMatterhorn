using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Beleg2020
{
    // TODO: WICHTIG
    // "Achten Sie darauf, dass mehrere aufeinander folgende, auf der aktuellen Fertigungsinsel
    // durchführbare Rezeptschritte auch auf dieser Fertigungsinsel durchgeführt werden."

    class Fertigungsabteilung
    {
        static public TransportRoboter roboter = new TransportRoboter();
        static public List<Produktionseinrichtung> produktionseinrichtungen = new List<Produktionseinrichtung>();

        static private bool firstLine = true;

        private static bool LiesKonfig(String path) {
            List<Tuple<Verarbeitungsschritt, int>> ListeFähigkeitenMitDauer;

            Console.WriteLine("Fertigungsabteilung: Einlesen gestartet!");
            var lines = File.ReadLines(path);
            foreach (var line in lines) {
                if (firstLine) { firstLine = false; continue; } // einfach nur die Kopfzeile überspringen

                var strings = line.Split(';');
                var name = strings[0];
                ListeFähigkeitenMitDauer = new List<Tuple<Verarbeitungsschritt, int>>();
                for (int i = 1; i < strings.Length; i++) {
                    var s = strings[i].Split(':');
                    ListeFähigkeitenMitDauer.Add(
                        new Tuple<Verarbeitungsschritt, int>(
                            (Verarbeitungsschritt)Enum.Parse(typeof(Verarbeitungsschritt), s[0]),
                            Int32.Parse(s.Length > 1 ? s[1] : "0")
                            )
                        );
                }

                if (ListeFähigkeitenMitDauer.Any(x => x.Item1 == Verarbeitungsschritt.INITIALISIEREN)) {
                    // erstelle Eingangslager
                    EingangsLager lager = new EingangsLager(name, ListeFähigkeitenMitDauer);
                    lager.InitialisiereTeile("..//..//..//Teile.csv");
                    produktionseinrichtungen.Add(lager);
                    continue;
                }

                if (ListeFähigkeitenMitDauer.Any(x => x.Item1 == Verarbeitungsschritt.EINLAGERN)) {
                    // erstelle Ausgangslager
                    AusgangsLager lager = new AusgangsLager(name, ListeFähigkeitenMitDauer);
                    produktionseinrichtungen.Add(lager);
                    continue;
                }

                //TODO: abfrage ist unlogisch (geht aber)
                if (ListeFähigkeitenMitDauer.Any(x => (x.Item1 != Verarbeitungsschritt.EINLAGERN) && (x.Item1 != Verarbeitungsschritt.INITIALISIEREN))) {
                    //erstelle Fertigungsinsel
                    Fertigungsinsel fertigungsinsel = new Fertigungsinsel(name, ListeFähigkeitenMitDauer);
                    produktionseinrichtungen.Add(fertigungsinsel);
                    continue;
                }
            }
            return true;
        }

        private static bool RegistriereProduktionseinrichtungenAmRoboter() {
            return roboter.RegistriereProduktionsEinrichtungen(produktionseinrichtungen);
        }
        private static bool InitProduktionseinrichtungen() {
            Console.WriteLine("Fertigungsabteilung: Initialisieren der Produktionseinrichtungen gestartet!");
            Console.WriteLine("Fertigungsabteilung: Konfigurationsdatei wird zum Einlesen vorbereitet!");
            return LiesKonfig("..//..//..//Config.csv");
        }

        /// <summary>
        /// Die ist die Startfunktion ihres Programms
        /// </summary>
        public static void Main() {
            Console.WriteLine("Fertigungsabteilung: Simulation gestartet!");
            // Die folgende Zeile sollten sie nicht verändern, sie kann Ihnen helfen
            if (!_internal.Check()) return;

            if (InitProduktionseinrichtungen()) {
                for (int i = 0; i < produktionseinrichtungen.Count; i++) {
                    Console.WriteLine("Fertigungsabteilung: prod.einr.: " + produktionseinrichtungen[i]._Name);
                    for (int i2 = 0; i2 < produktionseinrichtungen[i]._ListeAusfuehrbarerVerarbeitungsschritte.Count; i2++) {
                        Console.WriteLine("  " + produktionseinrichtungen[i]._ListeAusfuehrbarerVerarbeitungsschritte[i2].Item1 + " " + produktionseinrichtungen[i]._ListeAusfuehrbarerVerarbeitungsschritte[i2].Item2);
                    }
                }

                Console.WriteLine("Fertigungsabteilung: Initialisierung der Produktionseinrichtungen gestartet");
                if (RegistriereProduktionseinrichtungenAmRoboter()) {
                    Console.WriteLine("Fertigungsabteilung: Roboter wird gestartet.");
                    // roboter starten
                    roboter.Start();
                } else {
                    Console.WriteLine("!!!Fertigungsabteilung:  Roboter geht nicht! Roboter wird NICHT gestratet");
                }
            }
        }
    }
}
