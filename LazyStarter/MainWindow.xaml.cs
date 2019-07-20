using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace LazyStarter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainModel model = new MainModel();

        public MainWindow()
        {
            this.DataContext = model;
            InitializeComponent();

            dataGrid.AllowDrop = true;
            dataGrid.PreviewDragOver += DataControl_PreviewDragOver;
            dataGrid.PreviewDrop += DataControl_Drop;
            dataGrid.ContextMenu = new XtraContextMenu();
            dataGrid.ContextMenu.ItemsSource = model.ListMenu;
            dataGrid.ContextMenu.Opened += model.ListBoxMenu_Opened;

            model.LoadData();
        }

        private void DataControl_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;

            var files = model.GetAllowedFiles((string[])e.Data.GetData(DataFormats.FileDrop));
            if (files.Any()) e.Effects = DragDropEffects.Link;
        }

        private void DataControl_Drop(object sender, DragEventArgs e)
        {
            var allDropFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            model.AddFiles(allDropFiles);
        }
    }
}