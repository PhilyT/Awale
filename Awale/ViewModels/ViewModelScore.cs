using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Awale.ViewModels
{
    public class ViewModelScore : ViewModelBase
    {
        Frame frame;
        public ViewModelScore(Frame frame)
        {
            this.frame = frame;
        }
    }
}
