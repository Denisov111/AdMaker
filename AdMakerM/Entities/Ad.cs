using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdMakerM
{
    [Serializable]
    public class Ad : INotifyPropertyChanged
    {
        private decimal price;
        private string title;
        private string description;
        string imgFileName;
        string modImgFileName;
        string iconFileName;
        int articul;
        bool isPostedOnUla;
        bool isPostedOnAvito;
        bool isPostedOnAu;
        string linkOnUla;
        string linkOnAvito;
        string linkOnAu;

        public List<string> Guids = new List<string>();

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        public string ImgFileName
        {
            get { return imgFileName; }
            set
            {
                imgFileName = value;
                OnPropertyChanged();
            }
        }
        public string ModImgFileName
        {
            get { return modImgFileName; }
            set
            {
                modImgFileName = value;
                OnPropertyChanged();
            }
        }
        public string IconFileName
        {
            get { return iconFileName; }
            set
            {
                iconFileName = value;
                OnPropertyChanged();
            }
        }
        public int Articul
        {
            get { return articul; }
            set
            {
                articul = value;
                OnPropertyChanged();
            }
        }

        public bool IsPostedOnUla
        {
            get { return isPostedOnUla; }
            set
            {
                isPostedOnUla = value;
                OnPropertyChanged();
            }
        }

        public bool IsPostedOnAvito
        {
            get { return isPostedOnAvito; }
            set
            {
                isPostedOnAvito = value;
                OnPropertyChanged();
            }
        }

        public bool IsPostedOnAu
        {
            get { return isPostedOnAu; }
            set
            {
                isPostedOnAu = value;
                OnPropertyChanged();
            }
        }

        public string LinkOnUla
        {
            get { return linkOnUla; }
            set
            {
                linkOnUla = value;
                OnPropertyChanged();
            }
        }

        public string LinkOnAvito
        {
            get { return linkOnAvito; }
            set
            {
                linkOnAvito = value;
                OnPropertyChanged();
            }
        }

        public string LinkOnAu
        {
            get { return linkOnAu; }
            set
            {
                linkOnAu = value;
                OnPropertyChanged();
            }
        }


        public Ad()
        {
        }

        public void SetArticul()
        {
            Articul = CreateArticul();
        }

        private int CreateArticul()
        {
            Properties.Settings.Default.Articul = Properties.Settings.Default.Articul + 1;
            Properties.Settings.Default.Save();
            return Properties.Settings.Default.Articul;
        }

        #region INotifyPropertyChanged code

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
