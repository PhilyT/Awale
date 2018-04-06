using Awale.Models;
using Awale.ViewModels;
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

namespace Awale.Views
{
    /// <summary>
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView : Page
    {
        public GameView(Frame frame, Player player1, Player player2)
        {
            InitializeComponent();
            DataContext = new ViewModelGame(frame, player1, player2);
        }

        public GameView(Frame frame, Player player1, Player player2, Serveur serveur)
        {
            InitializeComponent();
            DataContext = new ViewModelGame(frame, player1, player2, serveur);
        }

        public GameView(Frame frame, Player player1, Player player2, Client client)
        {
            InitializeComponent();
            DataContext = new ViewModelGame(frame, player1, player2, client);
        }

        public GameView(Frame frame, Player player1, bool ia)
        {
            InitializeComponent();
            DataContext = new ViewModelGame(frame, player1, true);
        }
    }
}
