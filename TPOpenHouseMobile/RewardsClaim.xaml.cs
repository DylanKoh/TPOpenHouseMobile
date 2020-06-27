using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Common;
using ZXing.QrCode;
using static TPOpenHouseMobile.GlobalClass;

namespace TPOpenHouseMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RewardsClaim : ContentPage
    {
        Reward _reward;
        UserClaim _userClaim;
        public RewardsClaim(Reward reward, UserClaim userClaim)
        {
            InitializeComponent();
            _reward = reward;
            _userClaim = userClaim;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(500);
            lblRewardID.Text = _reward.ID.ToString();
            GenerateQRCode();
            var startTime = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(5);
            var timer = new System.Threading.Timer((e) =>
            {
               MainThread.BeginInvokeOnMainThread(new Action(CheckRedemption));
            }, null, startTime, periodTimeSpan);
        }

        private async void CheckRedemption()
        {
            var client = new WebApi();
            var response = await client.PostStatus($"UserClaims/CheckRedemptionStatus?rewardID={_reward.ID}", "");
            if (response)
            {
                lblSuccessMessage.IsVisible = true;
                aiCheck.IsRunning = false;
                aiCheck.IsVisible = false;
                await Task.Delay(1000);
                await Navigation.PopAsync();
            }
        }

        private void GenerateQRCode()
        {
            BarcodeImageView.BarcodeValue = _reward.ID.ToString();
            BarcodeImageView.IsVisible = true;
        }
    }
}