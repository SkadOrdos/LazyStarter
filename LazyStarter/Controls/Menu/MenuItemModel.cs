using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace LazyStarter
{
    [Serializable]
    public class MenuItemModel : BaseContainerModel
    {
        #region// Visual

        private bool isCheckable = false;
        public bool IsCheckable
        {
            get
            {
                return isCheckable;
            }
            set
            {
                isCheckable = value;
                NotifyPropertyChanged();
            }
        }


        private bool isChecked = false;
        public bool Checked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                NotifyPropertyChanged();
            }
        }

        private string toolTip;
        public string ToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                toolTip = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
        

        private ICommand command;
        [XmlIgnore]
        public ICommand Command
        {
            get
            {
                return command;
            }
            set
            {
                command = value;
                NotifyPropertyChanged();
            }
        }


        private string commandId;
        public string CommandId
        {
            get
            {
                return this.commandId;
            }
            set
            {
                commandId = value;
                NotifyPropertyChanged();
            }
        }


        public MenuItemModel() : base()
        { }

        public MenuItemModel(string text, int sortId = 0)
            : base(sortId)
        {
            Text = text;
        }


        public MenuItemModel(string commandId, string text, BitmapImage image, int sortId)
            : this(text, sortId)
        {
            this.CommandId = commandId;
            this.Icon.Source = image;
        }

        public MenuItemModel(string commandId, string text, int sortId)
            : this(commandId, text, null, sortId)
        { }


        public override string ToString()
        {
            return this.Text + " - " + this.CommandId;
        }
    }
}