using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoenwinkel.lib
{
    public enum Geslachten { Undefined = 0, Man = 1, Vrouw = 2 }
    public class Klant
    {
        private string voornaam;
        private string achternaam;
        private int schoenmaat;
        private string guID;

        private Geslachten geslacht;

        public Geslachten Geslacht
        {
            get
            {
                return geslacht;
            }
            set
            {
                if (value == Geslachten.Undefined)
                    throw new Exception("Kies een geslacht!");
                else
                    geslacht = value;
            }
        }

        public string Voornaam
        {
            get
            {
                return voornaam;
            }
            set
            {
                if (value != "")
                    voornaam = value;
                else
                    throw new Exception("Geef een voornaam in!");
            }
        }

        public string Achternaam
        {
            get
            {
                return achternaam;
            }
            set
            {
                if (value != "")
                    achternaam = value;
                else
                    throw new Exception("Geef een achternaam in!");
            }
        }

        public int Schoenmaat
        {
            get
            {
                return schoenmaat;
            }
            set
            {
                if (value >= 10 && value <= 50) //kleinste schoenmaat is 10, grootste is 50
                    schoenmaat = value;
                else
                    throw new Exception("De ingegeven schoenmaat moet tussen 10 en 50 zijn");
            }
        }

        public string GuID
        {
            get
            {
                return guID;
            }
        }

        public Klant(string klantVoornaam, string klantAchternaam, Geslachten klantGeslacht, string klantSchoenmaat)
        {
            //genereer nieuwe UserID
            guID = Guid.NewGuid().ToString(); //bewust gekozen voor geen guid set omdat we niet willen dat iemand dit handmatig aanmaakt.
            Voornaam = klantVoornaam;
            Achternaam = klantAchternaam;
            Geslacht = klantGeslacht;
            Schoenmaat = ParseSchoenmaat(klantSchoenmaat);
        }

        public void BewerkSchoenmaat(string nieuweSchoenmaat)
        {
            Schoenmaat = ParseSchoenmaat(nieuweSchoenmaat);
        }

        private int ParseSchoenmaat(string schoenmaat)
        { //stuk toevoegen waarin een leeg veld niet voor een numerieke fout zorgt
            bool schoenmaatCorrect = int.TryParse(schoenmaat, out int tempSchoenmaat);
            if (schoenmaatCorrect)
            { 
                return tempSchoenmaat;
            }
            else
            { 
                throw new Exception("De ingegeven schoenmaat moet numeriek zijn!");
            }
        }
    }
}
