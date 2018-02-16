﻿using Awale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Awale.ViewModels
{
    public class ViewModelMain : ViewModelBase
    {
        private Game game;
        //private RelayCommand relayCommand;
        //public RelayCommand RelayCommand { get => relayCommand; set => relayCommand = value; }
        public ViewModelMain()
        {
            //this.RelayCommand = new RelayCommand(o => OpenGameClick("MainButton"));
            Game = new Game();
        }

        public Game Game { get => game; set { game = value;RaisePropertyChanged("Game"); } }

        //private void OpenGameClick(object v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
