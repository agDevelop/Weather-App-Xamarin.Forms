using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ServerReqApp.API;
using Newtonsoft.Json;

namespace ServerReqApp
{
    public partial class MainPage : ContentPage
    {
        private OpenWeather weather = null;

        public List<string> WeatherList { get; set; }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public MainPage()
        {
            InitializeComponent();

            WeatherList = new List<string> { "Потяните вниз, чтобы получить актуальную погоду" };

            this.BindingContext = this;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await UpdateWeather();

                    IsRefreshing = false;
                });
            }
        }

        public async Task UpdateWeather()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                
                Uri serverUri = new Uri("http://api.openweathermap.org");
                
                Uri relativeUri = new Uri("data/2.5/weather?q=Podolsk&appid=64bc9c5f639ff73c8de7f689df34817b", UriKind.Relative);

                Uri fullUri = new Uri(serverUri, relativeUri);

                request.RequestUri = fullUri;

                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    string s = await responseContent.ReadAsStringAsync();

                    weather = JsonConvert.DeserializeObject<OpenWeather>(s);

                    WeatherList.Clear();

                    WeatherList.AddRange(new string[] {
                    $"Координаты {weather.coord.lat},{weather.coord.lon}",
                    $"Погода {weather.weather[0].description} (подробнее)",
                    $"Температура {GetCelsius(weather.main.temp)} С (подробнее)",
                    $"Облачность {weather.clouds.all}",
                    $"Общая информация {weather.sys.country}",
                    $"Город {weather.name}"});
                }

                weatherScroll.ItemsSource = null;
                weatherScroll.ItemsSource = WeatherList;
            }
            catch (Exception)
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так...", "Ок");
            }
        }

        public async void Button1_Click(object sender, EventArgs e)
        {

            await UpdateWeather();
        }

        private async void weatherScroll_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                ListView listView = (ListView)sender;

                var item = listView.SelectedItem;

                List<string> list = new List<string>();

                switch (WeatherList.IndexOf(Convert.ToString(item)))
                {
                    case 1:

                        list.Add(weather.weather[0].description);
                        list.Add(weather.weather[0].main);

                        await Navigation.PushAsync(new Details(list));

                        break;
                    case 2:

                        list.Add($"Температура {GetCelsius(weather.main.temp)}");
                        list.Add($"Ощущается {GetCelsius(weather.main.feels_like)}");
                        list.Add($"Макс {GetCelsius(weather.main.temp_max)}");
                        list.Add($"Мин {GetCelsius(weather.main.temp_min)}");
                        list.Add($"Давление {Math.Round(weather.main.pressure)}");
                        list.Add($"Влажность {weather.main.humidity} %");

                        await Navigation.PushAsync(new Details(list));

                        break;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так...", "Ок");
            }
        }

        double GetCelsius(double k)
        {
            return Math.Round(k - 273.15);
        }
    }
}
