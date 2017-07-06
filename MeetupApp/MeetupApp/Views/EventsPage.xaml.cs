using MeetupApp.Models;
using MeetupApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MeetupApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<EventsPageViewModel>(this, "", eventsPageViewModel =>
            {
                var meetupGroups = eventsPageViewModel.Groups;

                //Remove existing pins if any
                eventsMap.Pins.Clear();

                var meetupGroupResults = meetupGroups.results;
                foreach (var meetupGroup in meetupGroupResults)
                {
                    //Create a pin for each meetup
                    var position = new Position(meetupGroup.lat, meetupGroup.lon); // Latitude, Longitude
                    var pin = new Pin
                    {
                        Type = PinType.SearchResult,
                        Position = position,
                        Label = meetupGroup.name,
                        Address = "custom detail info",
                        
                    };

                    //Add the newly created pin to the map
                    eventsMap.Pins.Add(pin);
                }

                //venueMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(venueViewModel.Latitude, venueViewModel.Longitude), Distance.FromMiles(1)));
            });
        }
    }
}