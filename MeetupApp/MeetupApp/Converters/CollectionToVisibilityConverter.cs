using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace MeetupApp.Converters
{
    public class CollectionToVisibilityConverter : IValueConverter
    {
        private Boolean _nullOrEmptyIsVisible = false;

        public Boolean NullOrEmptyIsVisible
        {
            get { return _nullOrEmptyIsVisible; }
            set
            {
                _nullOrEmptyIsVisible = value;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<object> collection = value as IEnumerable<object>;

            bool hasContent = (collection != null && collection.Count() > 0);

            return NullOrEmptyIsVisible ? !hasContent : hasContent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
