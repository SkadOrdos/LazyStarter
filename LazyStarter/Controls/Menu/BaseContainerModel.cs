using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Serialization;

namespace LazyStarter
{
    [Serializable]
    public class BaseContainerModel : BaseMenuItemModel, ISubMenu<BaseMenuItemModel>
    {
        public BaseContainerModel(int sortId = 0) 
            : base(sortId)
        { }

        
        public bool HasChildren
        {
            get
            {
                return items != null && items.Any();
            }
        }

        protected ObservableCollection<BaseMenuItemModel> items;
        /// <summary>
        /// Список элементов меню
        /// </summary>
        public ObservableCollection<BaseMenuItemModel> Items
        {
            get
            {
                if (items == null)
                {
                    items = new ObservableCollection<BaseMenuItemModel>();
                    NotifyPropertyChanged();
                }
                return items;
            }
            set
            {
                if (items == value)
                    return;
                items = value;
                NotifyPropertyChanged();
            }
        }
    }
}
