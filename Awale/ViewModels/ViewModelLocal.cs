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
    public class ViewModelLocal : ViewModelBase
    {
        Frame frame;
        private DelegateCommand onePlayer;
        private DelegateCommand twoPlayer;
        private DelegateCommand retour;

        public ViewModelLocal(Frame frame)
        {
            this.frame = frame;
            onePlayer = new DelegateCommand(o => OnClickOnePlayer(o));
            twoPlayer = new DelegateCommand(o => OnClickTwoPlayer(o));
            retour = new DelegateCommand(o => OnClickRetour(o));
        }

        private void OnClickRetour(object o)
        {
            MenuView menu = new MenuView(frame);
            frame.Navigate(frame);
        }

        private void OnClickTwoPlayer(object o)
        {
            GameView game = new GameView();
            frame.Navigate(game);
        }

        private void OnClickOnePlayer(object o)
        {
        }

        public DelegateCommand Retour { get => retour; set => retour = value; }
        public DelegateCommand OnePlayer { get => onePlayer; set => onePlayer = value; }
        public DelegateCommand TwoPlayer { get => twoPlayer; set => twoPlayer = value; }
    }
}
