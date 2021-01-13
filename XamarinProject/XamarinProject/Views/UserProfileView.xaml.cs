﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProject.ViewModels;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfileView : CarouselPage
    {
        public UserProfileView()
        {
            InitializeComponent();
            BindingContext = UserProfileViewModel.getInstance();
        }
    }
}