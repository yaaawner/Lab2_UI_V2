using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ClassLibrary;

namespace Lab2_UI_V2
{
    class BindDataItem : IDataErrorInfo, INotifyPropertyChanged
    {
        float xCoord, yCoord, realValue, imagineValue;
        V2DataCollection collection;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public float X
        {
            get { return xCoord; }
            set { xCoord = value;
                OnPropertyChanged("X");
            }
        }

        public float Y
        {
            get { return yCoord; }
            set
            {
                yCoord = value;
                OnPropertyChanged("Y");
            }
        }

        public float Real
        {
            get { return realValue; }
            set
            {
                realValue = value;
                OnPropertyChanged("Real");
            }
        }

        public float Imagine
        {
            get { return imagineValue; }
            set
            {
                imagineValue = value;
                OnPropertyChanged("Imagine");
            }
        }

        public string Error { get { return "Error Text"; } }

        public string this[string columnName]
        {
            get
            {
                string msg = null;
                switch (columnName)
                {
                    case "X":
                        foreach (DataItem item in collection.dataItems)
                        {
                            if (item.Vector.X == X && item.Vector.Y == Y)
                            {
                                msg = "Match field points";
                                break;
                            }
                        }
                        break;

                    case "Y":
                        foreach (DataItem item in collection.dataItems)
                        {
                            if (item.Vector.X == X && item.Vector.Y == Y)
                            {
                                msg = "Match field points";
                                break;
                            }
                        }
                        break;
                    case "Real":
                        if (Real == 0 && Imagine == 0)
                        {
                            msg = "Modulus is zero";
                        }
                        break;

                    case "Imagine":
                        if (Real == 0 && Imagine == 0)
                        {
                            msg = "Modulus is zero";
                        }
                        break;
                    default:
                        break;
                }
                return msg;
            }
        }

        public BindDataItem(ref V2DataCollection dataItems)
        {
            collection = dataItems;
        }
    }
}
