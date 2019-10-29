using System;
using System.Threading.Tasks;

namespace Xamarin_Navigation.Interfaces
{
    public interface INavigator
    {
        Task PushModalAsync(Type pageType, object model);
    }
}
