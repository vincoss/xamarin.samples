using System;

using Default_Xamarin.Forms.MasterDetail.Models;

namespace Default_Xamarin.Forms.MasterDetail.ViewModels
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
