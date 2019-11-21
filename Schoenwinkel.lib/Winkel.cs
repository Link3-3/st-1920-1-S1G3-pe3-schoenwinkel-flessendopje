using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoenwinkel.lib
{
    public class Winkel
    {
        public int GrootsteSchoenmaat(List<Klant> klanten)
        {
            int grootsteSchoenmaat = 0;
            foreach(Klant klant in klanten)
            {
                if(klant.Schoenmaat > grootsteSchoenmaat)
                {
                    grootsteSchoenmaat = klant.Schoenmaat;
                }
            }
            return grootsteSchoenmaat;
        }

        public int KleinsteSchoenmaat(List<Klant> klanten)
        {
            int kleinsteSchoenmaat = 100;
            foreach (Klant klant in klanten)
            {
                if (klant.Schoenmaat < kleinsteSchoenmaat)
                {
                    kleinsteSchoenmaat = klant.Schoenmaat;
                }
            }
            if (kleinsteSchoenmaat == 100)
                kleinsteSchoenmaat = 0; //puur om er mooi uit te zien als we geen klanten hebben
            return kleinsteSchoenmaat;
        }

        public int GemiddeldeSchoenmaat(List<Klant> klanten)
        {
            int gemiddeldeSchoenmaat = 0;
            if (klanten.Count != 0) 
            { 
                foreach(Klant klant in klanten)
                {
                    gemiddeldeSchoenmaat += klant.Schoenmaat;
                }
                gemiddeldeSchoenmaat = gemiddeldeSchoenmaat / klanten.Count;
            }
            return gemiddeldeSchoenmaat;
        }

        public int AantalVerkochteSchoenen(List<Aankoop> aankopen)
        {
            return aankopen.Count();
        }

        public decimal TotaalPrijsVerkochteSchoenen(List<Aankoop> aankopen)
        {
            decimal totaalPrijs = 0;
            foreach(Aankoop aankoop in aankopen)
            {
                totaalPrijs += aankoop.Prijs;
            }
            return totaalPrijs;
        }


    }
}