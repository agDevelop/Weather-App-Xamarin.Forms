using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ServerReqApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationPage page = new NavigationPage(new MainPage())
            {
                Title = "Laba1 Goncharov 181-321",
            };

            MainPage = page;
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
