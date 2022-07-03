using PM2P2_T1.Model;
using PM2P2_T1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PM2P2_T1.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        private ObservableCollection<Country> countries = new ObservableCollection<Country>();

        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set { countries = value; OnPropertyChanged(); }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {
            //GetCountries();

        }

        public async void GetCountries(string region)
        {
            var countries = await CountryServices.GetCountries(region);
            
            var list = new ObservableCollection<Country>();

            foreach (Country item in countries)
            {
                list.Add(item);
            }

            Countries = list;

            Count = Countries.Count;
        }
    }
}
