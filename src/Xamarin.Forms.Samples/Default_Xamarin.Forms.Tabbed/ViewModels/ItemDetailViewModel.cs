using System;

using Default_Xamarin.Forms.Tabbed.Models;

namespace Default_Xamarin.Forms.Tabbed.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
