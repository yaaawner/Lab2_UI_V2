using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.IO;
using System.ComponentModel;
using System.Windows;
//using Lab1_UI_V2;

[assembly: InternalsVisibleToAttribute("Lab2_UI_V2")]

namespace ClassLibrary
{
    [Serializable]
    class V2MainCollection : IEnumerable<V2Data>, System.Collections.Specialized.INotifyCollectionChanged, INotifyPropertyChanged
    {
        private List<V2Data> v2Datas;

        [field: NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnCollectionChanged(NotifyCollectionChangedAction ev)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void AddDataCollection()
        {
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public bool CollectionChangedAfterSave 
        {
            get; set;
        }

        //[field: NonSerialized]
        public string ErrorMessage
        {
            get; set;
        }

        public void Save(string filename) 
        {
            FileStream fileStream = null;

            try
            {
                if (File.Exists(filename))
                {
                    fileStream = File.OpenWrite(filename);
                }
                else
                {
                    fileStream = File.Create(filename);
                }
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, v2Datas);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "Save: " + ex.Message;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
                CollectionChangedAfterSave = false;
                OnPropertyChanged("CollectionChangedAfterSave");
            }
        }

        public void Load(string filename) 
        {
            FileStream fileStream = null;

            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                v2Datas = (List<V2Data>)binaryFormatter.Deserialize(fileStream);
                //onCollectionChanged(NotifyCollectionChangedAction.Add);
                //OnPropertyChanged("Average");
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "Load: " + ex.Message;
            }
            finally
            {
                fileStream.Close();
                CollectionChangedAfterSave = true;
                OnPropertyChanged("CollectionChangedAfterSave");
            }
        }

        public int Count
        {
            get { return v2Datas.Count; }
        }

        public void Add(V2Data item)
        {
            try
            {
                v2Datas.Add(item);
                OnCollectionChanged(NotifyCollectionChangedAction.Add);
                OnPropertyChanged("Average");
                CollectionChangedAfterSave = true;
                OnPropertyChanged("CollectionChangedAfterSave");
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "Add Element from file: " + ex.Message;
            }
        }

        public bool Remove(string id, double w)
        {
            bool flag = false;

            for (int i = 0; i < v2Datas.Count;)
            {
                if (v2Datas[i].Freq == w && v2Datas[i].Info == id)
                {
                    v2Datas.RemoveAt(i);
                    flag = true;
                    OnCollectionChanged(NotifyCollectionChangedAction.Remove);
                    OnPropertyChanged("Average");
                    CollectionChangedAfterSave = true;
                    OnPropertyChanged("CollectionChangedAfterSave");
                    break;
                }
                else
                {
                    i++;
                }
            }

            //OnPropertyChanged("Average");
            return flag;
        }

        public void AddTest()
        {
            Grid1D Ox = new Grid1D(10, 3);
            Grid1D Oy = new Grid1D(10, 3);
            //v2Datas = new List<V2Data>();
            V2DataOnGrid[] grid = new V2DataOnGrid[4];
            V2DataCollection[] collections = new V2DataCollection[4];


            for (int i = 0; i < 3; i++)
            {
                grid[i] = new V2DataOnGrid("TEST", 2, Ox, Oy);    
                collections[i] = new V2DataCollection("sklnskvjzdfbjnsk" + i.ToString(), i);
            }

            for (int i = 0; i < 3; i++)
            {
                grid[i].initRandom(0, 100);
                collections[i].initTest(4, 100, 100, 0, 100);
                this.Add(grid[i]);
                this.Add(collections[i]);
            }
        }

        public void AddDefaults()
        {
            Grid1D Ox = new Grid1D(10, 3);
            Grid1D Oy = new Grid1D(10, 3);
            v2Datas = new List<V2Data>();
            V2DataOnGrid[] grid = new V2DataOnGrid[4];
            V2DataCollection[] collections = new V2DataCollection[4];

            for (int i = 0; i < 3; i++)
            {
                grid[i] = new V2DataOnGrid("data info2"/*+ i.ToString()*/, 2, Ox, Oy);     // test i = 2
                collections[i] = new V2DataCollection("collection info" + i.ToString(), i);
            }

            for (int i = 0; i < 3; i++)
            {
                grid[i].initRandom(0, 100);
                collections[i].initRandom(4, 100, 100, 0, 100);
                this.Add(grid[i]);
                this.Add(collections[i]);
            }

            Grid1D nullOx = new Grid1D(0, 0);
            Grid1D nullOy = new Grid1D(0, 0);
            grid[3] = new V2DataOnGrid("null", 100, nullOx, nullOy);
            collections[3] = new V2DataCollection("null", 100);

            grid[3].initRandom(0, 100);
            collections[3].initRandom(0, 100, 100, 0, 100);
            this.Add(grid[3]);
            this.Add(collections[3]);
        }

        public V2MainCollection()
        {
            this.v2Datas = new List<V2Data>();
            //AddDefaults();
            CollectionChangedAfterSave = false;
        }

        public void AddDefaultDataCollection()
        {
            V2DataCollection collection = new V2DataCollection("aaaaaaaaa ", 1);
            collection.initRandom(4, 100, 100, 0, 100);
            this.Add(collection);
        }

        public void AddDefaultDataOnGrid()
        {
            Grid1D Ox = new Grid1D(10, 3);
            Grid1D Oy = new Grid1D(10, 3);
            V2DataOnGrid grid = new V2DataOnGrid("data info ", 2, Ox, Oy);
            grid.initRandom(0, 100);
            this.Add(grid);
        }

        public void AddElementFromFile(string filename)
        {
            try
            {
                V2DataOnGrid datas = new V2DataOnGrid(filename);
                this.Add(datas); 
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "Add Element from file: " + ex.Message;
                //OnPropertyChanged("ErrorMessage");
            }
        }

        public override string ToString()
        {
            string ret = "";
            try
            {
                foreach (V2Data data in v2Datas)
                {
                    ret += (data.ToString() + '\n');
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "To String: " + ex.Message;
            }
            return ret;
            
        }

        public IEnumerator<V2Data> GetEnumerator()
        {
            return ((IEnumerable<V2Data>)v2Datas).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)v2Datas).GetEnumerator();
        }

        public string ToLongString(string format)
        {
            string ret = "";
            foreach (V2Data data in v2Datas)
            {
                ret += (data.ToLongString(format) + '\n');
            }
            return ret;
        }

        public double Average
        {
            get {
                IEnumerable<DataItem> collection = from elem in (from data in v2Datas
                                                                 where data is V2DataCollection
                                                                 select (V2DataCollection)data)
                                                   from item in elem
                                                   select item;

                IEnumerable<DataItem> grid = from elem in (from data in v2Datas
                                                                 where data is V2DataOnGrid
                                                                 select (V2DataOnGrid)data)
                                                   from item in elem
                                                   select item;

                IEnumerable<DataItem> items = collection.Union(grid);

                //OnPropertyChanged("Average");
                if (items.Count() != 0)
                    return items.Average(n => n.Complex.Magnitude);
                else return 0;
            }
        }

        public DataItem NearAverage
        {
            get
            {
                double a = this.Average;

                IEnumerable<DataItem> collection = from elem in (from data in v2Datas
                                                                 where data is V2DataCollection
                                                                 select (V2DataCollection)data)
                                                   from item in elem
                                                   select item;

                IEnumerable<DataItem> grid = from elem in (from data in v2Datas
                                                           where data is V2DataOnGrid
                                                           select (V2DataOnGrid)data)
                                             from item in elem
                                             select item;

                IEnumerable<DataItem> items = collection.Union(grid);

                var dif = from item in items
                          select Math.Abs(item.Complex.Magnitude - a);

                double min = dif.Min();

                var ret = from item in items
                          where Math.Abs(item.Complex.Magnitude - a) <= min
                          select item;

                //Console.WriteLine(ret.First().Complex.Magnitude);
                return ret.First();
            }
        }

        public IEnumerable<Vector2> Vectors
        {
            get
            {

                var collections = from data in v2Datas
                                  where data is V2DataCollection
                                  select (V2DataCollection)data;

                var first = collections.First();
                var notfirst = collections.Skip(1);

                var v = from elem in first
                        from a in notfirst
                        from elema in a
                        where elema.Vector == elem.Vector
                        select elem.Vector;

                return v.Distinct();
            }
        }
    }
}
