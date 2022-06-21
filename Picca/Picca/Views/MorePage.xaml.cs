﻿using Picca.Models;
using Picca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Picca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        public MorePage()
        {
            InitializeComponent();
        }

        private async void FrameOut_Tapped(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Подтвердить действие", "Вы действительно хотите выйти из аккаунта?", "Да", "Нет");
            if(result == true)
            {
                Preferences.Clear();
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
                await Shell.Current.GoToAsync($"//{nameof(AuthorizationPage)}");

            }
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            string login = Preferences.Get("Login", string.Empty);
            var user = await new UserService().GetUserByLogin(login);
            PrivetLabel.Text = $"Привет, {user.Name}";
        }
    }
}