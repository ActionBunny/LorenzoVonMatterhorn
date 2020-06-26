using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Beleg2020
{
    class Fertigungsabteilung
    {
        static public TransportRoboter roboter = new TransportRoboter();
        static public List<Produktionseinrichtung> produktionseinrichtungen = new List<Produktionseinrichtung>();

        static private bool firstLine = true;
        
        #region Funktionen per UML
        /// <summary>
        /// Diese Funktion liest die Anlagenkonfiguration aus der Config.csv.
        /// Sie können das hier gezeigte Vorgehen im Eingangslager adaptieren, 
        /// um dort die Teile zu erzeugen.
        /// 
        /// Beachten Sie bitte, das die .csv-Datein im Hauptverzeichniss des Projektes liegen!
        /// Da VisualStudio, je nach Ihrer Konfiguration, das ausführbare Projekt im debug- oder 
        /// release Unterordner erzeugt müssen die den Pfad von dort aus zu den .csv-Datein angeben.
        /// z.B. "..\\..\\..\\Config.csv", was hier genaugenommen bedeutet, gehe drei Verzeichnissebenen
        /// nach oben, dort sind die entsprechenden Datein zu finden.
        /// </summary>
        /// <param name="path">der relative Pfad zu .csv Datei</param>
        /// <returns></returns>
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
                   EingangsLager lager = new EingangsLager("Eingangslager", ListeFähigkeitenMitDauer);
                   produktionseinrichtungen.Add(lager);
                   continue;
                }

                if (ListeFähigkeitenMitDauer.Any(x => x.Item1 == Verarbeitungsschritt.EINLAGERN))
                {
                    // erstelle Ausgangslager
                    AusgangsLager lager = new AusgangsLager("Ausgangslager", ListeFähigkeitenMitDauer);
                    produktionseinrichtungen.Add(lager);
                    continue;
                }

                //TODO: abfrage ist unlogisch (geht aber)
                if (ListeFähigkeitenMitDauer.Any(x => (x.Item1 != Verarbeitungsschritt.EINLAGERN) && (x.Item1 != Verarbeitungsschritt.INITIALISIEREN)))
                {
                    //erstelle Fertigungsinsel
                    Fertigungsinsel fertigungsinsel = new Fertigungsinsel("Fertigungsinsel", ListeFähigkeitenMitDauer);
                    produktionseinrichtungen.Add(fertigungsinsel);
                    continue;
                }

                #endregion
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
            return roboter.RegistriereProduktionsEinrichtungen();
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
            
            /// Dieser Teil muss von Ihnen mit den entsprechenden Funktionsaufrufen zum 
            /// - Initialisieren der Produktionseinrichtungen
            /// - Registrieren der Selben am Roboter
            /// - und Starten des Roboters, sofern die vorangegangenen Schritte erfolgreich waren
            /// ergänzt werden
            if (InitProduktionseinrichtungen())
            {
                Console.WriteLine("Initialisierung der Produktionseinrichtungen gestartet");
                if (RegistriereProduktionseinrichtungenAmRoboter()) 
                {
                    Console.WriteLine("Module der Produktionseinrichtung erfolgreich am " +
                        "Roboter angemeldet! Roboter wird gestartet");
                   
                    // Denken Sie daran, die Funktion zum Straten des Transportroboters einzufuegen!!!

                }
            }
        }       
    }
}
