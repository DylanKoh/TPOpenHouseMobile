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
    public partial class CourseQuiz : ContentPage
    {
        User _user;
        List<Question> _questionSet;
        int numberOfQuestion = 0;
        int questionIndex = 0;
        public CourseQuiz(User user)
        {
            InitializeComponent();
            _user = user;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetQuestions();
        }

        private async Task GetQuestions()
        {
            var client = new WebApi();
            var response = await client.Post($"Questions/GetCategoryQuestions?category=Course", "");
            _questionSet = JsonConvert.DeserializeObject<List<Question>>(response);
            numberOfQuestion = _questionSet.Count();
            PopulateQuestion();
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            var client = new WebApi();
            var userResponse = new UserResponse() { userIDFK = _user.userID, questionIDFK = _questionSet[questionIndex].ID };
            if (rbAnswerOne.IsChecked)
            {
                userResponse.userAnswer = lblAnswerOne.Text;
            }
            else if (rbAnswerTwo.IsChecked)
            {
                userResponse.userAnswer = lblAnswerTwo.Text;
            }
            else if (rbAnswerThree.IsChecked)
            {
                userResponse.userAnswer = lblAnswerThree.Text;
            }
            else if (rbAnswerFour.IsChecked)
            {
                userResponse.userAnswer = lblAnswerFour.Text;
            }

            if (userResponse.userAnswer == _questionSet[questionIndex].Correct)
            {
                userResponse.isCorrect = true;
            }
            else
            {
                userResponse.isCorrect = false;
            }
            var JsonData = JsonConvert.SerializeObject(userResponse);
            var response = await client.Post("UserResponses/Create", JsonData);
            if (response == "\"Congrats! Your answer was correct!\"")
            {
                await DisplayAlert("Feedback", "Congrats! Your answer was correct!", "Ok");
                if (questionIndex == numberOfQuestion - 1)
                {
                    await DisplayAlert("End of quiz", "You have completed the last question! Redirecting you back to Quiz Menu!", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    questionIndex += 1;
                    ClearSelections();
                    PopulateQuestion();
                }
            }
            else if (response == "\"Your answer is incorrect!\"")
            {
                await DisplayAlert("Feedback", $"Your answer is incorrect! The correct answer is: {_questionSet[questionIndex].Correct}", "Ok");
                if (questionIndex == numberOfQuestion - 1)
                {
                    await DisplayAlert("End of quiz", "You have completed the last question! Redirecting you back to Quiz Menu!", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    questionIndex += 1;
                    ClearSelections();
                    PopulateQuestion();
                }
            }
            else if (response == "\"Unable to submit response!Please try again later!\"")
            {
                await DisplayAlert("Feedback", "Unable to submit response! Please try again later!", "Ok");
                await Navigation.PopAsync();
            }
        }

        private void ClearSelections()
        {
            rbAnswerOne.IsChecked = false;
            rbAnswerTwo.IsChecked = false;
            rbAnswerThree.IsChecked = false;
            rbAnswerFour.IsChecked = false;
        }

        private void PopulateQuestion()
        {
            lblAnswerOne.Text = _questionSet[questionIndex].AnswerOne;
            lblAnswerTwo.Text = _questionSet[questionIndex].AnswerTwo;
            lblAnswerThree.Text = _questionSet[questionIndex].AnswerThree;
            lblAnswerFour.Text = _questionSet[questionIndex].AnswerFour;
            lblQuestion.Text = _questionSet[questionIndex].questionString;
        }
    }

}