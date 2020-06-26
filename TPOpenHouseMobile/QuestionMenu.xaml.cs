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
    public partial class QuestionMenu : ContentPage
    {
        User _user;
        //Comment in QuestionMenu
        public QuestionMenu(User user)
        {
            InitializeComponent();
            _user = user;
        }
    }
}