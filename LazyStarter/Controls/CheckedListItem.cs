using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LazyStarter
{
    public class CheckedStateArgs : EventArgs
    {
        public bool OldValue { get; private set; }
        public bool NewValue { get; private set; }
        public CheckedStateArgs(bool oldState, bool newState)
        {
            OldValue = oldState;
            NewValue = newState;
        }
    }


    public class CheckedListItem : BaseViewModel, ICheckedListItem
    {
        public event EventHandler<CheckedStateArgs> CheckedChange = delegate { };
        private bool isChecked;
        public virtual bool Checked
        {
            get { return isChecked; }
            set
            {
                bool oldValue = isChecked;
                isChecked = value;
                if (oldValue != isChecked)
                    CheckedChange(this, new CheckedStateArgs(oldValue, isChecked));
                NotifyPropertyChanged();
            }
        }


        public bool IsHighLighted { get; set; }

        public bool Selected { get; set; }

        public Image CurrentStatusImage
        {
            get { return Checked ? ImageChecked : ImageUnchecked; }
        }

        public Image ImageChecked { get; set; }

        public Image ImageUnchecked { get; set; }

        public Image ImageRemove { get; set; }

        public Image ImageRemoveInactive { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public IList Container { get; set; }

        public object Tag { get; set; }


        public ICommand CheckedChangeCommand
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    Checked = !Checked;
                    NotifyPropertyChanged(nameof(CurrentStatusImage));
                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new DelegateCommand((p) => { Container.Remove(this); });
            }
        }


        public CheckedListItem(string caption)
        {
            Text = caption;

            ImageChecked = ResourceIcons.IconChecked;
            ImageUnchecked = ResourceIcons.IconUnchecked;
            ImageRemove = ResourceIcons.IconDelete;
            ImageRemoveInactive = ResourceIcons.IconDeleteInactive;
        }

        public void UpdateItem()
        {
            NotifyPropertyChanged(nameof(IsHighLighted));
            NotifyPropertyChanged(nameof(Selected));
        }

        public override bool Equals(object obj)
        {
            var ci = obj as CheckedListItem;
            if (ci == null) return false;

            return Text == ci.Text && Description == ci.Description;
        }

        public override int GetHashCode()
        {
            var str1 = Text ?? String.Empty;
            var str2 = Description ?? String.Empty;

            return String.Concat(str1, str2).GetHashCode();
        }
    }
}
