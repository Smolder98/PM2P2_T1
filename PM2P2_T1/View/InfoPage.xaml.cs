using PM2P2_T1.Model;
using PM2P2_T1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2P2_T1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {

        InfoViewModel infoViewModel;
        Country country;

        public InfoPage(Country country)
        {
            InitializeComponent();


            infoViewModel = new InfoViewModel(country);

            this.BindingContext = infoViewModel;

            this.country = country;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status == PermissionStatus.Granted)
                {

                    var localizacion = await Geolocation.GetLocationAsync();

                    if (localizacion != null)
                    {

                        var pin = new Pin()
                        {
                            Type = PinType.SavedPin,
                            Position = new Position(country.latlng[0], country.latlng[1]),
                            Label = "Country",
                            Address = country.NameCountry.official

                        };


                        map.Pins.Add(pin);

                        //mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(localizacion.Latitude, localizacion.Longitude), Distance.FromMeters(100)));


                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(country.latlng[0], country.latlng[1]), Distance.FromMeters(500000)));

                    }
                }
                else
                {
                    await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                     OnBackButtonPressed();


                }

                
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Location services are not enabled on device."))
                {

                    Message("Error", "Servicio de localizacion no encendido");
                }
                else
                {
                    Message("Error", e.Message);

                }

                OnBackButtonPressed();
            }


        }

        public async void Message(string title, string msg)
        {
            await DisplayAlert(title, msg, "OK");
        }


    }
}