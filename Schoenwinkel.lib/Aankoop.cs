using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoenwinkel.lib
{
    public class Aankoop
    {
        private int prijs;
        private string merk;
        private string kleur;
        private string guID;
        public int Prijs 
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

        private int ParsePrijs(string prijs)
        {
            bool prijsCorrect = int.TryParse(prijs, out int tempPrijs);
            if (prijsCorrect)
            {
                return tempPrijs;
            }
            else
            {
                throw new Exception("De ingegeven prijs moet numeriek zijn!");
            }
        }

        public void VerkoopSchoenen(string klantGuID, string schoenMerk, string schoenKleur, string schoenPrijs)
        {
            GuID = klantGuID; //houd bij welke klant dit item kocht
            Merk = schoenMerk;
            Kleur = schoenKleur;
            Prijs = ParsePrijs(schoenPrijs);
        }
    }
}
