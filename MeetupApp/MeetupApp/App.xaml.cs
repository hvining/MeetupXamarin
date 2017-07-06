using MeetupApp.ViewModels;
using MeetupApp.Views;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Unity;

namespace MeetupApp
{
    public partial class App : PrismApplication
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new MeetupApp.MainPage();
        //}

        //protected override void OnStart()
        //{
        //    // Handle when your app starts
        //}

        //protected override void OnSleep()
        //{
        //    // Handle when your app sleeps
        //}

        //protected override void OnResume()
        //{
        //    // Handle when your app resumes
        //}
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("EventsPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<EventsPage>();

            Container.RegisterType<EventsPageViewModel>(new ContainerControlledLifetimeManager());

            //Container.RegisterInstance<INavigationService>(NavigationService);
        }
    }
}
