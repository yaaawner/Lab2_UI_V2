using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;

//[assembly: InternalsVisibleToAttribute("ClassLibrary")]

namespace Lab2_UI_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        V2MainCollection mainCollection = new V2MainCollection();
        //TextBlock text = new TextBlock();
        BindDataItem bind;

        public static RoutedCommand AddDataItem = new RoutedCommand("Add", typeof(Lab2_UI_V2.MainWindow));

        public MainWindow()
        {

            InitializeComponent();
            DataContext = mainCollection;
            //text.Text = mainCollection.Average.ToString();


        }

        private void AddDef_btn_Click(object sender, RoutedEventArgs e)
        {
            mainCollection.AddDefaults();
        }

        private void AddDefDC_btn_Click(object sender, RoutedEventArgs e)
        {
            mainCollection.AddDefaultDataCollection();
        }

        private void AddDefDOG_btn_Click(object sender, RoutedEventArgs e)
        {
            mainCollection.AddDefaultDataOnGrid();
        }

        private void AddElemFile_btn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            if ((bool)dialog.ShowDialog())
                mainCollection.AddElementFromFile(dialog.FileName);
            MessageError();
        }

        private void Remove_btn_Click(object sender, RoutedEventArgs e)
        {
            var selectedMain = this.listBox_Main.SelectedItems;
            List<V2Data> selectedItems = new List<V2Data>();
            selectedItems.AddRange(selectedMain.Cast<V2Data>());

            foreach (V2Data item in selectedItems)
            {
                mainCollection.Remove(item.Info, item.Freq);
            }
        }


        private void DataCollection(object sender, FilterEventArgs args)
        {
            var item = args.Item;
            if (item != null)
            {
                if (item.GetType() == typeof(V2DataCollection)) args.Accepted = true;
                else args.Accepted = false;
            }
        }

        private void DataOnGrid(object sender, FilterEventArgs args)
        {
            var item = args.Item;
            if (item != null)
            {
                if (item.GetType() == typeof(V2DataOnGrid)) args.Accepted = true;
                else args.Accepted = false;
            }
        }

        private void New_btn_Click(object sender, RoutedEventArgs e)
        {
            if (mainCollection.CollectionChangedAfterSave)
            {
                UnsavedChanges();
            }
            mainCollection = new V2MainCollection();
            DataContext = mainCollection;
            MessageError();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            if ((bool)dialog.ShowDialog())
                mainCollection.Save(dialog.FileName);
            MessageError();
        }

        private void Open_btn_Click(object sender, RoutedEventArgs e)
        {
            if (mainCollection.CollectionChangedAfterSave)
            {
                UnsavedChanges();
            }
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            if ((bool)dialog.ShowDialog())
            {
                mainCollection = new V2MainCollection();
                mainCollection.Load(dialog.FileName);
                DataContext = mainCollection;

            }
            MessageError();
        }

        private bool UnsavedChanges()
        {
            MessageBoxResult message = MessageBox.Show("You have unsaved changes. Do you want save it?", "Save", MessageBoxButton.YesNoCancel);
            if (message == MessageBoxResult.Yes)
            {
                Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                if ((bool)dialog.ShowDialog())
                    mainCollection.Save(dialog.FileName);

            }
            else if (message == MessageBoxResult.Cancel)
            {
                return true;
            }
            return false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mainCollection.CollectionChangedAfterSave)
            {
                e.Cancel = UnsavedChanges();
            }
            MessageError();
        }

        public void MessageError()
        {
            if (mainCollection.ErrorMessage != null)
            {
                MessageBox.Show(mainCollection.ErrorMessage, "Error");
                mainCollection.ErrorMessage = null;
            }
        }

        private void AddDataItem_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (mainCollection.CollectionChangedAfterSave)
            {
                UnsavedChanges();
            }
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            if ((bool)dialog.ShowDialog())
            {
                mainCollection = new V2MainCollection();
                mainCollection.Load(dialog.FileName);
                DataContext = mainCollection;

            }
            MessageError();
        }

        /*
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
        */

        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {

            //if (mainCollection.CollectionChangedAfterSave == true) e.CanExecute = true;
            //else e.CanExecute = false;

            e.CanExecute = mainCollection.CollectionChangedAfterSave;


        }
        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show(e.Parameter.ToString());
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            if ((bool)dialog.ShowDialog())
                mainCollection.Save(dialog.FileName);
            MessageError();

        }

        private void CanDeleteCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            var selectedMain = this.listBox_Main.SelectedItems;
            List<V2Data> selectedItems = new List<V2Data>();
            selectedItems.AddRange(selectedMain.Cast<V2Data>());

            //if (selectedItems)

            if (selectedItems.Count != 0) e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedMain = this.listBox_Main.SelectedItems;
            List<V2Data> selectedItems = new List<V2Data>();
            selectedItems.AddRange(selectedMain.Cast<V2Data>());

            foreach (V2Data item in selectedItems)
            {
                mainCollection.Remove(item.Info, item.Freq);
            }
        }

        private void CanAddDataItemCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            /*
            V2DataCollection selectedDataCollection = (V2DataCollection)this.listBox_Main.SelectedItem;
            if (selectedDataCollection != null)
            {
                bind = new BindDataItem(ref selectedDataCollection);
            }  else
            {
                e.CanExecute = false;
                return;
            }
            */


            //this.listBox_DataCollection.SelectedItem
            //bind = new BindDataItem(ref selectedDataCollection);

            if (TextBox_X == null || TextBox_Y == null
                || TextBox_Imagine == null || TextBox_Real == null)
            {
                e.CanExecute = false;
        
             } else if (Validation.GetHasError(TextBox_X) || Validation.GetHasError(TextBox_Y)
                 || Validation.GetHasError(TextBox_Imagine) || Validation.GetHasError(TextBox_Real)) 
             {
                 e.CanExecute = false;
             }
            else
                e.CanExecute = true;
            //e.CanExecute = false;
           




        }

        private void AddDataItemCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                
                bind.Add(float.Parse(TextBox_X.Text), float.Parse(TextBox_Y.Text),
                         float.Parse(TextBox_Imagine.Text), float.Parse(TextBox_Real.Text));
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }


        private void listBox_DataCollection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //V2DataCollection selectedDataCollection = (V2DataCollection)this.listBox_Main.SelectedItem;
            if (listBox_DataCollection.SelectedItem as V2DataCollection != null)
            {
                V2DataCollection selectedDataCollection = (V2DataCollection)this.listBox_DataCollection.SelectedItem;
                bind = new BindDataItem(ref selectedDataCollection);
                TextBox_X.DataContext = bind;
                TextBox_Y.DataContext = bind;
                TextBox_Real.DataContext = bind;
                TextBox_Imagine.DataContext = bind;
                //DataItem_Value.DataContext = DataItemModel;
            }
        }
    }
}
