using ConsoleApp1;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour personne.xaml
    /// </summary>
    
    public partial class personne : Window
    {
        MainWindow mainWindow = new MainWindow() ;
        Personne pers;
        public personne(MainWindow mainWindow)
        {
            InitializeComponent();
            this.pers = new Personne();
            this.mainWindow = mainWindow;
            this.DataContext = pers;
        }

        public Personne Personne { get => pers; set => pers = value; }

        private void Button_ClickAjout(object sender, RoutedEventArgs e)
        {
            string[] tab = dateNaiss.Text.Split('/');

            DateTime date = new DateTime(int.Parse(tab[2]), int.Parse(tab[1]) ,int.Parse(tab[0]));
            pers.DateNaisss1 = date;
           
            mainWindow.Personnes.Add(pers);
            Close();
        
        }
    }
}
