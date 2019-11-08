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
        Klant klant = new Klant();
        List<Klant> klanten;
        //enum Geslachten {Undefined = 0, Man = 1, Vrouw = 2} //enum in de klasse gebruiken //enum is reeds publiek gedefiniëerd in de library
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnVoegKlantToe_Click(object sender, RoutedEventArgs e)
        {
            string voornaam = txtVoornaam.Text.ToString();
            string achternaam = txtFamilienaam.Text.ToString();
            int.TryParse(txtSchoenmaat.Text, out int schoenmaat);
            Geslachten geslacht;
            if (rdbGeslachtMan.IsChecked == true)
            {
                geslacht = Geslachten.Man;
            }
            else if (rdbGeslachtVrouw.IsChecked == true)
            {
                geslacht = Geslachten.Vrouw;
            }
            else //geen geslacht gesleecteerd
            {
                geslacht = Geslachten.Undefined;
            }

            try
            {
                klant.NieuweKlant(voornaam, achternaam, geslacht, schoenmaat );
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oeps er ging iets fout" + Environment.NewLine + exception.Message);
                return;
            }
            

            MessageBox.Show($"{klant.Voornaam} {klant.Achternaam} heeft een schoenmaat van {klant.Schoenmaat} en is een {klant.Geslacht}/nmet guid/n {klant.GuID}");
        }
    }
}
