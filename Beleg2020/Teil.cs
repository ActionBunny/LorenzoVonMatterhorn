using System;
using System.Collections.Generic;

namespace Beleg2020
{
    public class Teil
    {
        private List<Verarbeitungsschritt> _Rezept;
        private List<Tuple<Verarbeitungsschritt, string>> _Historie;
        private string _Seriennummer;

        public Teil(List<Verarbeitungsschritt> rezept, string seriennummer) {
            _Rezept = rezept;
            _Seriennummer = seriennummer;
            _Historie = new List<Tuple<Verarbeitungsschritt, string>>();
        }
        public string GetSeriennummer() {
            return _Seriennummer;
        }

        public Verarbeitungsschritt GetNaechsterSchritt() {
            return _Rezept[0];
        }

        public Verarbeitungsschritt TransferiereSchrittInHistorie(Produktionseinrichtung bearbeitetIn) {
            // packe erstes element aus rezept in historie
            Verarbeitungsschritt schritt = _Rezept[0];
            Tuple<Verarbeitungsschritt, string> historienEintrag = new Tuple<Verarbeitungsschritt, string>(_Rezept[0], bearbeitetIn.GetName());
            _Historie.Add(historienEintrag);
            // entferne aus rezept
            _Rezept.RemoveAt(0);
            return schritt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True wenn Rezept abgearbeitet.</returns>
        public bool SelbstTestTeil() {
            if (_Rezept.Count == 0)
                return true;

            return _Rezept[0] == Verarbeitungsschritt.EINLAGERN;
        }
        public List<Tuple<Verarbeitungsschritt, string>> LiefereHistorie() {
            return _Historie;
        }
    }
}