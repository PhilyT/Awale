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
   public  class ViewModelMenu: ViewModelBase
    {
        private DelegateCommand delegateLocal;
        private DelegateCommand delegateReseau;
        private DelegateCommand delegateScore;
        private Frame frame;
        
        public ViewModelMenu(Frame frame)
        {
            DelegateLocal = new DelegateCommand(o => OnclickLocal(o));
            DelegateReseau = new DelegateCommand(o => OnclickReseau(o));
            DelegateScore = new DelegateCommand(o => OnclickScore(o));
            this.frame = frame;   
        }

        public DelegateCommand DelegateLocal { get => delegateLocal; set => delegateLocal = value; }
        public DelegateCommand DelegateReseau { get => delegateReseau; set => delegateReseau = value; }
        public DelegateCommand DelegateScore { get => delegateScore; set => delegateScore = value; }
       
        public void OnclickLocal(object O)
       
        {
            LocalView viewLocal = new LocalView(frame);
            frame.Navigate(viewLocal);
        }

        public void OnclickReseau(object O)
        {
            SelectionReseau viewSelectionReseau = new SelectionReseau(frame);
            frame.Navigate(viewSelectionReseau);
        }
        public void OnclickScore(object O)
        {
            ScoreView viewScore = new ScoreView(frame);
            frame.Navigate(viewScore);
        }
    }
}
