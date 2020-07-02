using System;
using System.Collections.Generic;
using System.Text;

namespace Beleg2020
{
    class TransportRoboter
    {
        public List<Produktionseinrichtung> _produktionseinrichtungen;

        public bool RegistriereProduktionsEinrichtungen(List<Produktionseinrichtung> produktionseinrichtungen)
        {
            _produktionseinrichtungen = produktionseinrichtungen;
            Console.WriteLine("Roboter: prod einr: " + _produktionseinrichtungen.Count);
            return true;
        }

        public void Start(){
            // ???
        }

        /*
        private Produktionseinrichtung GetAbholbereiteProduktionseinrichtung(){

        }

        private Fertigungsinsel GetSchnellsteFertigungsinsel(){

        }*/
    }
}