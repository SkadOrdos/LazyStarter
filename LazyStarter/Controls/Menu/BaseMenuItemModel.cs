using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace LazyStarter
{
    [Serializable]
    [XmlRoot("BaseMenuItemModel")]
    [XmlInclude(typeof(BaseContainerModel)), 
        XmlInclude(typeof(MenuItemModel)), 
        XmlInclude(typeof(SeparatorModel))]
    public class BaseMenuItemModel : BaseViewModel, IMenuItem, IComparable<BaseMenuItemModel>
    {
        private int sortId = 0;
        public virtual int SortId
        {
            get
            {
                return this.sortId;
            }
        }

        private bool enabled = true;
        /// <summary>
        /// Доступность элемента меню
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility visibility;
        public virtual Visibility Visibility
        {
            get { return visibility; }
            set
            {
                if (visibility == value)
                    return;
                visibility = value;
                NotifyPropertyChanged();
            }
        }

        private string text = String.Empty;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                NotifyPropertyChanged();
            }
        }
        
        private Image icon = new Image();
        public Image Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Сепаратор ли я, вот в чем вопрос...
        /// </summary>
        public virtual bool IsSeparator
        {
            get { return false; }
        }

        [XmlIgnore]
        /// <summary>
        /// Содержимое (не сериализируется)
        /// </summary>
        public object Tag { get; set; }


        public BaseMenuItemModel()
        { }

        protected BaseMenuItemModel(int sortId)
        {
            this.sortId = sortId;
        }
        

        public int CompareTo(BaseMenuItemModel other)
        {
            return this.SortId - other.SortId;
        }
    }
}