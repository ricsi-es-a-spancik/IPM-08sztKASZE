using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using Core.src.Utils;
using DomainModel;

namespace Core.src.ViewModels
{
    class ScoreViewModel : BaseViewModel
    {
        public ObservableCollection<LeaderboardEntry> Entries
        {
            get;
            protected set;
        }

        public EventHandler BackToMenu;

        public Core.src.Utils.DelegateCommand BackToMenuCommand
        {
            get;
            protected set;
        }

        public Int32 Size
        {
            get;
            protected set;
        }

        public ScoreViewModel(IEnumerable<LeaderboardEntry> inp)
        {
            Entries = new ObservableCollection<LeaderboardEntry>(inp.ToList());
            OnPropertyChanged("Entries");

            BackToMenuCommand = new Core.src.Utils.DelegateCommand(param => { onBackToMenu(); });

            Size = Entries.Count;
        }

        private void onBackToMenu()
        {
            BackToMenu(this,EventArgs.Empty);
        }

    }
}
