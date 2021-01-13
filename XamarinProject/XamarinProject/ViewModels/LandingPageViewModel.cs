using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using XamarinProject.Models;

namespace XamarinProject.ViewModels
{
     class LandingPageViewModel : INotifyPropertyChanged
    {

        #region Singleton

        public static LandingPageViewModel instance = null;

        public LandingPageViewModel()
        {
            InitCommands();
            InitClass();

        }

        public static LandingPageViewModel getInstance()
        {
            if (instance == null)
            {
                instance = new LandingPageViewModel();
            }

            return instance;
        }

        public static void deleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }


        #endregion


        #region Variables

        private ObservableCollection<NewsModel> news = new ObservableCollection<NewsModel>();

        public ObservableCollection<NewsModel> News
        {
            get { return news; }

            set
            {
                news = value;
                OnPropertyChanged("News");
            }
        }


        #endregion

        #region Inits

        public void InitClass()
        {
            this.News.Add(new NewsModel { Header="Promo MTG 1", Image="noticia2.jpg", Footer=" Precio regular: $73.49",Description="Boosters Variados de MTG"});
            this.News.Add(new NewsModel { Header = "Promo MTG 2", Image = "noticia3.jpg", Footer = " Precio regular: $81.49", Description = "Boosters Variados de MTG" });
            this.News.Add(new NewsModel { Header = "Promo PKM 1", Image = "noticia4.jpg", Footer = " Precio regular: $27.49", Description = "Deck Base y Sleves" });
            this.News.Add(new NewsModel { Header = "Promo Funko", Image = "noticia5.jpg", Footer = " Precio regular: $56.99", Description = "Set de Funkos" });

        }

        private void InitCommands()
        {
           
        }
        #endregion

        #region INotifyPropertyChanged Implentation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
