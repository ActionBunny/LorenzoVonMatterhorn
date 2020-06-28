using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Beleg2020
{
    // TODO: WICHTIG
    // "Achten Sie darauf, dass mehrere aufeinander folgende, auf der aktuellen Fertigungsinsel
    // durchführbare Rezeptschritte auch auf dieser Fertigungsinsel durchgeführt werden."



    // x Initialisiere fertigungsabteilung
    // x erstelle liste produktionse.
    // x sende liste an roboter
    // x starte roboter
    // befülle e lager
    // -> lese teil aus csv
    // -> instanziiere teile
    // -> füge zum e lager hinzu
    // 
    //
    //
    //

    class Fertigungsabteilung
    {
        static public TransportRoboter roboter = new TransportRoboter();
        static public List<Produktionseinrichtung> produktionseinrichtungen = new List<Produktionseinrichtung>();

        static private bool firstLine = true;
        
        private static bool LiesKonfig(String path)
        {
            List<Tuple<Verarbeitungsschritt, int>> ListeFähigkeitenMitDauer;

            Console.WriteLine("Einlesen gestartet!");
            var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                if (firstLine) { firstLine = false; continue; } // einfach nur die Kopfzeile überspringen

                var strings = line.Split(';');
                var name = strings[0];
                ListeFähigkeitenMitDauer = new List<Tuple<Verarbeitungsschritt, int>>();
                for (int i = 1; i < strings.Length; i++)
                {
                    var s = strings[i].Split(':');
                    ListeFähigkeitenMitDauer.Add(
                        new Tuple<Verarbeitungsschritt, int>(
                            (Verarbeitungsschritt)Enum.Parse(typeof(Verarbeitungsschritt), s[0]),
                            Int32.Parse(s.Length > 1 ? s[1] : "0") 
                            )
                        );
                }

                /**
                 * Hier werden die eigentlichen Objekte erzeugt. 
                 **/

                #region 
                if (ListeFähigkeitenMitDauer.Any(x => x.Item1 == Verarbeitungsschritt.INITIALISIEREN))
                {
                    // erstelle Eingangslager
                   EingangsLager lager = new EingangsLager(name, ListeFähigkeitenMitDauer);
                   produktionseinrichtungen.Add(lager);
                   continue;
                }

                if (ListeFähigkeitenMitDauer.Any(x => x.Item1 == Verarbeitungsschritt.EINLAGERN))
                {
                    // erstelle Ausgangslager
                    AusgangsLager lager = new AusgangsLager(name, ListeFähigkeitenMitDauer);
                    produktionseinrichtungen.Add(lager);
                    continue;
                }

                //TODO: abfrage ist unlogisch (geht aber)
                if (ListeFähigkeitenMitDauer.Any(x => (x.Item1 != Verarbeitungsschritt.EINLAGERN) && (x.Item1 != Verarbeitungsschritt.INITIALISIEREN)))
                {
                    //erstelle Fertigungsinsel
                    Fertigungsinsel fertigungsinsel = new Fertigungsinsel(name, ListeFähigkeitenMitDauer);
                    produktionseinrichtungen.Add(fertigungsinsel);
                    continue;
                }
            }
            return true;
        }

        /// <summary>
        /// Diese Funktion ist nicht besonders umfangreich, sie registriert die erzeugten 
        /// Produktionseinrichtungen lediglich am Roboter.
        /// </summary>
        /// <returns></returns>

        private static bool RegistriereProduktionseinrichtungenAmRoboter()
        {
            return roboter.RegistriereProduktionsEinrichtungen(produktionseinrichtungen);
        }
        /// <summary>
        /// Diese Funktion dient nur zum Abtrennen der eigentlichen Initialisierung 
        /// </summary>
        /// <returns></returns>
        private static bool InitProduktionseinrichtungen() 
        {
            Console.WriteLine("Initialisieren der Produktionseinrichtungen gestartet!");
            Console.WriteLine("Konfigurationsdatei wird zum Einlesen vorbereitet!");
            return LiesKonfig("..//..//..//Config.csv");
        }
        #endregion

        
       
        /// <summary>
        /// Die ist die Startfunktion ihres Programms
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Simulation gestartet!");
            // Die folgende Zeile sollten sie nicht verändern, sie kann Ihnen helfen
            if (!_internal.Check()) return;
            
            if (InitProduktionseinrichtungen())
            {
                Console.WriteLine("count "+produktionseinrichtungen.Count);
                for (int i = 0; i < produktionseinrichtungen.Count; i++)
                {
                    Console.WriteLine("prod.einr.: " + produktionseinrichtungen[i]._Name);
                    for(int i2=0; i2< produktionseinrichtungen[i]._ListeAusfuehrbarerVerarbeitungsschritte.Count; i2++)
                    {
                        Console.WriteLine("  " + produktionseinrichtungen[i]._ListeAusfuehrbarerVerarbeitungsschritte[i2].Item1 + " " + produktionseinrichtungen[i]._ListeAusfuehrbarerVerarbeitungsschritte[i2].Item2);
                    }
                }

                Console.WriteLine("Initialisierung der Produktionseinrichtungen gestartet");
                if (RegistriereProduktionseinrichtungenAmRoboter()) 
                {
                    Console.WriteLine("Roboter wird gestartet.");
                    // roboter starten
                    roboter.Start();
                }
                else
                {
                    Console.WriteLine("!!! Roboter geht nicht! Roboter wird NICHT gestratet");
                }
            }
        }       
    }
}
