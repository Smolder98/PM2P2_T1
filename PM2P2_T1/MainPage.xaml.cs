using PM2P2_T1.Model;
using PM2P2_T1.ViewModel;
using System;
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

        private  async void listCountries_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var country = (Country)e.Item;

           await Navigation.PushModalAsync(new View.InfoPage(country));
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
