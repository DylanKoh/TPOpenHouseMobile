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
    public partial class QuestionMenu : ContentPage
    {
        User _user;
        //Comment in QuestionMenu
        public QuestionMenu(User user)
        {
            InitializeComponent();
            _user = user;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await DisplayAlert("Disclaimer", "You can only take each quiz once!", "Ok");
            await CheckQuizzes();
        }
        private async Task CheckQuizzes()
        {
            var client = new WebApi();
            var response = await client.Post($"Questions/CheckCategory?userID={_user.userID}", "");
            var questionTaken = JsonConvert.DeserializeObject<List<string>>(response);
            foreach (var item in questionTaken)
            {
                if (item == "Course")
                {
                    btnCourses.IsVisible = false;
                }
                else if (item == "TP")
                {
                    btnTP.IsVisible = false;
                }
            }
            if (btnCourses.IsVisible == false && btnTP.IsVisible == false)
            {
                lblCompleted.IsVisible = true;
            }
        }

        private async void btnCourses_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseQuiz(_user));
            btnCourses.IsVisible = false;
            if (btnCourses.IsVisible == false && btnTP.IsVisible == false)
            {
                lblCompleted.IsVisible = true;
            }
        }

        private async void btnTP_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TPQuiz(_user));
            btnTP.IsVisible = false;
            if (btnCourses.IsVisible == false && btnTP.IsVisible == false)
            {
                lblCompleted.IsVisible = true;
            }
        }
    }
}