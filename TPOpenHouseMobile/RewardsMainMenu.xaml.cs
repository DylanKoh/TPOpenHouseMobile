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
    public partial class RewardsMainMenu : ContentPage
    {
        User _user;
        List<RequiredPoint> _list;
        public RewardsMainMenu(User user)
        {
            InitializeComponent();
            _user = user;
            lblPoints.Text = _user.points.ToString();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadPoints();
        }

        private async Task LoadPoints()
        {
            var client = new WebApi();
            var response = await client.Post("Rewards/GetRequiredPoints", "");
            _list = JsonConvert.DeserializeObject<List<RequiredPoint>>(response);
            foreach (var item in _list)
            {
                if (item.RewardName.Contains("ITAS"))
                {
                    lblITASPoint.Text = item.RequiredPoints.ToString();
                }
                else if (item.RewardName.Contains("DesPad"))
                {
                    lblDesPoint.Text = item.RequiredPoints.ToString();
                }
            }
        }

        private async void btnClaimITAS_Clicked(object sender, EventArgs e)
        {
            var client = new WebApi();
            var rewardName = _list.Where(x => x.RewardName.Contains("ITAS")).Select(x => x.RewardName).FirstOrDefault();
            var checkUserClaim = await client.Post($"Rewards/CheckAvailableRewards?rewardName={rewardName}&userID={_user.userID}", "");
            if (checkUserClaim == "\"No available rewards for this user at the moment!\"")
            {
                var newRewardJson = await client.Post($"Rewards/GetNewReward?rewardName={rewardName}&userID={_user.userID}", "");
                if (newRewardJson == "\"Unable to claim as there are no more available rewards!\"")
                {
                    await DisplayAlert("Rewards", $"There are no more {rewardName} vouchers left!", "Ok");
                }
                else if (newRewardJson == "\"Unable to claim reward!Insufficient points!\"")
                {
                    await DisplayAlert("Rewards", "Unable to claim reward! Insufficient points!", "Ok");
                }
                else
                {
                    var newReward = JsonConvert.DeserializeObject<Reward>(newRewardJson);
                    var newClaim = new UserClaim() { isClaimed = false, rewardsIDFK = newReward.ID, userIDFK = _user.userID };
                    var JsonData = JsonConvert.SerializeObject(newClaim);
                    var response = await client.Post("UserClaims/Create", JsonData);
                    if (response == "\"Claim is online!\"")
                    {
                        var userClaimJson = await client.Post($"UserClaims/GetClaim?rewardID={newReward.ID}", "");
                        var userClaim = JsonConvert.DeserializeObject<UserClaim>(userClaimJson);
                        await DisplayAlert("Rewards", "Reward claimed successfully! Please use within 3 minutes!", "Ok");
                        await Navigation.PushAsync(new RewardsClaim(newReward, userClaim));
                    }
                    else
                    {
                        await DisplayAlert("Rewards", "Something went wrong. Please contact our administrator to ensure the voucher you claimed is given to you!", "Ok");
                    }
                }
            }
            else
            {
                var reward = JsonConvert.DeserializeObject<Reward>(checkUserClaim);
                await DisplayAlert("Rewards", "Reward claimed successfully! Please use within 3 minutes!", "Ok");
                var userClaimJson = await client.Post($"UserClaims/GetClaim?rewardID={reward.ID}", "");
                var userClaim = JsonConvert.DeserializeObject<UserClaim>(userClaimJson);
                await Navigation.PushAsync(new RewardsClaim(reward, userClaim));
            }
        }

        private async void btnClaimDes_Clicked(object sender, EventArgs e)
        {
            var client = new WebApi();
            var rewardName = _list.Where(x => x.RewardName.Contains("DesPad")).Select(x => x.RewardName).FirstOrDefault();
            var checkUserClaim = await client.Post($"Rewards/CheckAvailableRewards?rewardName={rewardName}&userID={_user.userID}", "");
            if (checkUserClaim == "\"No available rewards for this user at the moment!\"")
            {
                var newRewardJson = await client.Post($"Rewards/GetNewReward?rewardName={rewardName}&userID={_user.userID}", "");
                if (newRewardJson == "\"Unable to claim as there are no more available rewards!\"")
                {
                    await DisplayAlert("Rewards", $"There are no more {rewardName} vouchers left!", "Ok");
                }
                else if (newRewardJson == "\"Unable to claim reward!Insufficient points!\"")
                {
                    await DisplayAlert("Rewards", "Unable to claim reward! Insufficient points!", "Ok");
                }
                else
                {
                    var newReward = JsonConvert.DeserializeObject<Reward>(newRewardJson);
                    var newClaim = new UserClaim() { isClaimed = false, rewardsIDFK = newReward.ID, userIDFK = _user.userID };
                    var JsonData = JsonConvert.SerializeObject(newClaim);
                    var response = await client.Post("UserClaims/Create", JsonData);
                    if (response == "\"Claim is online!\"")
                    {
                        var userClaimJson = await client.Post($"UserClaims/GetClaim?rewardID={newReward.ID}", "");
                        var userClaim = JsonConvert.DeserializeObject<UserClaim>(userClaimJson);
                        await DisplayAlert("Rewards", "Reward claimed successfully! Please use within 3 minutes!", "Ok");
                        await Navigation.PushAsync(new RewardsClaim(newReward, userClaim));
                    }
                    else
                    {
                        await DisplayAlert("Rewards", "Something went wrong. Please contact our administrator to ensure the voucher you claimed is given to you!", "Ok");
                    }
                }
            }
            else
            {
                var reward = JsonConvert.DeserializeObject<Reward>(checkUserClaim);
                await DisplayAlert("Rewards", "Reward claimed successfully! Please use within 3 minutes!", "Ok");
                var userClaimJson = await client.Post($"UserClaims/GetClaim?rewardID={reward.ID}", "");
                var userClaim = JsonConvert.DeserializeObject<UserClaim>(userClaimJson);
                await Navigation.PushAsync(new RewardsClaim(reward, userClaim));
            }
        }
    }
}