/*
 * Kristian Kerrigan
 * Show.cs
 * This class contains the model object for storing the data in the DataGrid
 */

using System;
using System.ComponentModel;
using System.Windows;

namespace KKerrigan_Graphing.Models
{
    [Serializable]
    public class Show : INotifyPropertyChanged, IDataErrorInfo
    {
        // Properties
        private string title_;

        public string Title
        {
            get
            {
                return title_;
            }
            set
            {
                title_ = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }

        private int numEpisodes_;

        public int NumberOfEpisodes
        {
            get
            {
                return numEpisodes_;
            }
            set
            {
                if(value >= 0)
                {
                    numEpisodes_ = value;
                }
                else
                {
                    numEpisodes_ = 0;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfEpisodes"));
                
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string errorMessage = null;
                switch (columnName)
                {
                    case "Title":
                        if (String.IsNullOrWhiteSpace(Title))
                        {
                            errorMessage = "Title is required";
                        }
                        break;
                }
                return errorMessage;
            }
        }

        //Events
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
