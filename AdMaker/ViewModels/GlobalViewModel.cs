using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace AdMaker
{
    class GlobalViewModel : INotifyPropertyChanged
    {
        public delegate void CommandHandler(string commandName);
        public event CommandHandler OnSendCommand;

        private MainWindow f;
        private Global global;

        public GlobalViewModel(MainWindow f)
        {
            this.f = f;
            global = new Global(f, this);
            OnSendCommand += global.OnSendCommandHandler;
            //OnSendCommandDelMessage += global.OnSendDelMessageCommandHandler;
            //OnSendCommandGoToMessage += global.OnSendGoToMessageCommandHandler;
            //OnSendCommandWithObject += global.OnSendCommandWithObjectCommandHandler;
        }

        public RelayCommand UniversalCommand
        {
            get
            {
                RelayCommand rc = new RelayCommand(obj =>
                {
                    string commandName = obj.ToString();
                    OnSendCommand(commandName);
                });
                return rc;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
