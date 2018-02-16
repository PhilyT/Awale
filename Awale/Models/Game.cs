using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awale.Models
{
    public class Game :ModelBase
    {

        private List<string> trous;
        private Player playeur1;
        private Player playeur2;

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
            Playeur1 = new Player(true);
            Playeur2 = new Player(false);
        }

        public Player Playeur1 { get => playeur1; set => playeur1 = value; }
        public Player Playeur2 { get => playeur2; set { playeur2 = value; RaisePropertyChanged("Playeur2"); } }

        public string Next(string element)
        {
            int index = trous.FindIndex(item => element.Equals(item))+1;
            if(index == trous.Count)
            {
                return trous.ElementAt(0);
            }
            else
            {
                return trous.ElementAt(index);
            }
            
        }
        public string Previous(string element)
        {
            int index = trous.FindIndex(item => element.Equals(item)) - 1;
            if (index == -1)
            {
                return trous.ElementAt(trous.Count-1);
            }
            else
            {
                return trous.ElementAt(index);
            }
        }
    }
}
