using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TPOpenHouseMobile;
using static TPOpenHouseMobile.GlobalClass;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;

namespace TPOpenHouseMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (entryUserID.Text == null || entryPassword.Text == null || entryUserID.Text == "" || entryPassword.Text == "")
            {
                await DisplayAlert("Login", "Please check your fields! One or both of the fields are empty!", "Ok");
            }
            else
            {
                var client = new WebApi();
                var response = await client.Post($"Users/Login?userID={entryUserID.Text}&password={entryPassword.Text}", "");
                if (response == "\"User account does not exist!\"")
                {
                    await DisplayAlert("Login", "User account does not exist!", "Ok");
                }
                else if (response == "\"Password is incorrect! Please try again!\"")
                {
                    await DisplayAlert("Login", "Password is incorrect! Please try again!", "Ok");
                }
                else
                {
                    var userData = JsonConvert.DeserializeObject<User>(response);
                    await DisplayAlert("Login", $"Welcome {userData.userName}!", "Ok");
                    await Navigation.PushAsync(new MainMenu(userData));
                }
            }
            
        }

        private async void btnCreateAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccount());
        }
    }
}
