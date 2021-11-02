using Prism;
using Prism.Ioc;
using Weather.Prism.Services;
using Weather.Prism.ViewModels;
using Weather.Prism.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Weather.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //await NavigationService.NavigateAsync
            //        ($"/{nameof(WeatherMasterDetailPage)}/NavigationPage/{nameof(WeatherPage)}");
            await NavigationService.NavigateAsync("NavigationPage/WeatherPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.Register<IApiService, ApiService>();//To use the new interface
            containerRegistry.RegisterForNavigation<NavigationPage>();
           
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();//it was created automatically when the page was created
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>(); 
            
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
           
            containerRegistry.RegisterForNavigation<WeatherMasterDetailPage, WeatherMasterDetailPageViewModel>();
        }
    }
}
