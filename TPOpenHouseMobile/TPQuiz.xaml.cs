using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TPOpenHouseMobile.GlobalClass;

namespace TPOpenHouseMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TPQuiz : ContentPage
    {
        User _user;
        public TPQuiz(User user)
        {
            InitializeComponent();
            _user = user;
        }
    }
}