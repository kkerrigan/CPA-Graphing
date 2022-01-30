/*
 * Kristian Kerrigan
 * ShowViewModel.cs
 * This class holds all of the logic to bind the Show model to the DataGrid in the Main view.
 */

using KKerrigan_Graphing.Models;
using KKerrigan_Graphing.Helpers;
using KKerrigan_Graphing.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;


namespace KKerrigan_Graphing.View_Models
{
    public class ShowViewModel : DependencyObject 
    {
        // Relay Commands
        public RelayCommand SaveToFileCommand { get; set; }
        public RelayCommand OpenDataFileCommand { get; set; }
        public RelayCommand DeleteRecordCommand { get; set; }
        public RelayCommand CreateGraphWindowCommand { get; set; }

        // Constructor
        public ShowViewModel()
        {
            Shows = new ObservableCollection<Show>();
            OpenDataFileCommand = new RelayCommand(OpenDataFile);
            SaveToFileCommand = new RelayCommand(SaveToFile);
            DeleteRecordCommand = new RelayCommand(DeleteReacord);
            CreateGraphWindowCommand = new RelayCommand(CreateGraphWindow);
        }

        //Dependency Properties
        public Show SelectedShow
        {
            get { return (Show)GetValue(SelectedShowProperty); }
            set { SetValue(SelectedShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedShowProperty =
            DependencyProperty.Register("SelectedShow", typeof(Show), typeof(ShowViewModel), new PropertyMetadata(null));

        public ObservableCollection<Show> Shows
        {
            get { return (ObservableCollection<Show>)GetValue(ShowsProperty); }
            set { SetValue(ShowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Shows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowsProperty =
            DependencyProperty.Register("Shows", typeof(ObservableCollection<Show>), typeof(ShowViewModel), new PropertyMetadata(null));

        public string StatusBar
        {
            get { return (string)GetValue(StatusBarProperty); }
            set { SetValue(StatusBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatusBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusBarProperty =
            DependencyProperty.Register("StatusBar", typeof(string), typeof(ShowViewModel), new PropertyMetadata(null));

        // Methods
        void SaveToFile(object parameter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Shows (*.txt)|*.txt";
            dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? file = dialog.ShowDialog();

            if(file == true)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Show>));
                    using (StreamWriter writer = new StreamWriter(dialog.OpenFile()))
                    {
                        serializer.Serialize(writer, Shows);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("The contents could not be saved to a file:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                StatusBar = "Data was saved successfullly!";
            }
            
        }

        void OpenDataFile(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Shows (*.txt)|*.txt";
            dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? file = dialog.ShowDialog();

            if(file == true)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Show>));
                    using (StreamReader reader = new StreamReader(dialog.OpenFile()))
                    {
                        Shows = serializer.Deserialize(reader) as ObservableCollection<Show>;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("The selected file could not be oppened:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Shows = new ObservableCollection<Show>();
                }
            }
            StatusBar = "File successfully loaded!";
            
        }

        void DeleteReacord(object parameter)
        {
            while(SelectedShow != null)
            {
                Shows.Remove(SelectedShow);
                DeleteReacord(null);
            }
            StatusBar = "Selected Show(s) successfully deleted.";
        }

        void CreateGraphWindow(object parameter)
        {
            if(Shows.Count == 0)
            {
                MessageBox.Show("You must enter at least one data point in order to generate a graph", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusBar = "Please enter some data before creating a graph";
            }
            else
            {
                var window = new GraphWindow(Shows);
                window.ShowDialog();
                StatusBar = "Graph generated in new window";
            }
            
        }
    }
}
