using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
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
    /// Interaction logic for CheckedListBox.xaml
    /// </summary>
    public partial class CheckedListBox : ListBox
    {
        private System.Windows.Media.Brush mouseHoverBrush = new SolidColorBrush(Colors.WhiteSmoke);
        public virtual System.Windows.Media.Brush MouseHoverBrush
        {
            get { return mouseHoverBrush; }
            set { mouseHoverBrush = value; }
        }

        private System.Windows.Media.Brush selectionBrush = new SolidColorBrush(Colors.LightGray);
        public virtual System.Windows.Media.Brush SelectionBrush
        {
            get { return selectionBrush; }
            set { selectionBrush = value; }
        }

        private ObservableCollection<String> headerList = new ObservableCollection<String>();
        public ICollection<String> HeaderList { get { return headerList; } }

        public CheckedListBox()
        {
            this.InitializeComponent();
        }


        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            ResetItemsSelected();

            if (e.AddedItems != null)
                e.AddedItems.OfType<ICheckedListItem>().ForEach(i => i.Selected = true);
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                e.NewItems.OfType<ICheckedListItem>().ForEach(i => i.Container = (IList)ItemsSource);

            base.OnItemsChanged(e);
        }

        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            var hit = VisualTreeHelper.HitTest(this, Mouse.GetPosition(this)).VisualHit;
            var element = hit as FrameworkElement;
            var context = element != null ? element.DataContext as ICheckedListItem : null;
            SelectedItem = context;

            base.OnContextMenuOpening(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var hit = VisualTreeHelper.HitTest(this, e.GetPosition(this)).VisualHit;
            var element = hit as FrameworkElement;
            var context = element != null ? element.DataContext : null;

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

        protected void ResetItemsSelected()
        {
            if (ItemsSource != null)
                ItemsSource.OfType<ICheckedListItem>().ForEach(i => i.Selected = false);
        }
    }
}
