using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Move
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Files
        {
            get
            {
                return _files;
            }
        }

        private ObservableCollection<string> _files = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void DropBox_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                _files.Clear();

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string filePath in files)
                {
                    _files.Add(filePath);
                }

                UploadFiles(files);
            }

            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void DropBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as ListBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropBox_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void UploadFiles(string[] files)
        {
            try
            {
                foreach (var file in files)
                {
                    string ext = System.IO.Path.GetExtension(file);
                    int num = Convert.ToInt32(Num.Text);
                    string newName;

                    do
                    {
                        newName = String.Format(@"np\{0:D4}{1}", num, ext);
                        Num.Text = (++num).ToString();
                    }
                    while (File.Exists(newName));

                    File.Move(file, newName);
                }

            }
            catch
            {

            }
        }
    }
}
