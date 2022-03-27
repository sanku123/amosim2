using AmoSim2.Battle;
using AmoSim2.Monster;
using AmoSim2.Player;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EnemyViewModel>();

            SimpleIoc.Default.Register<PlayerViewModel>();
            SimpleIoc.Default.Register<BattleViewModel>();
            SimpleIoc.Default.Register<MonsterViewModel>();

        }

        private static ViewModelLocator _instance = null;

        public static ViewModelLocator Instance
        {
            get
            {
                if (_instance == null) _instance = new ViewModelLocator();
                return _instance;
            }
        }
        public PlayerViewModel HubViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayerViewModel>();
            }
        }
        public EnemyViewModel EnemyView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EnemyViewModel>();
            }
        }
        public BattleViewModel BattleView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BattleViewModel>();
            }
        }
        public MonsterViewModel MonsterView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MonsterViewModel>();
            }
        }

    }
}
