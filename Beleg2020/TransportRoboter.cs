using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    class TransportRoboter
    {
        public List<Produktionseinrichtung> _Produktionseinrichtungen;
        Teil _AktuellesTeil;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produktionseinrichtungen"></param>
        /// <returns></returns>
        public bool RegistriereProduktionsEinrichtungen(List<Produktionseinrichtung> produktionseinrichtungen) {
            _Produktionseinrichtungen = produktionseinrichtungen;
            Console.WriteLine("Roboter: prod einr: " + _Produktionseinrichtungen.Count);
            return true;
        }

        public void Start() {
            // TODO: schaue ob alle Verarbeitungsschritte in FIs behandelt werden können

            DateTime nächsterSchritt = DateTime.Now;

            while (!IstFertig()) {
                if (DateTime.Now >= nächsterSchritt.AddSeconds(1 / 4.0)) {
                    nächsterSchritt = DateTime.Now;

                    Produktionseinrichtung prodEinr = GetAbholbereiteProduktionseinrichtung();
                    // wenn es eine abholbereite produktionseinrichtung gibt
                    if (prodEinr != null) {
                        // hole teil
                        if (prodEinr is EingangsLager) {
                            _AktuellesTeil = ((EingangsLager)prodEinr).TeilAusgeben();
                        } else if (prodEinr is Fertigungsinsel) {
                            _AktuellesTeil = ((Fertigungsinsel)prodEinr).TeilZurueckgeben();
                            //    _aktue
                        }
                        Console.WriteLine(_AktuellesTeil.GetSeriennummer() + " < " + prodEinr.GetName());

                        if (_AktuellesTeil.SelbstTestTeil()) {
                            Console.WriteLine(_AktuellesTeil.GetSeriennummer() + " > LagerAus");
                            FindeAusgangsLager().TeilFuerDenVersandEmpfangen(_AktuellesTeil);
                        } else {

                            Fertigungsinsel insel = GetSchnellsteFertigungsinsel();
                            // wenn es keine FI gibt die das teil verarbeiten kann
                            if (insel == null) {
                                Console.WriteLine(_AktuellesTeil.GetSeriennummer() + " > LagerEin");
                                findeEingangsLager().TeilZwischenlagern(_AktuellesTeil);

                            } else {
                                Console.WriteLine(_AktuellesTeil.GetSeriennummer() + " > " + insel.GetName());
                                insel.TeilEntgegennehmen(_AktuellesTeil);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Robbie ist fertig. " + FindeAusgangsLager().GetBestand().Count + " Teile im Ausgangslager");
        }

        private AusgangsLager FindeAusgangsLager() {
            foreach (Produktionseinrichtung p in _Produktionseinrichtungen) {
                if (p is AusgangsLager) {
                    return (AusgangsLager)p;
                }
            }
            return null;
        }

        private EingangsLager findeEingangsLager() {
            foreach (Produktionseinrichtung p in _Produktionseinrichtungen) {
                if (p is EingangsLager) {
                    return (EingangsLager)p;
                }
            }
            return null;
        }

        private bool IstFertig() {
            foreach (Produktionseinrichtung p in _Produktionseinrichtungen) {
                if (p.BerechneStatus() != Status.EMPFANGSBEREIT) {
                    return false;
                }
            }
            return true;
        }

        private Produktionseinrichtung GetAbholbereiteProduktionseinrichtung() {
            // priorisiere Fertigungsinseln
            // suche nach bereiten fertigungsinseln
            foreach (Produktionseinrichtung p in _Produktionseinrichtungen) {

                if ((p.BerechneStatus() == Status.ABHOLBEREIT) && (p is Fertigungsinsel)) {
                    return p;
                }
            }
            // suche nach bereiter eingangslager
            foreach (Produktionseinrichtung p in _Produktionseinrichtungen) {
                if ((p.BerechneStatus() == Status.ABHOLBEREIT) && (p is EingangsLager)) {
                    return p;
                }
            }
            return null;
        }

        /// <summary>
        /// Findet irgendwas.
        /// </summary>
        /// <returns> Schnellste empfangsbereite Fertigungsinsel. null wenn es keine Schnellste gibt.</returns>
        private Fertigungsinsel GetSchnellsteFertigungsinsel() {
            // aktueller schritt
            Verarbeitungsschritt naechsterSchritt = _AktuellesTeil.GetNaechsterSchritt();

            // suche nach allen EMPFANGSBEREITen Fertigungsinseln
            List<Fertigungsinsel> empfangsbereiteFertigungsinseln = new List<Fertigungsinsel>();

            foreach (Produktionseinrichtung p in _Produktionseinrichtungen) {
                if ((p.BerechneStatus() == Status.EMPFANGSBEREIT) && (p is Fertigungsinsel)) {
                    empfangsbereiteFertigungsinseln.Add((Fertigungsinsel)p);
                }
            }
            // suche nach geringster fertigungsdauer für aktuellen verarbeitungsschritt
            int geringsteDauer = 9999;
            foreach (Fertigungsinsel f in empfangsbereiteFertigungsinseln) {
                foreach (Tuple<Verarbeitungsschritt, int> schritt in f.GetAusfuehrbareVerarbeitungsschritteUndDauer()) {
                    if (schritt.Item1 == naechsterSchritt) {
                        if (schritt.Item2 < geringsteDauer) {
                            geringsteDauer = schritt.Item2;
                        }
                    }
                }
            }

            // suche nach fertigungsinsel mit geringster fertigungsdauer für aktuellen verarbeitungsschritt
            foreach (Fertigungsinsel f in empfangsbereiteFertigungsinseln) {
                foreach (Tuple<Verarbeitungsschritt, int> schritt in f.GetAusfuehrbareVerarbeitungsschritteUndDauer()) {
                    if (schritt.Item1 == naechsterSchritt && schritt.Item2 == geringsteDauer) {
                        return f;
                    }
                }
            }
            return null;
        }
    }
}