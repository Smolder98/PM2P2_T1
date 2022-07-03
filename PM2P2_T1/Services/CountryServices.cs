using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PM2P2_T1.Model;

namespace PM2P2_T1.Services
{
    public class CountryServices
    {
        public static string endpoint = "https://restcountries.com/v3.1/region/";

        public async static Task<List<Country>> GetCountries(string region)
        {

            try
            {
                List<Country> countries = new List<Country>();

                using (HttpClient client = new HttpClient())
                {
                    var resp = await client.GetAsync(endpoint + region);

                    if (resp.IsSuccessStatusCode)
                    {
                        var content =  resp.Content.ReadAsStringAsync().Result;

                        JArray results = JArray.Parse(content);

                        Country temp = null;

                        foreach (var item in results)
                        {
                            temp = new Country();
  
                            var curTemp =new List<Country.Currencies>();
                            foreach (var current in item["currencies"].Values())
                            {
                                try
                                {
                                    curTemp.Add(new Country.Currencies()
                                    {
                                        name = current["name"].ToString(),
                                        symbol = current["symbol"].ToString()
                                    });
                                }
                                catch 
                                {
                                    curTemp.Add(new Country.Currencies()
                                    {
                                        name = current["name"].ToString(),
                                        symbol = "--"
                                    });

                                }
                            }
                        
                            var languTemp = new List<string>();
                            foreach (var current in item["languages"].Values())
                                languTemp.Add(current.ToString());

                            var timeTemp = new List<string>();
                            foreach (var current in item["timezones"].Values())
                                timeTemp.Add(current.ToString());
                            
                            var contTemp = new List<string>();
                            foreach (var current in item["continents"].Values())
                            {
                                contTemp.Add(current.ToString());
                            }

                          
                            temp.NameCountry = new Country.Name()
                            {
                                common = item["name"]["common"].ToString(),
                                official = item["name"]["official"].ToString()

                            };

                            try
                            {
                                temp.independent = item["independent"].ToString(); //Europa
                            }
                            catch { temp.independent = "--"; }

                            temp.status = item["status"].ToString();

                            temp.currencies = curTemp;

                            try
                            {
                                temp.capital = item["capital"][0].ToString(); //asia
                            }
                            catch { temp.capital = "--"; }
                            

                            temp.region = item["region"].ToString();

                            temp.subregion = item["subregion"].ToString();

                            temp.languages = languTemp;

                            temp.latlng = new List<double>() { (double)item["latlng"][0], (double)item["latlng"][1] };

                            temp.flag = item["flag"].ToString();

                            temp.maps = new Country.Maps()
                            {
                                googleMaps = item["maps"]["googleMaps"].ToString(),
                                openStreetMaps = item["maps"]["openStreetMaps"].ToString()
                            };

                            temp.population = (int)item["population"];

                            temp.timezones = timeTemp;

                            temp.continents = contTemp;

                            temp.flags = new Country.Flags()
                            {
                                png = item["flags"]["png"].ToString(),
                                svg = item["flags"]["svg"].ToString()
                            };

                            temp.startOfWeek = item["startOfWeek"].ToString();

                            //temp.capitalInfo = new List<double>() { (double)item["capitalInfo"]["latlng"][0], (double)item["capitalInfo"]["latlng"][1] };

                            

                            countries.Add(temp);
                        }
                    }

                }

                

                return countries.OrderBy(x => x.NameCountry.common).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

                return null;
            }

    
        }
    }
}
