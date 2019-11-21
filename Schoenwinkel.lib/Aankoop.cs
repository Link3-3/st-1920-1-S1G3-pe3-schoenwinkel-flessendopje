using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoenwinkel.lib
{
    public class Aankoop
    {
        private decimal prijs;
        private string merk;
        private string kleur;
        private string guID;
        public decimal Prijs 
        {
            get
            {
                return prijs;
            }
            set
            {
                if (value > 0)
                    prijs = value;
                else if (value == 0)
                    throw new Exception("De prijs kan niet nul zijn!");
                else
                    throw new Exception("De prijs kan niet negatief zijn!");
            }
        }

        public string Merk
        {
            get
            {
                return merk;
            }
            set
            {
                if (value != "")
                    merk = value;
                else
                    throw new Exception("Selecteer een merk!");
            }
        }

        public string Kleur
        {
            get
            {
                return kleur;
            }
            set
            {
                if (value != "") //anders schrijven want het is een index
                    kleur = value;
                else
                    throw new Exception("Selecteer een kleur");
            }
        }

        public string GuID
        {
            get
            {
                return guID;
            }
            set
            {
                if (value != "")
                    guID = value;
                else
                    throw new Exception("Selecteer eerst een klant alvorens een aankoop te maken");
            }
        }

        private decimal ParsePrijs(string prijs)
        {
            prijs = prijs.Replace('.', ','); //zal een punt-komma omzetten naar een decimale komma
            bool prijsCorrect = decimal.TryParse(prijs, out decimal tempPrijs);
            if (prijsCorrect)
            {
                return tempPrijs;
            }
            else
            {
                if (prijs == "")
                    throw new Exception("Geef een prijs in");
                else
                    throw new Exception("De ingegeven prijs moet numeriek zijn!");
            }
        }

        public Aankoop(string klantGuID, string schoenMerk, string schoenKleur, string schoenPrijs)
        {
            GuID = klantGuID; //houd bij welke klant dit item kocht
            Merk = schoenMerk;
            Kleur = schoenKleur;
            Prijs = ParsePrijs(schoenPrijs);
        }
    }
}
