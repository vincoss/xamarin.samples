using System;

using Default_XamarinForms_MasterDetail.Models;

namespace Default_XamarinForms_MasterDetail.ViewModels
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
