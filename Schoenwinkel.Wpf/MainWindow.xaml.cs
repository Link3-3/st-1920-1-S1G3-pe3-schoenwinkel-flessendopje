using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Schoenwinkel.lib;

namespace Schoenwinkel.Wpf
{
    public partial class MainWindow : Window
    {
        List<Klant> klanten = new List<Klant>();
        List<Aankoop> aankopen = new List<Aankoop>();
        List<string> verkoopGeschiedenis = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void wdwMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartUp();
            KlantenToevoegen(); //zet dit gerust uit om te testen
            FillKlantenListBox();
            UpdateStoreStatistics();
        }
        void KlantenToevoegen() //zet dit gerust uit om te testen
        {
            //testen met vooringevulde correcte waarden zodat we niet duizend keer een nieuwe klant moeten invoegen.
            Klant klant1 = new Klant("Tim", "De Gieter", Geslachten.Man, "45");
            klanten.Add(klant1);
            Klant klant2 = new Klant("Pieter", "Peters", Geslachten.Man, "22");
            klanten.Add(klant2);
            Klant klant3 = new Klant("Lara", "Liers", Geslachten.Vrouw, "38");
            klanten.Add(klant3);
        }
        void StartUp() //testen met vooringevulde correcte waarden zodat we niet duizend keer een nieuwe klant moeten invoegen.
        {
            cmbSchoenKleur.Items.Add("Rood");
            cmbSchoenKleur.Items.Add("Groen");
            cmbSchoenKleur.Items.Add("Blauw");
            cmbSchoenKleur.Items.Add("Zwart");
            cmbSchoenKleur.Items.Add("Wit");

            cmbSchoenMerk.Items.Add("Adidas");
            cmbSchoenMerk.Items.Add("Nike");
            cmbSchoenMerk.Items.Add("Fila");
            cmbSchoenMerk.Items.Add("Puma");
        }

        private void btnVoegKlantToe_Click(object sender, RoutedEventArgs e) //voeg klant toe
        {
            Klant klant;
            lstKlantenlijst.SelectedIndex = -1;
            string voornaam = txtVoornaam.Text.ToString();
            string achternaam = txtFamilienaam.Text.ToString();
            string schoenmaat = txtSchoenmaat.Text.ToString();
            Geslachten geslacht;
            if (rdbGeslachtMan.IsChecked == true)
            {
                geslacht = Geslachten.Man;
            }
            else if (rdbGeslachtVrouw.IsChecked == true)
            {
                geslacht = Geslachten.Vrouw;
            }
            else //geen geslacht geslecteerd
            {
                geslacht = Geslachten.Undefined;
            }
            try
            {
                klant = new Klant(voornaam, achternaam, geslacht, schoenmaat);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            klanten.Add(klant); //wordt niet meer uitgevoerd indien fout doordat er een return in de catch zit

            ResetInputFields();
            FillKlantenListBox();
            ClearCostumerLabels();
            ClearCostumerStatistics();
            UpdateStoreStatistics();
            lstKlantenlijst.SelectedIndex = lstKlantenlijst.Items.Count - 1; //de nieuwe toegevoegde klant direct selecteren
        }
        void ResetInputFields()
        {
            rdbGeslachtMan.IsChecked = false;
            rdbGeslachtVrouw.IsChecked = false;
            txtFamilienaam.Text = "";
            txtVoornaam.Text = "";
            txtSchoenmaat.Text = "";
        }
        void FillKlantenListBox() //vult de klanten listbox met klanten uit de lijst klanten.
        {
            lstKlantenlijst.Items.Clear();
            foreach (Klant klant in klanten)
            {
                lstKlantenlijst.Items.Add(klant.Achternaam + " " + klant.Voornaam);
            }
        }
        void ClearCostumerLabels()
        {
            lblFamilienaam.Content = "-";
            lblVoornaam.Content = "-";
            lblSchoenmaat.Content = "-";
            lblId.Content = "-";
            lblGeslacht.Content = "-";
            txtNieuweSchoenmaat.Text = "";
        }
        void ClearCostumerStatistics()
        {
            lblAantalGekochteSchoenen.Content = "-";
            lblPrijsGekochteSchoenen.Content = "-";
            lstGekochteSchoenen.Items.Clear();
        }
        void UpdateCostumerStatistics()
        {
            lstGekochteSchoenen.Items.Clear();
            //find number of articles the client has bought & show price of total items bought
            int index = lstKlantenlijst.SelectedIndex;
            if (index == -1) //checks if a client is selected
                return;
            Klant geselecteerdeKlant = klanten[index];
            string guID = geselecteerdeKlant.GuID;

            int numberOfItemsBought = 0;
            decimal priceOfItemsBought = 0;
            foreach(Aankoop artikel in aankopen)
            {
                if (artikel.GuID == guID)
                {
                    numberOfItemsBought++;
                    priceOfItemsBought += artikel.Prijs;
                    lstGekochteSchoenen.Items.Add($"{artikel.Merk} - {artikel.Kleur} - € {artikel.Prijs}");
                }
            }
            lblAantalGekochteSchoenen.Content = numberOfItemsBought.ToString();
            lblPrijsGekochteSchoenen.Content = "€ " + priceOfItemsBought.ToString();
        }
        void UpdateStoreStatistics()
        {
            Winkel winkel = new Winkel();

            lblAantalVerkochteSchoenen.Content = winkel.AantalVerkochteSchoenen(aankopen);
            lblPrijsVerkochteSchoenen.Content = "€ " + winkel.TotaalPrijsVerkochteSchoenen(aankopen);

            lblGrootsteSchoenmaat.Content = winkel.GrootsteSchoenmaat(klanten);
            lblKleinsteSchoenmaat.Content = winkel.KleinsteSchoenmaat(klanten);
            lblGemiddeldeSchoenmaat.Content = winkel.GemiddeldeSchoenmaat(klanten);

            lstVerkochteSchoenen.Items.Clear();

            foreach(string sale in verkoopGeschiedenis)
            {
                lstVerkochteSchoenen.Items.Add(sale);
            }
        }
        private void lstKlantenlijst_SelectionChanged(object sender, SelectionChangedEventArgs e) //vult de labels met de geselecteerde klant.
        {
            int index = lstKlantenlijst.SelectedIndex;
            if (index != -1)
            { 
                Klant vraagKlantGegevens = klanten[index];
                lblId.Content = vraagKlantGegevens.GuID;
                lblFamilienaam.Content = vraagKlantGegevens.Achternaam;
                lblVoornaam.Content = vraagKlantGegevens.Voornaam;
                lblGeslacht.Content = vraagKlantGegevens.Geslacht;
                lblSchoenmaat.Content = vraagKlantGegevens.Schoenmaat;
                txtNieuweSchoenmaat.Text = vraagKlantGegevens.Schoenmaat.ToString();
            }
            cmbSchoenKleur.SelectedIndex = -1;
            cmbSchoenMerk.SelectedIndex = -1;
            txtSchoenPrijs.Text = "";
            UpdateCostumerStatistics();
        }
        private void btnWijzigSchoenmaat_Click(object sender, RoutedEventArgs e) //schoenmaat van een al bestaande klant wijzigen
        {
            int index = lstKlantenlijst.SelectedIndex;
            if (index >= 0)
            { 
                Klant geselecteerdeKlant = klanten[index];
                try
                {
                    geselecteerdeKlant.BewerkSchoenmaat(txtNieuweSchoenmaat.Text);
                    lblSchoenmaat.Content = geselecteerdeKlant.Schoenmaat;
                    MessageBox.Show($"De schoenmaat van {geselecteerdeKlant.Voornaam} {geselecteerdeKlant.Achternaam} is nu {geselecteerdeKlant.Schoenmaat}.", "Schoenmaat gewijzigd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    UpdateStoreStatistics();
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een klant alvorens de schoenmaat te wijzigen", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnVerkoopSchoen_Click(object sender, RoutedEventArgs e) //klant koopt een paar schoenen
        {
            if (lstKlantenlijst.SelectedIndex == -1)
            {
                MessageBox.Show("Selecteer eerst een klant alvorens schoenen te verkopen!", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Aankoop aankoop;
                string guID = lblId.Content.ToString(); //link client to sale
                string merk = cmbSchoenMerk.Text;
                string kleur = cmbSchoenKleur.Text;
                string prijs = txtSchoenPrijs.Text;
                try
                {
                    aankoop = new Aankoop(guID, merk, kleur, prijs);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                aankopen.Add(aankoop);

                foreach (Klant klant in klanten)
                {
                    if (aankoop.GuID == klant.GuID) //dit is de klant die dit item kocht
                    {
                        verkoopGeschiedenis.Insert(0,($"{klant.Achternaam} {klant.Voornaam} - {aankoop.Merk} - {aankoop.Kleur} - € {aankoop.Prijs}"));
                    }
                }
                UpdateCostumerStatistics();
                UpdateStoreStatistics();
            }
        }

        private void btnVerwijderKlant_Click(object sender, RoutedEventArgs e)
        {
            //bij het verwijderen van deze klant zullen ook de aangekochte schoenen verwijderd worden
            //verwijdert de klant uit lijst klanten en verwijdert zijn/haar aankopen uit lijst aankopen
            int index = lstKlantenlijst.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Selecteer eerst een klant om te verwijderen", "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Klant verwijderKlant = klanten[index];
            string guID = verwijderKlant.GuID;
            string voornaam = verwijderKlant.Voornaam;
            string achternaam = verwijderKlant.Achternaam;
            List<int> teDeletenLocaties = new List<int>();

            if(MessageBox.Show($"Wilt u {achternaam} {voornaam} permanent verwijderen?" + Environment.NewLine + "Alsook de door deze klant aangekochte schoenen zullen verwijderd worden." + Environment.NewLine + "Echter blijven de aankopen wel in het winkelregister staan.", "Bevestig verwijdering klant", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return; //bij het induwen van de NO knop zal de methode hier afgebroken worden
            }
            if (aankopen.Count != 0)
            {
                for (int i = 0; i >= aankopen.Count; i++) //zoekt alle aankopen door de gebruiker
                {
                    Aankoop aankoop = aankopen[i];
                    if (aankoop.GuID == guID)
                    {
                        teDeletenLocaties.Add(i);
                    }
                }
            
                teDeletenLocaties.Reverse(); //keert lijst om, zodat de laatste index nummers eerst staan
                foreach(int locatie in teDeletenLocaties) //verwijdert indexen te beginnen met de hoogste (als we het andersom zouden doen zou er een probleem zijn doordat de indexen veranderen (eg: delete 1 en 2 wordt 1)
                {
                    aankopen.RemoveAt(locatie);
                }
            }
            //na het deleten van alle aankopen deleten we nu ook de klant
            klanten.RemoveAt(index);
            FillKlantenListBox();
            ClearCostumerLabels();
            ClearCostumerStatistics();
            UpdateStoreStatistics();
        }
        //all this just to highlight a textbox :(
        private void txtNieuweSchoenmaat_GotMouseCapture(object sender, MouseEventArgs e)
        {
            txtNieuweSchoenmaat.SelectAll();
        }
    }
}