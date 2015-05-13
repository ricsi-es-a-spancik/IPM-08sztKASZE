using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.src.Utils;

namespace Core.src.ViewModels
{
    class InputDialogViewModel
    {
        public InputDialog dia;

        public DelegateCommand Ok
        {
            get;
            protected set;
        }
        public DelegateCommand Cancel
        {
            get;
            protected set;
        }

        public EventHandler CloseDialog;

        public Boolean result;

        public InputDialogViewModel(InputDialog dia)
        {
            Ok = new DelegateCommand(param => { onOK(); });
            Cancel = new DelegateCommand(param => { onCancel(); });
            this.dia = dia;
        }

        private void onCancel()
        {
            this.result = false;
            CloseDialog(this, EventArgs.Empty);
        }

        private void onOK()
        {
            this.result = true;
            CloseDialog(this, EventArgs.Empty);
        }
    }
}
