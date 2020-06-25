using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TPOpenHouseMobile.GlobalClass;

namespace TPOpenHouseMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            if (entryName.Text == null || entryUserID.Text == null || entryPassword == null || entryRePassword == null)
            {
                await DisplayAlert("Create Account", "One of more field(s) are empty!", "Ok");
            }
            else if (entryPassword.Text != entryRePassword.Text)
            {
                await DisplayAlert("Create Account", "Your passwords do not match! Please check and try again!", "Ok");
            }
            else
            {
                var newUser = new User() { userName = entryName.Text, userID = entryUserID.Text, password = entryPassword.Text, Points = 0 };
                var client = new WebApi();
                var JsonData = JsonConvert.SerializeObject(newUser);
                var response = await client.Post("Users/Create", JsonData);
                if (response == "\"User ID has been used!\"") 
                {
                    await DisplayAlert("Create Account", "User ID has been used!", "Ok");
                }
                else if (response == "\"Unable to create user account! Please contact our administrator!\"")
                {
                    await DisplayAlert("Create Account", "Unable to create user account! Please contact our administrator!", "Ok");
                }
                else
                {
                    await DisplayAlert("Create Account", "Account created successfully!", "Ok");
                    await Navigation.PopAsync();
                }
            }
        }
    }
}