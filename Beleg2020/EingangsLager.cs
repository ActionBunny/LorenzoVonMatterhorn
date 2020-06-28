using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Beleg2020
{
    public class EingangsLager : Produktionseinrichtung
    {

        List<Teil> teile = new List<Teil>();

        public EingangsLager(string name, List<Tuple<Verarbeitungsschritt, int>> faehigkeiten)
        {
            _Name = name;
            _ListeAusfuehrbarerVerarbeitungsschritte = faehigkeiten;
        }

        public override Status BerechneStatus()
        {
            throw new NotImplementedException();
        }

        private bool InitialisiereTeile(string pfadZuCSV){
            Console.WriteLine("Einlesen gestartet!");
            var lines = File.ReadLines(pfadZuCSV);
            bool firstLine = true;
            foreach (var line in lines)
            {
                if (firstLine) { firstLine = false; continue; } // einfach nur die Kopfzeile überspringen

                var worte = line.Split(';');  // teile zeile in worte
                
                //erstelle teil infos
                var seriennummer = worte[0];
                List<Verarbeitungsschritt> rezept = new List<Verarbeitungsschritt>();
                rezept.Add((Verarbeitungsschritt) Enum.Parse(typeof(Verarbeitungsschritt), worte[1]));
                rezept.Add((Verarbeitungsschritt) Enum.Parse(typeof(Verarbeitungsschritt), worte[2]));
                rezept.Add((Verarbeitungsschritt) Enum.Parse(typeof(Verarbeitungsschritt), worte[3]));
                rezept.Add((Verarbeitungsschritt) Enum.Parse(typeof(Verarbeitungsschritt), worte[4]));

                // erstelle objekt "Teil"
                Teil teil = new Teil(rezept, seriennummer);
                // für teil teile liste hinzu
                teile.Add(teil);
            }
            return false;
        }
/*
        public Teil TeilAusgeben() {
            //Status zurückgeben
        }

        public void TeilZwischenlagern(Teil t) {

        }

        public Status BerechneStatus() {
            //Status zurückgeben
        }
        
         */

    }
}