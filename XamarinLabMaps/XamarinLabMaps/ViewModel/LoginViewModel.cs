using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinLabMaps.View;

namespace XamarinLabMaps.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        private string _Username { get; set; }
        public string Username { get { return _Username; } set{ _Username = value; OnPropertyChanged("Username"); } }

        private string _Password { get; set; }
        public string Password { get { return _Password; } set { _Password = value; OnPropertyChanged("Password"); } }

        private bool _RememberUser { get; set; }
        public bool RememberUser { get { return _RememberUser; } set { _RememberUser = value; OnPropertyChanged("RememberUser"); } }

        #region Singleton
        private static LoginViewModel instance = null;
        public LoginViewModel()
        {
            InitClass();
            InitCommands();
           
        }

        private void InitClass()
        {
            Username = "";
            Password = "";
        }

        public static LoginViewModel GetInstance()
        {

            if (instance == null)
                instance = new LoginViewModel();

            
            return instance;
        }
        #endregion

        public ICommand SignInClickedCommand { get; set; }
        private void InitCommands()
        {
            SignInClickedCommand = new Command(SignInClicked);
           

        }
        private void SignInClicked() {
            if (Username == "diego" && Password == "123")
            {
                NavigationPage navigation = new NavigationPage(new MasterContent());

                App.Current.MainPage = new MasterDetailPage
                {
                    Master = new MenuPage(),
                    Detail = navigation
                };
                LabMapsViewModel.GetInstance().Username = Username;
                LabMapsViewModel.GetInstance().RememberUser = RememberUser;
            }
            else {
                Application.Current.MainPage.DisplayAlert("Invalid Credentials", "Invalid Username/Password", "OK");
            }
        }
        #region INotifyPropertyChange Interface Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {

            if (propertyName != null && PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
