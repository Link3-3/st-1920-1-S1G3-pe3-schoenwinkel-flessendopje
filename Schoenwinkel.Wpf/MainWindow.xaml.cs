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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void wdwMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartUp();
            FillKlantenListBox();
        }

        void StartUp() //testen met vooringevulde correcte waarden zodat we niet duizend keer een nieuwe klant moeten invoegen.
        {
            Klant klant1 = new Klant();
            klant1.NieuweKlant("Tim", "De Gieter", Geslachten.Man, "45");
            klanten.Add(klant1);
            Klant klant2 = new Klant();
            klant2.NieuweKlant("Pieter", "Peters", Geslachten.Man, "22");
            klanten.Add(klant2);
            Klant klant3 = new Klant();
            klant3.NieuweKlant("Lara", "Liers", Geslachten.Vrouw, "38");
            klanten.Add(klant3);

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
            lstKlantenlijst.SelectedIndex = -1;
            Klant klant = new Klant();
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
                klant.NieuweKlant(voornaam, achternaam, geslacht, schoenmaat);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            klanten.Add(klant); //wordt niet meer uitgevoerd indien fout doordat er een return in de catch zit
            FillKlantenListBox();
        }

        void FillKlantenListBox() //vult de klanten listbox met klanten uit de lijst klanten. eventueel alphabetisch maken?
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
                    MessageBox.Show($"De schoenmaat van {geselecteerdeKlant.Voornaam} {geselecteerdeKlant.Achternaam} is nu {geselecteerdeKlant.Schoenmaat}.", "Schoenmaat gewijzigd",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
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
                Aankoop aankoop = new Aankoop();
                string guID = lblId.Content.ToString(); //link client to sale
                string merk = cmbSchoenMerk.Text;
                string kleur = cmbSchoenKleur.Text;
                string prijs = txtSchoenPrijs.Text;
                try
                {
                    aankoop.VerkoopSchoenen(guID, merk, kleur, prijs);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Fout!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                aankopen.Add(aankoop);
                //MessageBox.Show(aankoop.GuID + aankoop.Kleur + aankoop.Merk + aankoop.Prijs); //debug
            }
        }

        private void btnVerwijderKlant_Click(object sender, RoutedEventArgs e)
        {
            //messagebox om te vragen of je effectief de klant wil deleten?
            //verwijdert de klant uit lijst klanten en verwijdert zijn/haar aankopen uit lijst aankopen
            int index = lstKlantenlijst.SelectedIndex;
            Klant verwijderKlant = klanten[index];
            string guID = verwijderKlant.GuID;
            List<int> teDeletenLocaties = new List<int>();
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
        }
    }
}
