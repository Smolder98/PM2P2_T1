using PM2P2_T1.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2P2_T1.ViewModel
{
    public class InfoViewModel : BaseViewModel
    {

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string capital;

        public string Capital
        {
            get { return capital; }
            set { capital = value; OnPropertyChanged(); }
        }

        private string population;

        public string Population
        {
            get { return population; }
            set { population = value; OnPropertyChanged(); }
        }

        private string flag;

        public string Flag
        {
            get { return flag; }
            set { flag = value; OnPropertyChanged(); }
        }

        private List<string> currencies;

        public List<string> Currencies
        {
            get { return currencies; }
            set { currencies = value; OnPropertyChanged(); }
        }

        private List<string> languages;

        public List<string> Languages
        {
            get { return languages; }
            set { languages = value; OnPropertyChanged(); }
        }

        public Command SharedCountry { get; set; }
        public Command MoreCountry { get; set; }

        private Country country;

        public InfoViewModel(Country country)
        {
            this.country = country;

            SetData();

            SharedCountry = new Command(async() => await sharedCountry());
            MoreCountry = new Command(async() => await moreCountry());
        }

        private void SetData()
        {
            Name = country.NameCodeCountry;
            Capital = country.capital.Trim();
            Population = $"{country.population:N0}";
            Flag = country.flags.svg;

            Currencies = new List<string>();

            foreach (var item in country.currencies)
            {
                Currencies.Add("Name: "+item.name + "\nSymbol: " + item.symbol);
            }

            Languages = new List<string>();

            foreach (var item in country.languages)
            {
                Languages.Add(item);
            }
        }

        private async Task sharedCountry()
        {
            

            try
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = country.maps.googleMaps,
                    Title = "Shared Country: " + country.NameCountry.official
                });
            }
            catch { }        
            
        }

        private async Task moreCountry()
        {


            try
            {
                await Browser.OpenAsync(country.maps.openStreetMaps, BrowserLaunchMode.SystemPreferred);
            }
            catch { }

        }

    }
}
