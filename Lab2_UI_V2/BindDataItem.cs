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
                OnPropertyChanged("Y");
            }
        }

        public float Y
        {
            get { return yCoord; }
            set
            {
                yCoord = value;
                OnPropertyChanged("Y");
                OnPropertyChanged("X");
            }
        }

        public float Real
        {
            get { return realValue; }
            set
            {
                realValue = value;
                //OnPropertyChanged("Real");
                OnPropertyChanged("Imagine");
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
                OnPropertyChanged("Real");
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
            /*
            xCoord = yCoord = 0;
            realValue = imagineValue = 1;
            */
        }

        public void Add()
        {
            /*
            X = x;
            Y = y;
            Real = real;
            Imagine = imagine;
            */
            collection.Add(new DataItem(new System.Numerics.Vector2(X, Y), 
                                                  new System.Numerics.Complex(Real, Imagine)));
            OnPropertyChanged("Imagine");
            OnPropertyChanged("Real");
            OnPropertyChanged("Y");
            OnPropertyChanged("X");
        }
    }
}
