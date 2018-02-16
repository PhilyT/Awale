using Awale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale.ViewModels
{
    public class ViewModelGame : ViewModelBase
    {
        private Game game;
        public ViewModelGame()
        {
            Game = new Game();
        }

        public Game Game { get => game; set { game = value; RaisePropertyChanged("Game"); } }
    }
}
