using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using MeetupApp.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using Xamarin.Forms.Maps;
using System.Net.Http;
using Xamarin.Forms;
using Plugin.Geolocator;

namespace MeetupApp.ViewModels
{
    public class EventsPageViewModel : BindableBase, INavigationAware
    {
        #region Fields
        private String token = "6118135d6430792a7721c191642a50"; 
        #endregion

        #region Constructors
        public EventsPageViewModel()
        {
            SearchCommand = new DelegateCommand(Search, () => CanSearch);
            UndoFilterCommand = new DelegateCommand(UndoFilter, () => CanUndoFilter);

            //Get Distances
            DistanceOptions = new ObservableCollection<int>();
            GetDistanceOptions(false);

            //Get Page sizes
            PageSizeOptions = new ObservableCollection<int>();
            GetPageSizes(false);

            //Get Categories
            CategoryOptions = new ObservableCollection<Category>();

            SearchResults = new ObservableCollection<Result>();
            SearchResults.CollectionChanged += (s, e) =>
                {
                    RaisePropertyChanged(nameof(SearchResults));
                    RaisePropertyChanged(nameof(HasSearchResults));
                    UndoFilterCommand.RaiseCanExecuteChanged();
                };
        } 
        #endregion

        #region Methods
        private void GetDistanceOptions(Boolean init)
        {
            DistanceOptions.Add(5);
            DistanceOptions.Add(10);
            DistanceOptions.Add(50);
            DistanceOptions.Add(100);

            if (init)
                Distance = DistanceOptions.First();
        }

        private void GetPageSizes(Boolean init)
        {
            for (int i = 1; i < 6; i++)
            {
                PageSizeOptions.Add(i * 10);
            }

            if(init)
                PageSize = PageSizeOptions.First();
        }

        private async Task GetCategories(Boolean init)
        {
            String url = "http://api.meetup.com/2/categories.json/?page=40&key={0}";
            url = String.Format(url, token);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    String str = await http.GetStringAsync(url);
                    var categories = JsonConvert.DeserializeObject<MeetupCategory>(str);

                    foreach (var category in categories.results)
                    {
                        CategoryOptions.Add(category);
                    }
                }
                catch (Exception ex)
                {
                }

                CategoryOptions.Insert(0, new Category { name = "All", shortname = "All", id = 0 });

                if (init)
                    Category = CategoryOptions.First();
            }
        } 
        #endregion

        #region Properties
        private Boolean _isLoading;

        public Boolean IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }


        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                SetProperty(ref _pageSize, value);
            }
        }

        public ObservableCollection<int> PageSizeOptions { get; set; }


        private int _distance;
        public int Distance
        {
            get { return _distance; }
            set
            {
                SetProperty(ref _distance, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<int> DistanceOptions { get; set; }

        private Category _category;

        public Category Category
        {
            get { return _category; }
            set
            {
                SetProperty(ref _category, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Category> CategoryOptions { get; set; }

        private Position _position;
        public Position Position
        {
            get { return _position; }
            set
            {
                SetProperty(ref _position, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        private MeetupGroup _groups;

        public MeetupGroup Groups
        {
            get { return _groups; }
            set
            {
                SetProperty(ref _groups, value);
                if (_groups == null)
                    return;

                foreach (var result in _groups.results)
                {
                    SearchResults.Add(result);
                }
            }
        }

        public ObservableCollection<Result> SearchResults { get; set; }
        
        public Boolean HasSearchResults
        {
            get
            {
                return SearchResults.Count > 0;
            }
        }

        public Boolean HasCategoryOptions
        {
            get
            {
                return CategoryOptions.Count > 0;
            }
        }

        private Result _selectedGroup;

        public Result SelectedGroup
        {
            get { return _selectedGroup; }
            set { SetProperty(ref _selectedGroup, value); }
        } 
        #endregion

        #region Commands
        public DelegateCommand SearchCommand { get; set; }
        private async void Search()
        {
            IsLoading = true;
            SearchResults.Clear();
            String url = "http://api.meetup.com/2/groups.json/?lat={0}&lon={1}&radius={2}{3}&order=members&page={4}&key={5}";
            url = String.Format(url, Position.Latitude, Position.Longitude, Distance, Category.id == 0 ? String.Empty : String.Concat("&category_id=", Category.id), PageSize, token);

            using (HttpClient http = new HttpClient())
            {
                Groups = JsonConvert.DeserializeObject<MeetupGroup>(await http.GetStringAsync(url));
            }

            MessagingCenter.Send(this, "");
            IsLoading = false;
        }
        public bool CanSearch
        {
            get
            {
                return Position != null && Distance != 0 && Category != null;
            }
        }

        public DelegateCommand UndoFilterCommand { get; set; }
        private void UndoFilter()
        {
            foreach (var result in Groups.results)
            {
                if (!SearchResults.Contains(result))
                    SearchResults.Add(result);
            }
        }
        private Boolean CanUndoFilter
        {
            get
            {
                return !(Groups.results.Count == SearchResults.Count);
            }
        } 
        #endregion

        #region Events

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GetCurrentUserLocation();

            if (Distance == 0)
                Distance = DistanceOptions.First();

            if (PageSize == 0)
                PageSize = PageSizeOptions.First();
        }

        private async Task GetCurrentUserLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var pos = await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));
                Position = new Position(pos.Latitude, pos.Longitude);
            }catch
            {
                Position = new Position(42.93708, -75.6107);
            }

            MessagingCenter.Send(this, "LocationUpdated");
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            GetCategories(true);
        }
        #endregion

    }
}
