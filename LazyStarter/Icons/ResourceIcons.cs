using System.Drawing;

namespace LazyStarter
{
    class ResourceIcons
    {
        public static ImageConverter ImageConverter = new ImageConverter();

        private static Image iconChecked;
        public static Image IconChecked
        {
            get
            {
                if (iconChecked == null)
                {
                    var stream = ResourceHelper.GetResourceStream("check.png");
                    iconChecked = Image.FromStream(stream);
                }
                return iconChecked;
            }
        }

        private static Image iconUnchecked;
        public static Image IconUnchecked
        {
            get
            {
                if (iconUnchecked == null)
                {
                    var stream = ResourceHelper.GetResourceStream("uncheck.png");
                    iconUnchecked = Image.FromStream(stream);
                }
                return iconUnchecked;
            }
        }

        private static Image iconDelete;
        public static Image IconDelete
        {
            get
            {
                if (iconDelete == null)
                {
                    var stream = ResourceHelper.GetResourceStream("delete.png");
                    iconDelete = Image.FromStream(stream);
                }
                return iconDelete;
            }
        }

        private static Image iconDeleteInactive;
        public static Image IconDeleteInactive
        {
            get
            {
                if (iconDeleteInactive == null)
                {
                    var stream = ResourceHelper.GetResourceStream("delete_inactive.png");
                    iconDeleteInactive = Image.FromStream(stream);
                }
                return iconDeleteInactive;
            }
        }

        private static Image iconAdd;
        public static Image IconAdd
        {
            get
            {
                if (iconAdd == null)
                {
                    var stream = ResourceHelper.GetResourceStream("add.png");
                    iconAdd = Image.FromStream(stream);
                }
                return iconAdd;
            }
        }

        private static Image iconRun;
        public static Image IconRun
        {
            get
            {
                if (iconRun == null)
                {
                    var stream = ResourceHelper.GetResourceStream("shell_run.png");
                    iconRun = Image.FromStream(stream);
                }
                return iconRun;
            }
        }

        private static Image iconSettings;
        public static Image IconSettings
        {
            get
            {
                if (iconSettings == null)
                {
                    var stream = ResourceHelper.GetResourceStream("settings.png");
                    iconSettings = Image.FromStream(stream);
                }
                return iconSettings;
            }
        }

        private static Image iconArrowUp;
        public static Image IconArrowUp
        {
            get
            {
                if (iconArrowUp == null)
                {
                    var stream = ResourceHelper.GetResourceStream("arrow_up.png");
                    iconArrowUp = Image.FromStream(stream);
                }
                return iconArrowUp;
            }
        }

        private static Image iconArrowDown;
        public static Image IconArrowDown
        {
            get
            {
                if (iconArrowDown == null)
                {
                    var stream = ResourceHelper.GetResourceStream("arrow_down.png");
                    iconArrowDown = Image.FromStream(stream);
                }
                return iconArrowDown;
            }
        }

        private static Image iconArrowUpGray;
        public static Image IconArrowUpGray
        {
            get
            {
                if (iconArrowUpGray == null)
                {
                    var stream = ResourceHelper.GetResourceStream("arrow_up_gray.png");
                    iconArrowUpGray = Image.FromStream(stream);
                }
                return iconArrowUpGray;
            }
        }

        private static Image iconArrowDownGray;
        public static Image IconArrowDownGray
        {
            get
            {
                if (iconArrowDownGray == null)
                {
                    var stream = ResourceHelper.GetResourceStream("arrow_down_gray.png");
                    iconArrowDownGray = Image.FromStream(stream);
                }
                return iconArrowDownGray;
            }
        }
    }
}
