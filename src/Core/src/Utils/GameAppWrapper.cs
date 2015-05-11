using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.Model;
using DomainModel.Model.Player;

namespace Core.src.Utils
{
    public class GameAppWrapper : IGameApp
    {
        protected readonly IGameApp original;

        public event EventHandler GameExit;

        public Player P1
        {
            get;
            set;
        }

        public Player P2
        {
            get;
            set;
        }
        

        public Int32 Id
        {
            get;
            protected set;
        }

        public String GameName
        {
            get;
            protected set;
        }

        public DelegateCommand LaunchGameCommand
        {
            get;
            set;
        }


        public GameAppWrapper(IGameApp original, Int32 Id, String name)
        {
            this.original = original;

            this.original.GameExit += original_GameExit;
            this.Id = Id;
            this.GameName = name;
        }

        void original_GameExit(object sender, EventArgs e)
        {
            this.GameExit(this, e);
        }

        public void CloseWindow()
        {
            original.CloseWindow();
        }

        public void OpenWindow()
        {
            original.OpenWindow();
        }

        public void Initialize(Player p1, Player p2)
        {
            this.original.Initialize(p1, p2);
        }

        public void Initialize()
        {
            this.original.Initialize(this.P1, this.P2);
        }


        public DomainModel.GameTypes GameType
        {
            get { return original.GameType; }
        }
    }
}
