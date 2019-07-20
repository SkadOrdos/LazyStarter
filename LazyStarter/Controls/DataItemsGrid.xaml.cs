using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for DataItemsGrid.xaml
    /// </summary>
    public partial class DataItemsGrid : DataGrid
    {
        public DataItemsGrid()
        {
            InitializeComponent();
        }


        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                e.NewItems.OfType<ICheckedListItem>().ForEach(i => i.Container = (IList)ItemsSource);

            base.OnItemsChanged(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            FrameworkElement element = null;
            var hitResult = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (hitResult != null) element = hitResult.VisualHit as FrameworkElement;
            object context = element != null ? element.DataContext : null;

            ResetItemsHighLighted();

            OnMouseItemEnter(new MouseOverItemArgs(element, context, (MouseDevice)e.Device, e.Timestamp, e.StylusDevice));
        }

        protected virtual void OnMouseItemEnter(MouseOverItemArgs e)
        {
            var firstItem = Items.Count > 0 ? Items[0] : null;
            if (e.ElementContext != null && firstItem != null && e.ElementContext.GetType() == firstItem.GetType())
            {
                var listItem = e.ElementContext as ICheckedListItem;
                if (listItem != null) listItem.IsHighLighted = true;

                this.Cursor = Cursors.Hand;
            }
            else this.Cursor = Cursors.Arrow;
        }

        protected void ResetItemsHighLighted()
        {
            if (ItemsSource != null)
                ItemsSource.OfType<ICheckedListItem>().ForEach(i => i.IsHighLighted = false);
        }
    }
}
