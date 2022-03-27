using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Reflection;
using AmoSim2.Player;

namespace AmoSim2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public PlayerViewModel PlayerViewModel { get; } = new PlayerViewModel();
        public PlayerModel Player { get; set; }

        //[PreferredConstructorAttribute]
        public MainViewModel()
        {
            AddClickCommand = new DelegateCommand(this.OnAddButtonClick);   
        }
        public ICommand AddClickCommand { get; private set; }

        private void OnAddButtonClick()
        {

        }
    }
}
