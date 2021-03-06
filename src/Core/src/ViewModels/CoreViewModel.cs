﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

using Core.src.Utils;
using DomainModel.Model;
using DomainModel.Contracts;

using AI;

using TicTacToeGameApp;


using System.Collections.ObjectModel;

namespace Core.src.ViewModels
{
    class CoreViewModel : BaseViewModel
    {
        protected CoreModel model;

        public event EventHandler<IGameApp> LaunchGame;

        public DelegateCommand showLoadModule
        {
            get;
            protected set;
        }

        public DelegateCommand setAICommand
        {
            get;
            protected set;
        }

        public DelegateCommand showScoreWindow
        {
            get;
            protected set;
        }

        public ObservableCollection<GameAppWrapper> GameList
        {
            get
            {
                return model.Games;
            }
        }

        public EventHandler<Int32> showScoreEvent;

        public Int32 GameNum
        {
            get
            {
                return model.GameNum;
            }
        }

        public CoreViewModel()
        {
            model = new CoreModel();

            model.LaunchGame += onGameLaunch;

            showLoadModule = new DelegateCommand(param => { onLoadModule(); });
            setAICommand = new DelegateCommand(param => { onSetAI(param); });
            showScoreWindow = new DelegateCommand(param => { onShowScore(param); });

            // Register Game applications

            model.register(new TicTacToeGameApp.TicTacToeGameApp(), "TicTacToe");

        }

        private void onShowScore(object param)
        {
            Int32 code = Int32.Parse((String)param);
            showScoreEvent(this,code);
        }

        private void onLoadModule()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Module file|.dll";
            dlg.CheckPathExists = true;
            dlg.Multiselect = false;
            DialogResult dr = dlg.ShowDialog();

            if(dr == DialogResult.OK)
            {
                String filename = dlg.FileName;
                IGameApp gameApp = Activator.CreateInstanceFrom(filename, "GameApp").Unwrap() as IGameApp;
                model.register(gameApp, "Egyedi Játék");
                OnPropertyChanged("GameList");
            }
        }

        private void onSetAI(object param)
        {
            Int32 Param = Int32.Parse((String)param);
            switch(Param)
            {
                case 0:
                    model.removeAI();
                    break;
                case 1:
                    AlphaBetaAI abAI = new AlphaBetaAI();
                    model.setAI(abAI);
                    break;
                case 2:
                    GeneralAI genAI = new GeneralAI();
                    model.setAI(genAI);
                    break;
                case 3:
                    MinimaxAI mmAI = new MinimaxAI();
                    model.setAI(mmAI);
                    break;
            }
        }

        private void onGameLaunch(object sender,Int32 param)
        {
            Int32 Id = param;
            GameAppWrapper current = GameList[(int)Id];

            current.P1 = model.P1;
            current.P2 = model.P2;

            LaunchGame(this, current);
        }
    }
}
