using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace AdMakerM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddImages.xaml
    /// </summary>
    public partial class AddImages : Window
    {
        Global global;
        Computer comp;

        public ObservableCollection<AdImage> Images { get; set; } = new ObservableCollection<AdImage>();

        public AddImages(Global global, Computer comp)
        {
            InitializeComponent();

            this.global = global;
            this.comp = comp;
            DataContext = new
            {
                ImageOptions = comp.ImagesPath,
                Local = this
            };

            //foreach (ProcessorSocket sock in Enum.GetValues(typeof(ProcessorSocket)))
            //{
            //    processorSocketComboBox.Items.Add(new ComboBoxItem() { Content = sock.ToString() });
            //}
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Image"; // Default file name
            dlg.Multiselect = true;
            //dlg.DefaultExt = ".txt"; // Default file extension
            //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                await Task.Delay(1000);
                foreach(string origFilePath in dlg.FileNames)
                {
                    await AddImage(origFilePath);
                    

                }
            }
        }

        async private Task AddImage(string origFilePath)
        {
            AdImage adImage = new AdImage() { Path = origFilePath };
            try
            {

                //создаём иконки, если надо
                string currentDir = Directory.GetCurrentDirectory();
                string origFileName = System.IO.Path.GetFileName(origFilePath);

                if (!Directory.Exists("icons"))
                {
                    Directory.CreateDirectory("icons");
                }
                string newFileName = "icons\\icon_" + origFileName;
                string newIconPath = System.IO.Path.Combine(currentDir, newFileName);
                if (!File.Exists(newIconPath))
                {
                    await ImageEditor.EditImage(origFilePath, newIconPath, 100);
                }
                adImage.IconPath = newIconPath;


                //создаём копии, если ещё нет таких копий
                if (!Directory.Exists("copies"))
                {
                    Directory.CreateDirectory("copies");
                }
                string copyFileName = "copies\\copy_" + origFileName;
                string newCopyPath = System.IO.Path.Combine(currentDir, copyFileName);
                if (!File.Exists(newCopyPath))
                {
                    await ImageEditor.CopyAndRotate(origFilePath, newCopyPath);
                }
                adImage.CopyPath = newCopyPath;
                adImage.Path = newCopyPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await Task.Delay(100);
            }
            comp.ImagesPath.Add(adImage);
            await Task.Delay(10);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        async private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    string[] allFoundFilesRecursive = Directory.GetFiles(dialog.SelectedPath, "*.jpg", SearchOption.AllDirectories);

                    foreach (string path in allFoundFilesRecursive)
                    {
                        await AddImage(path);
                    }
                }
            }
        }
    }
}
