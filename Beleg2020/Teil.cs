using System;
using System.Collections.Generic;


namespace Beleg2020
{
    public class Teil
    {
        private List<Verarbeitungsschritt> _Rezept;
        private List<Tuple<Verarbeitungsschritt, String>> _Historie;
        private String _Seriennummer;

        public Teil(List<Verarbeitungsschritt> rezept, String seriennummer) {
            _Rezept = rezept;
            _Seriennummer = seriennummer;
        }
        public string GetSeriennummer() {
            return _Seriennummer;
        }

        public Verarbeitungsschritt GetNaechsterSchritt() {
            throw new NotImplementedException("Hier müssen Sie noch etwas ergänzen");

        }

        public Verarbeitungsschritt TransferiereSchrittInHistorie(Produktionseinrichtung bearbeitetIn) {
            // packe erstes element aus rezept in historie
            Verarbeitungsschritt schritt = _Rezept[0];
            Tuple<Verarbeitungsschritt, string> historienEintrag = new Tuple<Verarbeitungsschritt, string>(_Rezept[0], bearbeitetIn._Name);
            _Historie.Add(historienEintrag);
            // entferne aus rezept
            _Rezept.RemoveAt(0);
            return schritt;
        }

        public bool SelbstTestTeil() {
            foreach (Verarbeitungsschritt schritt in _Rezept) {
                if (schritt == Verarbeitungsschritt.FRAESEN
                    || schritt == Verarbeitungsschritt.LOETEN
                    || schritt == Verarbeitungsschritt.BESCHICHTEN
                    || schritt == Verarbeitungsschritt.TRENNEN) {

                    return false;
                }
            }
            return true;
        }
        public List<Tuple<Verarbeitungsschritt, String>> LiefereHistorie() {
            return _Historie;
        }
    }
}