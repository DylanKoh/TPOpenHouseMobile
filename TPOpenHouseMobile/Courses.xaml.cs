using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TPOpenHouseMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Courses : ContentPage
    {
        public Courses()
        {
            InitializeComponent();
        }

        private async void btnAppliedAI_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.tp.edu.sg/schools/iit/applied-artificial-intelligence");
        }

        private async void btnBDA_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.tp.edu.sg/schools/iit/big-data-and-analytics");
        }

        private async void btnCommonICT_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.tp.edu.sg/schools/iit/common-ict-programme");
        }

        private async void btnCDF_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.tp.edu.sg/schools/iit/cybersecurity-and-digital-forensics");
        }

        private async void btnFBI_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.tp.edu.sg/schools/iit/financial-business-informatics");
        }

        private async void btnGDD_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.tp.edu.sg/schools/iit/game-design-and-development");
        }

        private async void btnIT_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.tp.edu.sg/schools/iit/information-technology");
        }
    }
}