
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Xaml;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinLabMaps.ViewModel;

namespace XamarinLabMaps.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PinMapView : ContentPage
	{
        
        public PinMapView ()
		{
			InitializeComponent ();
            BindingContext = LabMapsViewModel.GetInstance();
          
        }
	}
}