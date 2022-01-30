/*
 * Kristian Kerrigan
 * GraphWindow.xaml.cs
 * This file contains the code-behind for the GraphWindow.xaml
 */

using KKerrigan_Graphing.Models;
using KKerrigan_Graphing.View_Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace KKerrigan_Graphing.Views
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow(ObservableCollection<Show> shows)
        {
            InitializeComponent();
            GraphViewModel vm = new GraphViewModel(shows);
            DataContext = vm;
        }
    }
}
