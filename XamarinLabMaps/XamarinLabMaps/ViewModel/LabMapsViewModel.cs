using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinLabMaps.Model;
using XamarinLabMaps.View;

namespace XamarinLabMaps.ViewModel
{
    public class LabMapsViewModel : INotifyPropertyChanged
    {
        #region Singleton
        private static LabMapsViewModel instance = null;
        public LabMapsViewModel()
        {
            InitClass();
            InitCommands();
            InitMenu();
        }

        public static LabMapsViewModel GetInstance()
        {

            if (instance == null)
                instance = new LabMapsViewModel();

            return instance;
        }
        #endregion
        #region Instances
        private ObservableCollection<MenuItem> _InitialMenu = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> InitialMenu
        {
            get
            {
                return _InitialMenu;
            }

            set
            {
                _InitialMenu = value;
                OnPropertyChanged("InitialMenu");

            }
        }
        private string _Username { get; set; }
        public string Username { get { return _Username; } set { _Username = value; OnPropertyChanged("Username"); } }

        private bool _RememberUser { get; set; }
        public bool RememberUser { get { return _RememberUser; } set { _RememberUser = value; OnPropertyChanged("RememberUser"); } }

        private Map _MyMap { get; set; }
        public Map MyMap { get { return _MyMap; } set { _MyMap = value; OnPropertyChanged("MyMap"); } }

        private string _NewPinName { get; set; }
        public string NewPinName { get { return _NewPinName; } set { _NewPinName = value; OnPropertyChanged("NewPinName"); } }

        private string _NewPinDescription { get; set; }
        public string NewPinDescription { get { return _NewPinDescription; } set { _NewPinDescription = value; OnPropertyChanged("NewPinDescription"); } }

        private double _NewPinLongitude { get; set; }
        public double NewPinLongitude { get { return _NewPinLongitude; } set { _NewPinLongitude = value; OnPropertyChanged("NewPinLongitude"); } }

        private double _NewPinLatitude { get; set; }
        public double NewPinLatitude { get { return _NewPinLatitude; } set { _NewPinLatitude = value; OnPropertyChanged("NewPinLatitude"); } }

        private Position _myPosition = new Position(-37.8141, 144.9633);
        public Position MyPosition { get { return _myPosition; } set { _myPosition = value; OnPropertyChanged("MyPosition"); } }

        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection { get { return _pinCollection; } set { _pinCollection = value; OnPropertyChanged("PinCollection"); } }
        #endregion
        #region commands
        public ICommand MenuItemClickedCommand { get; set; }
        public ICommand SignOutClickedCommand { get; set; }
        public ICommand AddNewPinTapCommand { get; set; }
        public ICommand TapOnListedPinCommand { get; set; }
        public ICommand MapTapOnListPinCommand { get; set; }
        public ICommand AddTestPinLocationsCommand { get; set; }
        #endregion
        #region Inits
        private async void InitClass()
        {
            RememberUser = false;

            var position = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync();
            MyPosition = new Position(position.Latitude, position.Longitude);

            PinCollection.Add(new Pin() { Position = MyPosition, Type = PinType.Place,  Label = "My Current Position", Address="Initial Position" });
         
        }
        private void InitCommands()
        {
            MenuItemClickedCommand = new Command<int>(MenuItemClicked);
            SignOutClickedCommand = new Command(SignOutClicked);
            AddNewPinTapCommand = new Command(AddNewPinTap);
            TapOnListedPinCommand = new Command<string>(TapOnListedPin);
            MapTapOnListPinCommand = new Command<string>(MapTapOnListPin);
            AddTestPinLocationsCommand = new Command(AddTestPinLocations);
        }

        private void MapTapOnListPin(string pLabel)
        {
            Pin selectedPin = PinCollection.Where(x => x.Label == pLabel).FirstOrDefault();

            MyPosition = selectedPin.Position;
        }

        private void TapOnListedPin(string pLabel)
        {
            Pin selectedPin = PinCollection.Where(x => x.Label == pLabel).FirstOrDefault();

            MyPosition = selectedPin.Position;
          
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PinMapView());
            ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
        }

        private void AddNewPinTap()
        {
          

            var NewPinPosition = new Position(NewPinLatitude, NewPinLongitude); // Latitude, Longitude

            Pin NewPin = new Pin() { Position = NewPinPosition, Type = PinType.SavedPin, Label = NewPinName, Address = NewPinDescription };
            PinCollection.Add(NewPin);
            MyPosition= NewPinPosition;

            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
            ((MasterDetailPage)App.Current.MainPage).IsPresented = true;
          
        }

        private void SignOutClicked() {
            App.Current.MainPage = new LoginView();

        }
        private void MenuItemClicked(int menuID)
        {
            if (menuID == 1) {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AddNewPinView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 2)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PinListView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 3)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PinMapView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 4)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new EditUserView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
        }
        private void AddTestPinLocations()
        {
            var NewPinPosition = new Position(9.938248, -84.096195); // Latitude, Longitude

            Pin NewPin = new Pin() { Position = NewPinPosition, Type = PinType.SavedPin, Label = "OSullivan Culinary", Address = "Cooking Classes" };
            PinCollection.Add(NewPin);

            NewPinPosition = new Position(9.9323162, -84.0332226); // Latitude, Longitude

            NewPin = new Pin() { Position = NewPinPosition, Type = PinType.SavedPin, Label = "Universidad Cenfotec", Address = "Xamarin Classes" };
            PinCollection.Add(NewPin);

            NewPinPosition = new Position(9.8997754, -84.0710544); // Latitude, Longitude

            NewPin = new Pin() { Position = NewPinPosition, Type = PinType.SavedPin, Label = "Ex2 Outcoding", Address = "Work Office" };
            PinCollection.Add(NewPin);

            NewPinPosition = new Position(9.900609, -84.047928); // Latitude, Longitude

            NewPin = new Pin() { Position = NewPinPosition, Type = PinType.SavedPin, Label = "Home", Address = "Villa Karen 2" };
            PinCollection.Add(NewPin);


            MyPosition = NewPinPosition;

        }
        private void InitMenu()
        {
            InitialMenu = new ObservableCollection<MenuItem>() { new MenuItem() {ID=1, Logo="MapEditing_48px", Text="Add Map Pin", Description="This will take you to the Add Pin"},
                                                                 new MenuItem() {ID=2, Logo="MapMarker_48px", Text="See Pin List", Description="This will take you to the Pin List" },
                                                                 new MenuItem() {ID=3, Logo="MapPinpoint_48px", Text="See Map", Description="This will take you to the MAP" },
                                                                 new MenuItem() {ID=4, Logo="Settings_48px", Text="Edit User Preferences", Description="This will take you to the Edit User" }
                                                                  };

        }
        #endregion
       
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
