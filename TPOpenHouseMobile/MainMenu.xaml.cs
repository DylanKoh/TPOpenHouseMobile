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
    public partial class MainMenu : ContentPage
    {
        User _user;
        public MainMenu(User user)
        {
            InitializeComponent();
            _user = user;
            this.Title = $"Welcome {_user.userName}!";
        }

        private async void btnEventOutline_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventOutline());
        }

        private async void btnCourses_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Courses());
        }

        private async void btnQuizzes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuestionMenu(_user));
        }

        private void btnRewards_Clicked(object sender, EventArgs e)
        {

        }
    }
}