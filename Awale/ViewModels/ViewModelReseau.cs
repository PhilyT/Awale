using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Awale.ViewModels
{
    public class ViewModelReseau : ViewModelBase
    {
        Frame frame;

        public ViewModelReseau(Frame frame)
        {
            this.frame = frame;
        }
    }
}
