﻿using System;
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
            }
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
            
            variantsLabel.Content = variantsCount.ToString();
        }

        private void AddCompButton_Click(object sender, RoutedEventArgs e)
        {
            if(editMode)
            {
                
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
                    HDDs = HDDOptionsColl
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
    }
}
