using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinLabMaps.View;
using Xamarin.Forms;

namespace XamarinLabMaps
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();


            //NavigationPage navigation = new NavigationPage(new MasterContent());

            //App.Current.MainPage = new MasterDetailPage
            //{
            //    Master = new MenuPage(),
            //    Detail = navigation
            //};
            App.Current.MainPage = new LoginView();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
