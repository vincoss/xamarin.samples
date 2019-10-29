using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Navigation.Interfaces;
using Xamarin_Navigation.Views;

namespace Xamarin_Navigation.Services
{
    public class Navigator : INavigator
    {
        public async Task PushModalAsync(Type pageType, object model)
        {
            if(pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            var page = Get(pageType);
            if(page != null && model != null)
            {
                page.BindingContext = model;
            }
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        private Page Get(Type type)
        {
            if (type == typeof(SettingOneView))
            {
                return new SettingOneView();
            }
            if (type == typeof(SettingTwoView))
            {
                return new SettingTwoView();
            }

            return new NotFoundView();
        }
    }
}
