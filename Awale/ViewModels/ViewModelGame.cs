using Awale.Models;
using Awale.Utils;
using Awale.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Awale.ViewModels
{
    public class ViewModelGame : ViewModelBase
    {
        private Game game;
        private DelegateCommand delegateCommand;
        private Frame frame;

        public ViewModelGame(Frame frame, Player player1, Player player2)
        {
            Game = new Game(player1, player2);
            DelegateCommand = new DelegateCommand(o => OnClickRetour(o));
            this.frame = frame;
        }

        public ViewModelGame(Frame frame, Player player1, Player player2,Serveur serveur)
        {
            Game = new Game(player1, player2, serveur);
            DelegateCommand = new DelegateCommand(o => OnClickRetour(o));
            this.frame = frame;
        }
        public ViewModelGame(Frame frame, Player player1, Player player2, Client client)
        {
            Game = new Game(player1, player2, client);
            DelegateCommand = new DelegateCommand(o => OnClickRetour(o));
            this.frame = frame;
        }
        public ViewModelGame(Frame frame, Player player1, bool ia)
        {
            Game = new Game(player1, ia);
            DelegateCommand = new DelegateCommand(o => OnClickRetour(o));
            this.frame = frame;
        }

        private void OnClickRetour(object o)
        {
            LocalView game = new LocalView(frame);
            frame.Navigate(game);
        }

        public Game Game { get => game; set { game = value; RaisePropertyChanged("Game"); } }

        public DelegateCommand DelegateCommand { get => delegateCommand; set => delegateCommand = value; }
    }
}
