using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Windows.Data;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Converters
{
    public class QuestionItemM2VMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = App.Host.Services.GetRequiredService<QuestionItemViewModel>();
            vm.QuestionItem = (QuestionItem)value;
            return vm;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((QuestionItemViewModel)value).QuestionItem;
        }
    }
}
