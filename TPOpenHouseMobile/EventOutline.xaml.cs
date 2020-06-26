using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using static TPOpenHouseMobile.GlobalClass;

namespace TPOpenHouseMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventOutline : ContentPage
    {
        List<Event> _event;
        int dayIndex = 0;
        int numberOfDays = 0;
        List<DateTime> _customViewIndex;
        public EventOutline()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadEvents();
            if (dayIndex == 0)
            {
                btnPrevious.IsVisible = false;
            }
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            if (dayIndex + 1 == numberOfDays - 1)
            {
                btnNext.IsVisible = false;
                btnPrevious.IsVisible = true;
                dayIndex += 1;
                var currentDay = _customViewIndex.ElementAt(dayIndex).Date;
                var customView = (from x in _event
                                  where x.eventTime.Date == currentDay
                                  select new { eventName = x.eventName, eventVenue = x.eventVenue, eventTime = x.eventTime.ToLocalTime().ToString("HH:mm") }).ToList();
                lvEvent.ItemsSource = customView;
                var dayString = currentDay.ToString("dd/MM/yyyy");
                lblDay.Text = $"Event of {dayString}";
            }
            else
            {
                btnPrevious.IsVisible = true;
                dayIndex += 1;
                var currentDay = _customViewIndex.ElementAt(dayIndex).Date;
                var customView = (from x in _event
                                  where x.eventTime.Date == currentDay
                                  select new { eventName = x.eventName, eventVenue = x.eventVenue, eventTime = x.eventTime.ToLocalTime().ToString("HH:mm") }).ToList();
                lvEvent.ItemsSource = customView;
                var dayString = currentDay.ToString("dd/MM/yyyy");
                lblDay.Text = $"Event of {dayString}";
            }
        }

        private async Task LoadEvents()
        {
            var client = new WebApi();
            var response = await client.Post("Events", "");
            _event = JsonConvert.DeserializeObject<List<Event>>(response);
            foreach (var item in _event)
            {
                Console.WriteLine(item.eventTime);
            }
            numberOfDays = (from x in _event
                            group x by x.eventTime.Date into y
                            select y.Key).Count();

            _customViewIndex = (from x in _event
                                group x by x.eventTime.Date into y
                                select y.Key).ToList();

            var currentDay = _customViewIndex.ElementAt(dayIndex).Date;
            var customView = (from x in _event
                              where x.eventTime.Date == currentDay
                              select new { eventName = x.eventName, eventVenue = x.eventVenue, eventTime = x.eventTime.ToLocalTime().ToString("HH:mm") }).ToList();
            lvEvent.ItemsSource = customView;
            var dayString = currentDay.ToString("dd/MM/yyyy");
            lblDay.Text = $"Event of {dayString}";
        }

        private void btnPrevious_Clicked(object sender, EventArgs e)
        {
            if (dayIndex - 1 == 0)
            {
                btnPrevious.IsVisible = false;
                btnNext.IsVisible = true;
                dayIndex -= 1;
                var currentDay = _customViewIndex.ElementAt(dayIndex).Date;
                var customView = (from x in _event
                                  where x.eventTime.Date == currentDay
                                  select new { eventName = x.eventName, eventVenue = x.eventVenue, eventTime = x.eventTime.ToLocalTime().ToString("HH:mm") }).ToList();
                lvEvent.ItemsSource = customView;
                var dayString = currentDay.ToString("dd/MM/yyyy");
                lblDay.Text = $"Event of {dayString}";
            }
            else
            {
                btnNext.IsVisible = true;
                dayIndex -= 1;
                var currentDay = _customViewIndex.ElementAt(dayIndex).Date;
                var customView = (from x in _event
                                  where x.eventTime.Date == currentDay
                                  select new { eventName = x.eventName, eventVenue = x.eventVenue, eventTime = x.eventTime.ToLocalTime().ToString("HH:mm") }).ToList();
                lvEvent.ItemsSource = customView;
                var dayString = currentDay.ToString("dd/MM/yyyy");
                lblDay.Text = $"Event of {dayString}";
            }
        }
    }
}