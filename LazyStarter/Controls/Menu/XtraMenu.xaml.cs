using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LazyStarter
{
    /// <summary>
    /// Interaction logic for MenuEx.xaml
    /// </summary>
    public partial class XtraMenu : Menu
    {
        public XtraMenu()
        {
            InitializeComponent();
        }


        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            SortCollection(newValue);

            base.OnItemsSourceChanged(oldValue, newValue);
        }


        ICollection<BaseMenuItemModel> SortCollection(IEnumerable collection)
        {
            var target = collection as ICollection<BaseMenuItemModel>;
            if (target != null)
            {
                target = new ObservableCollection<BaseMenuItemModel>(target.OrderBy(i => i.SortId));

                foreach (var item in target)
                {
                    var subMenu = item as ISubMenu<BaseMenuItemModel>;
                    if (subMenu != null && subMenu.HasChildren)
                        subMenu.Items = new ObservableCollection<BaseMenuItemModel>(SortCollection(subMenu.Items));
                }
            }

            return target;
        }
    }
}
