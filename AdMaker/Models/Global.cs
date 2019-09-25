using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMaker
{
    class Global
    {
        MainWindow form;
        GlobalViewModel globalViewModel;

        public Global(MainWindow form, GlobalViewModel globalViewModel)
        {
            //Установка обработчика
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(L.AppDomain_CurrentDomain_UnhandledException);
            //System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(L.Application_ThreadException);

            this.form = form;
            form.Closing += Form_Closing;
            this.globalViewModel = globalViewModel;
        }

        private void Form_Closing(object sender, CancelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        internal void OnSendCommandHandler(string commandName)
        {
            switch (commandName)
            {
                case "AddAdd":
                    AddAdd();
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        private void AddAdd()
        {
            throw new NotImplementedException();
        }
    }
}
