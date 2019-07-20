using LazyStarter.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LazyStarter
{
    class MainModel : BaseViewModel, ILocalize
    {
        private const string HiddenDirName = "Startup (Disabled by LazyStarter)";
        private readonly string HiddenDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), HiddenDirName);
        private const string LinkExt = "lnk";
        private const string ExeExt = "exe";

        private readonly ProgramExecutor executor = new ProgramExecutor();

        private ObservableCollection<ProgramItem> programList = new ObservableCollection<ProgramItem>();
        public ICollection<ProgramItem> ProgramList { get { return programList; } }


        private ObservableCollection<MenuItemModel> listMenu = new ObservableCollection<MenuItemModel>();
        public ICollection<MenuItemModel> ListMenu { get { return listMenu; } }

        private ProgramItem selectedItem;
        public ProgramItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged();
                UpdateUpDownControls();
            }
        }


        private void UpdateUpDownControls()
        {
            UpdateButtonUpImage(CanMoveUp);
            NotifyPropertyChanged(nameof(CanMoveUp));

            UpdateButtonDownImage(CanMoveDown);
            NotifyPropertyChanged(nameof(CanMoveDown));
        }

        public System.Drawing.Image ButtonUpImage { get; set; }

        public void UpdateButtonUpImage(bool canMoveUp)
        {
            if (canMoveUp) ButtonUpImage = ResourceIcons.IconArrowUp;
            else ButtonUpImage = ResourceIcons.IconArrowUpGray;
            NotifyPropertyChanged(nameof(ButtonUpImage));
        }

        public DelegateCommand ButtonUpCommand
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    int selectedIndex = programList.IndexOf(SelectedItem);
                    programList.Move(selectedIndex, selectedIndex - 1);
                }, (cp) => { return CanMoveUp; });
            }
        }

        public bool CanMoveUp
        {
            get
            {
                return SelectedItem != null && programList.Any() &&
                    programList.IndexOf(SelectedItem) > 0 &&
                    programList.IndexOf(SelectedItem) < programList.Count;
            }
        }

        public System.Drawing.Image ButtonDownImage { get; set; }

        public void UpdateButtonDownImage(bool canMoveDown)
        {
            if (canMoveDown) ButtonDownImage = ResourceIcons.IconArrowDown;
            else ButtonDownImage = ResourceIcons.IconArrowDownGray;
            NotifyPropertyChanged(nameof(ButtonDownImage));
        }

        public DelegateCommand ButtonDownCommand
        {
            get
            {
                return new DelegateCommand((p) =>
                {
                    int selectedIndex = programList.IndexOf(SelectedItem);
                    programList.Move(selectedIndex, selectedIndex + 1);
                }, (cp) => { return CanMoveDown; });
            }
        }

        public bool CanMoveDown
        {
            get
            {
                return SelectedItem != null && programList.Any() &&
                    programList.IndexOf(SelectedItem) >= 0 &&
                    programList.IndexOf(SelectedItem) < programList.Count - 1;
            }
        }

        private void itemList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateUpDownControls();
        }


        private string labelAddText;
        public string LabelAddText
        {
            get { return labelAddText; }
            set
            {
                labelAddText = value;
                NotifyPropertyChanged();
            }
        }


        public MainModel()
        {
            programList.CollectionChanged += itemList_CollectionChanged;

            FillListBoxMenu(ListMenu);
            Localize(LocaleConstants.LOCALE_ENGLISH);
        }

        internal void LoadData()
        {
            var disabledFolder = CreateHiddenFolder(HiddenDirPath);
            AddFiles(disabledFolder.FullName, false);

            var commonStartup = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            AddFiles(commonStartup);
            var userStartup = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            AddFiles(userStartup);

            //new ProgramExecutor().Execute(ProgramList, 2000);
        }


        internal IEnumerable<string> GetAllowedFiles(IEnumerable<string> files)
        {
            return files.Where(f => f.EndsWith(ExeExt) || f.EndsWith(LinkExt)).OrderBy(f => f);
        }

        internal void AddFiles(string path, bool active = true, bool resort = true)
        {
            AddFiles(Directory.GetFiles(path), resort);
        }

        internal void AddFiles(IEnumerable<string> files, bool active = true, bool resort = true)
        {
            var allowedFiles = GetAllowedFiles(files);
            foreach (var file in allowedFiles)
            {
                var newProgram = new ProgramItem(Path.GetFileNameWithoutExtension(file));
                newProgram.Description = file;
                newProgram.Checked = active;
                newProgram.CheckedChange += NewProgram_CheckedChange;

                if (!programList.Contains(newProgram)) programList.Add(newProgram);
            }

            if (resort)
            {
                var sortedList = programList.OrderBy(prop => prop.Name).ToList();
                programList.Clear();
                programList.AddRange(sortedList);
            }
        }

        private void NewProgram_CheckedChange(object sender, CheckedStateArgs e)
        {

        }

        private DirectoryInfo CreateHiddenFolder(string path)
        {
            DirectoryInfo di = Directory.CreateDirectory(path);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            return di;
        }

        private void FillListBoxMenu(ICollection<MenuItemModel> cMenu)
        {
            var menuRun = new MenuItemModel(LocaleConstants.ACTION_RUNPROGRAM, LocaleConstants.ACTION_RUNPROGRAM, 10);
            menuRun.Command = new DelegateCommand((state) => { executor.Execute(selectedItem, 0); });
            menuRun.Icon.Source = ImageToBitmapSourceConverter.Convert(ResourceIcons.IconRun);
            cMenu.Add(menuRun);

            var menuAdd = new MenuItemModel(LocaleConstants.ACTION_ADDPROGRAM, LocaleConstants.ACTION_ADDPROGRAM, 10);
            menuAdd.Icon.Source = ImageToBitmapSourceConverter.Convert(ResourceIcons.IconAdd);
            cMenu.Add(menuAdd);

            var menuRemove = new MenuItemModel(LocaleConstants.ACTION_REMOVEPROGRAM, LocaleConstants.ACTION_REMOVEPROGRAM, 10);
            menuRemove.Icon.Source = ImageToBitmapSourceConverter.Convert(ResourceIcons.IconDelete);
            cMenu.Add(menuRemove);
        }

        internal void ListBoxMenu_Opened(object sender, RoutedEventArgs e)
        {
            var items = ((ItemsControl)e.Source).ItemsSource;

            var deleteMenuItem = VcUtils.FindMenuItem(LocaleConstants.ACTION_REMOVEPROGRAM, items);
            if (deleteMenuItem != null) deleteMenuItem.Enabled = SelectedItem != null;
        }


        public void Localize(string locale)
        {
            LabelAddText = LocalePlugin.GetString(LocaleConstants.LABEL_ADDITEMS);
            VcUtils.LocalizeMenu(ListMenu, locale);
        }
    }
}
