using AMS_Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AMSClass ams = new AMSClass();
        Device d = new Device();
        public static BindingList<Device> DevicesBindingList { get; set; }
        public static BindingList<Device> DevicesBindingList2 { get; set; }
        private Object lockThis = new Object();

        // reference za odabir opcija iz combo box-a
        private int selectedControllerID = -1;
        private string selectedDeviceID = String.Empty;

        public MainWindow()
        {
            
            DevicesBindingList = new BindingList<Device>();
            DevicesBindingList2 = new BindingList<Device>();

            DataContext = this;
            InitializeComponent();
            StartRefresh();
            comboBoxController.ItemsSource = RealTimeProcessing.controllerListUI;
            comboBoxDevices.ItemsSource = RealTimeProcessing.devicesListUI;
            // za svaki kontroler iscitaj uredjaje koji njemu pripadaju
            // za svaki uredjaj uzmi njegove informacije i prikazi na grafiku

            showColumnChart();
        }

        public void StartRefresh()
        {
            Thread refresh = new Thread(()=> {
                while (true)
                {
                    this.Dispatcher.Invoke(() => { RealTimeProcessing.ProcessingData(ams.AMSDatabase, DevicesBindingList); });
                    
                    Thread.Sleep(2000);
                }
            });
            

            refresh.Start();
        }


        private void comboBoxDevices_DropDownOpened(object sender, EventArgs e)
        {
            comboBoxDevices.ItemsSource = RealTimeProcessing.devicesListUI;
            comboBoxDevices.Items.Refresh();
        }

        private void comboBoxController_DropDownOpened(object sender, EventArgs e)
        {
            comboBoxController.ItemsSource = RealTimeProcessing.controllerListUI;
            comboBoxController.Items.Refresh();
        }

        private void comboBoxController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedControllerID = Convert.ToInt32(comboBoxController.SelectedItem);
        }

        private void comboBoxDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DevicesBindingList.Clear();
            // biranje uredjaja iz cmb Box i smestanje u  promenljivu
            selectedDeviceID = Convert.ToString(comboBoxDevices.SelectedItem);
            if (dataGridTab2.Items.Count > 0)
            {
                dataGridTab2.ClearValue(ItemsControl.ItemBindingGroupProperty);
                
            }


            // prodji kroz 
            for (int i=0; i < RealTimeProcessing.tuples.Count; i++)
            {
                while (selectedDeviceID == RealTimeProcessing.tuples[i].Item2.Id)
                {
                    RealTimeProcessing.tuples[i].Item2.Id = d.Id;
                    RealTimeProcessing.tuples[i].Item2.Configuration = d.Configuration;
                    RealTimeProcessing.tuples[i].Item2.TypeDevice = d.TypeDevice;
                    RealTimeProcessing.tuples[i].Item2.Value = d.Value;
                    RealTimeProcessing.tuples[i].Item2.WorkTime = d.WorkTime;

                    DevicesBindingList2.Add(d);
                }
            }
            dataGridTab2.Items.Refresh();

        }

        private void showColumnChart()
        {
            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
            valueList.Add(new KeyValuePair<string, int>("Developer", 60));
            valueList.Add(new KeyValuePair<string, int>("Misc", 20));
            valueList.Add(new KeyValuePair<string, int>("Tester", 50));
            valueList.Add(new KeyValuePair<string, int>("QA", 30));
            valueList.Add(new KeyValuePair<string, int>("Project Manager", 40));

            //Setting data for line chart
            
            lineChart.DataContext = valueList;
        }
    }
}
