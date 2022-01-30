/*
 * Kristian Kerrigan
 * MainWindow.xaml.cs
 * This file contains the code-behind for the MainWindow.xaml
 */

using KKerrigan_Graphing.View_Models;
using System.Windows;
using System.Windows.Input;

namespace KKerrigan_Graphing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ShowViewModel();
        }

        private void ApplicationClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
