﻿using System;
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
        }

        private void btnEventOutline_Clicked(object sender, EventArgs e)
        {

        }

        private void btnCourses_Clicked(object sender, EventArgs e)
        {

        }

        private void btnQuizzes_Clicked(object sender, EventArgs e)
        {

        }

        private void btnRewards_Clicked(object sender, EventArgs e)
        {

        }
    }
}