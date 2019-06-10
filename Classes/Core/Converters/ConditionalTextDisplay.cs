using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Personalised_News_Feed.Core.Converters
{
    public class ConditionalTextDisplay : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string ifText = (string)values[0];
            string elseText = (string)values[1];
            var showIfText = (bool)values[2];
            return showIfText ? ifText : elseText;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
