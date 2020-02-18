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
using Microsoft.Win32;

namespace AdMakerM
{
    /// <summary>
    /// Логика взаимодействия для AddComp.xaml
    /// </summary>
    public partial class AddComp : Window
    {
        Global global;
        Computer comp;
        public bool editMode = false;

        public string AdTitle { get; set; } = "Игровой ПК ";
        public string AdDesc { get; set; } = "Игровой ПК ";
        public ObservableCollection<VideoAdapter> VideoAdaptersColl { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<Memory> MemoryOptionsColl { get; set; } = new ObservableCollection<Memory>();
        public ObservableCollection<SSD> SSDOptionsColl { get; set; } = new ObservableCollection<SSD>();
        public ObservableCollection<HDD> HDDOptionsColl { get; set; } = new ObservableCollection<HDD>();
        public ObservableCollection<Processor> ProcessorOptionsColl { get; set; } = new ObservableCollection<Processor>();
        public ObservableCollection<Motherboard> MBOptionsColl { get; set; } = new ObservableCollection<Motherboard>();
        public ObservableCollection<Case> CaseOptionsColl { get; set; } = new ObservableCollection<Case>();
        public ObservableCollection<ProcessorCooler> CPUCoolerOptionsColl { get; set; } = new ObservableCollection<ProcessorCooler>();
        public ObservableCollection<AdImage> ImageOptionsColl { get; set; } = new ObservableCollection<AdImage>();
        public int Memory { get; set; } = 0;
        public int TDP { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        

        public AddComp(Global global, Computer comp)
        {
            InitializeComponent();
            this.global = global;
            DataContext = this;

            if(comp!=null)
            {
                editMode = true;
                Price = comp.Price;
                VideoAdaptersColl = comp.VideoAdapters;
                MemoryOptionsColl = comp.Memories;
                SSDOptionsColl = comp.SSDs;
                HDDOptionsColl = comp.HDDs;
                ProcessorOptionsColl = comp.Processors;
                ImageOptionsColl = comp.ImagesPath;
                AdTitle = comp.Title;
                AdDesc = comp.Description;
            }
            this.comp = comp;
            if (this.comp == null) this.comp = new Computer();
            ShowVariants();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Views.VideoCards f = new Views.VideoCards(global);
            f.ShowDialog();
            VideoAdaptersColl = f.SelectedVideo;
            videoAdaptersDataGrid.ItemsSource = null;
            videoAdaptersDataGrid.ItemsSource = VideoAdaptersColl;
            videoAdaptersDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void ShowVariants()
        {
            int variantsCount = 1;
            if(VideoAdaptersColl.Count!=0)
            {
                variantsCount = variantsCount * VideoAdaptersColl.Count;
            }
            if (MemoryOptionsColl.Count != 0)
            {
                variantsCount = variantsCount * MemoryOptionsColl.Count;
            }
            if (SSDOptionsColl.Count != 0)
            {
                variantsCount = variantsCount * SSDOptionsColl.Count;
            }
            if (HDDOptionsColl.Count != 0)
            {
                variantsCount = variantsCount * HDDOptionsColl.Count;
            }
            if (ProcessorOptionsColl.Count != 0)
            {
                variantsCount = variantsCount * ProcessorOptionsColl.Count;
            }

            variantsLabel.Content = variantsCount.ToString();
            if(comp!=null)
                photoCountLabel.Content = comp.ImagesPath.Count.ToString();
        }

        private void AddCompButton_Click(object sender, RoutedEventArgs e)
        {
            if(editMode)
            {
                comp.Price = Price;
                comp.VideoAdapters = VideoAdaptersColl;
                comp.Memories = MemoryOptionsColl;
                comp.SSDs = SSDOptionsColl;
                comp.HDDs = HDDOptionsColl;
                comp.Processors = ProcessorOptionsColl;
                comp.ImagesPath = ImageOptionsColl;
                comp.Title = AdTitle;
                comp.Description = AdDesc;
                global.SaveAll();
            }
            else
            {
                comp = new Computer()
                {
                    Guid = Guid.NewGuid().ToString(),
                    Title = AdTitle,
                    Description = AdDesc,
                    Price = Price,
                    Memories = MemoryOptionsColl,
                    VideoAdapters = VideoAdaptersColl,
                    SSDs = SSDOptionsColl,
                    HDDs = HDDOptionsColl,
                    Processors = ProcessorOptionsColl
                };
                global.Comps.Add(comp);
            }
            
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Views.AddMemory f = new Views.AddMemory(global);
            f.ShowDialog();
            MemoryOptionsColl = f.SelectedMemory;
            memoryOptionsDataGrid.ItemsSource = null;
            memoryOptionsDataGrid.ItemsSource = MemoryOptionsColl;
            memoryOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var text = ((Button)(FrameworkElement)sender).Content.ToString();
            descTextBox.Text += " {"+ text + "}";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Views.AddSSD f = new Views.AddSSD(global);
            f.ShowDialog();
            SSDOptionsColl = f.SelectedSSD;
            ssdOptionsDataGrid.ItemsSource = null;
            ssdOptionsDataGrid.ItemsSource = SSDOptionsColl;
            ssdOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Views.AddHDD f = new Views.AddHDD(global);
            f.ShowDialog();
            HDDOptionsColl = f.SelectedHDD;
            hddOptionsDataGrid.ItemsSource = null;
            hddOptionsDataGrid.ItemsSource = HDDOptionsColl;
            hddOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Views.AddProcessor f = new Views.AddProcessor(global);
            f.ShowDialog();
            ProcessorOptionsColl = f.SelectedProcessors;
            processorOptionsDataGrid.ItemsSource = null;
            processorOptionsDataGrid.ItemsSource = ProcessorOptionsColl;
            processorOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Views.AddImages f = new Views.AddImages(global, comp);
            f.ShowDialog();
            ImageOptionsColl = comp.ImagesPath;
            imageOptionsDataGrid.ItemsSource = null;
            imageOptionsDataGrid.ItemsSource = ImageOptionsColl;
            imageOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Views.AddMama f = new Views.AddMama(global);
            f.ShowDialog();
            MBOptionsColl = f.SelectedMB;
            mamaOptionsDataGrid.ItemsSource = null;
            mamaOptionsDataGrid.ItemsSource = MBOptionsColl;
            mamaOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Views.AddCase f = new Views.AddCase(global);
            f.ShowDialog();
            CaseOptionsColl = f.SelectedCase;
            caseOptionsDataGrid.ItemsSource = null;
            caseOptionsDataGrid.ItemsSource = CaseOptionsColl;
            caseOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Views.AddCPUCooler f = new Views.AddCPUCooler(global);
            f.ShowDialog();
            CPUCoolerOptionsColl = f.SelectedCPUCooler;
            cpuCoolerOptionsDataGrid.ItemsSource = null;
            cpuCoolerOptionsDataGrid.ItemsSource = CPUCoolerOptionsColl;
            cpuCoolerOptionsDataGrid.Items.Refresh();
            ShowVariants();
        }
    }
}
