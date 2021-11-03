using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Weather.Prism.ItemViewModel;
using Weather.Prism.Models;
using Weather.Prism.Views;
namespace Weather.Prism.ViewModels
{
    public class WeatherMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public WeatherMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_person",
                    PageName = $"{nameof(LoginPage)}",
                    Title = "Login",
                    IsLoginRequired = true,
                },
                new Menu
                {
                    Icon = "ic_weather",
                    PageName = $"{nameof(WeatherPage)}",
                    Title = "Weather"
                },
                new Menu
                {
                    Icon = "ic_about",
                    PageName = $"{nameof(AboutPage)}",
                    Title = "About",
                    IsLoginRequired = false
                },
                new Menu
                {
                    Icon = "ic_privacy",
                    PageName = $"{nameof(Privacy)}",
                    Title = "Privacy",
                    IsLoginRequired = false
                }
            };
            Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel(_navigationService)
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title,
                IsLoginRequired = m.IsLoginRequired
            }).ToList());
        }


    }
}
