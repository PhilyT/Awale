using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Game
    {
        private List<string> trous;

        public Game()
        {
            trous = new List<string>();
            trous.Add("Trou1");
            trous.Add("Trou2");
            trous.Add("Trou3");
            trous.Add("Trou4");
            trous.Add("Trou5");
            trous.Add("Trou6");
            trous.Add("Trou1Adverse");
            trous.Add("Trou2Adverse");
            trous.Add("Trou3Adverse");
            trous.Add("Trou4Adverse");
            trous.Add("Trou5Adverse");
            trous.Add("Trou6Adverse");
        }

        public string Next(string element)
        {
            int index = trous.FindIndex(item => element.Equals(item));
            return trous.ElementAt(index);
        }
    }
}
