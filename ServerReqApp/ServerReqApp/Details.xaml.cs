using ServerReqApp.API;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ServerReqApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        public OpenWeather weather = null;

        public List<string> List1 = null;

        public Details(List<string> list)
        {
            InitializeComponent();

            this.BindingContext = this;

            List1 = list;

            scroll1.ItemsSource = null;
            scroll1.ItemsSource = List1;
        }

        //public static object GetPropValue(object src, string propName)
        //{
        //    return src.GetType().GetProperty(propName).GetValue(src, null);
        //}
    }
}