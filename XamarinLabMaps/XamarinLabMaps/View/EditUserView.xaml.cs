﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinLabMaps.ViewModel;

namespace XamarinLabMaps.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditUserView : ContentPage
	{
		public EditUserView ()
		{
			InitializeComponent ();
            BindingContext = LabMapsViewModel.GetInstance();
		}
	}
}