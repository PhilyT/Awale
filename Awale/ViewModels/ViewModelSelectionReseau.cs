using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Awale.ViewModels
{
    public class ViewModelSelectionReseau : ViewModelBase
    {
        Frame frame;
        public ViewModelSelectionReseau(Frame frame)
        {
            this.frame = frame;
        }
    }
}
