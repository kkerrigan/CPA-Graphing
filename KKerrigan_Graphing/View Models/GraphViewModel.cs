/*
 * Kristian Kerrigan
 * GraphViewModel.cs
 * This class holds all of the logic to bind the graph data to its corresponding view
 */

using KKerrigan_Graphing.Models;
using KKerrigan_Graphing.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace KKerrigan_Graphing.View_Models
{
    public class GraphViewModel : DependencyObject 
    {
        // Define a class that holds all the information for each data point 
        public class DrawBar
        {
            public string Title { get; set; }
            public PathGeometry Bar { get; set; }
            public Brush BarColor { get; set; }
            public double BarThickness { get; set; }
        }

        // Constructor
        public GraphViewModel(ObservableCollection<Show> shows)
        {
            Shows = shows.ToList();
            Bars = new List<DrawBar>();
            DrawGraph();
            GenerateScale();
        }

        // Dependency Properties
        public int WindowHeight
        {
            get { return (int)GetValue(WindowHeightProperty); }
            set { SetValue(WindowHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowHeightProperty =
            DependencyProperty.Register("WindowHeight", typeof(int), typeof(GraphViewModel), new PropertyMetadata(600));

        public List<DrawBar> Bars
        {
            get { return (List<DrawBar>)GetValue(BarsProperty); }
            set { SetValue(BarsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Bars.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarsProperty =
            DependencyProperty.Register("Bars", typeof(List<DrawBar>), typeof(GraphViewModel), new PropertyMetadata(null));

        public List<Show> Shows
        {
            get { return (List<Show>)GetValue(ShowsProperty); }
            set { SetValue(ShowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowsProperty =
            DependencyProperty.Register("Shows", typeof(List<Show>), typeof(GraphViewModel), new PropertyMetadata(null));

        public List<string> Scales
        {
            get { return (List<string>)GetValue(ScalesProperty); }
            set { SetValue(ScalesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Scales.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScalesProperty =
            DependencyProperty.Register("Scales", typeof(List<string>), typeof(GraphViewModel), new PropertyMetadata(null));


        public int ScaleCount
        {
            get { return (int)GetValue(ScaleCountProperty); }
            set { SetValue(ScaleCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleCountProperty =
            DependencyProperty.Register("ScaleCount", typeof(int), typeof(GraphViewModel), new PropertyMetadata(0));


        public List<string> Titles
        {
            get { return (List<string>)GetValue(TitlesProperty); }
            set { SetValue(TitlesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Titles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitlesProperty =
            DependencyProperty.Register("Titles", typeof(List<string>), typeof(GraphViewModel), new PropertyMetadata(null));


        public int TitlesCount
        {
            get { return (int)GetValue(TitlesCountProperty); }
            set { SetValue(TitlesCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitlesCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitlesCountProperty =
            DependencyProperty.Register("TitlesCount", typeof(int), typeof(GraphViewModel), new PropertyMetadata(0));

        // Methods
        public void DrawGraph()
        {
            int yCoord = 0;
            int yAxisBottom = 200;
            int xCoord = 30;

            // Find the show with the most episodes so you know how high the scale needs to be and the 
            // relative bar size
            int maxEpisodeCount = Shows.OrderByDescending(show => show.NumberOfEpisodes).First().NumberOfEpisodes;
            double relativeHeight = (WindowHeight / (double)maxEpisodeCount);

            // Randomly color each bar a different color
            Random rng = new Random();

            // Declare a temp list of DrawBar objects 
            List<DrawBar> temp = new List<DrawBar>();

            // Declare a List of string to hold the show titles to be used to label the x-axis
            List<string> showTitles = new List<string>();

            foreach(Show show in Shows)
            {
                PathGeometry bar = new PathGeometry();
                yCoord = WindowHeight - (int)(relativeHeight * show.NumberOfEpisodes);
                // Correct for y-coordinates that fall below the 250 starting point 
                if (yCoord > (WindowHeight - yAxisBottom))
                    yCoord = (WindowHeight - yAxisBottom) - show.NumberOfEpisodes;

                bar.Figures.Add(new PathFigure(new Point(xCoord, WindowHeight - yAxisBottom),
                    new List<LineSegment>() { new LineSegment(new Point(xCoord, yCoord), true) }, false));

                Color barColor = Color.FromRgb((byte)rng.Next(1, 255), (byte)rng.Next(1, 255), (byte)rng.Next(1, 255));

                temp.Add(new DrawBar { BarColor = new SolidColorBrush(barColor), Bar = bar, BarThickness = 50 });

                showTitles.Add(show.Title);

                xCoord += 100;
            }
            Bars = temp;
            Titles = showTitles;
            TitlesCount = showTitles.Count;
        }

        public void GenerateScale()
        {
            List<string> values = new List<string>();
            int maxEpisodeCount = Shows.OrderByDescending(show => show.NumberOfEpisodes).First().NumberOfEpisodes;

            //Round the max number to the nearest 10
            maxEpisodeCount = ExtensionMethods.RoundToNearestTen(maxEpisodeCount);

            for(int i = maxEpisodeCount; i >= 0; i -= 20)
            {
                values.Add(i.ToString());
            }
            Scales = values;
            ScaleCount = values.Count;
        }
    }
}
