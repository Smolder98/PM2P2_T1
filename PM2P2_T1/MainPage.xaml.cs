using PM2P2_T1.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2P2_T1
{
    public partial class MainPage : ContentPage
    {
        MainViewModel mainViewModel;

        public MainPage()
        {
            InitializeComponent();

            mainViewModel = new MainViewModel();

            this.BindingContext = mainViewModel;
        }

        private void listCountries_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        private async void pickerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var region = pickerRegion.SelectedItem as string;

            var internetAccess = Connectivity.NetworkAccess;
            if (internetAccess == NetworkAccess.Internet)
            {
                mainViewModel.GetCountries(region);
            }
            else
            {
                await DisplayAlert("Aviso", "No tiene acceso a internet", "OK");
            }
        }
    }
}
