using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblRewardID.Text = _reward.ID.ToString();
            GenerateQRCode();
        }


        private void GenerateQRCode()
        {
            BarcodeImageView.BarcodeValue = "Dylan";
            BarcodeImageView.IsVisible = true;
        }
    }
}