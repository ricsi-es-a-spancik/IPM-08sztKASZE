using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.src.InterFaces;
using Core.src.Structures;

namespace Core.src.Utils
{
    class GameAppWrapper : IGameApp
    {
        protected readonly IGameApp original;
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


        public GameAppWrapper(IGameApp original, Int32 Id)
        {
            this.original = original;

            this.original.GameOver += original_GameOver;
            this.Id = Id;
        }

        void original_GameOver(object sender, EventArgs e)
        {
            this.GameOver(this, e);
        }

        public void Initialize(Structures.Player p1, Structures.Player p2)
        {
            this.original.Initialize(p1, p2);
        }

        public void Initialize()
        {
            this.original.Initialize(this.P1, this.P2);
        }


        public event EventHandler GameOver;
    }
}
