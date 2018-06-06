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
        #region Field
        /* triger za pauziranje thread-a za chart: kada je izabran samo uredjaj iz combo box-a, chart se ucitava svake 3 sekunde (thread je aktivan) => datumi nisu odabrani
         * kada su datumi odabrani, treba samo jednom iscrtati promene za dati period i pauzira se thread i jednom se poziva funckija za crtanja charta */
        ManualResetEvent trigger = new ManualResetEvent(false);         

        AMSClass ams = new AMSClass();
        public static BindingList<Device> DevicesBindingList { get; set; }
        public static BindingList<Device> DevicesBindingList2 { get; set; }
        public static BindingList<Device> DevicesBindingList3 { get; set; }          //lista predstavlja uredjaje u alarmu...
        public static List<KeyValuePair<string, int>> valueList { get; set; }         // lista za iscrtavanje charta
        public int counter = 0; // brojac za chart na x osi

        // reference za odabir opcija iz combo box-a
        //private int selectedControllerID = -1;
        private string selectedDeviceID = String.Empty;
        private string selectedTypeReport = String.Empty;
        private string selectedReportDevice = String.Empty;
        private string limitOfReport = String.Empty;
        #endregion
        public MainWindow()
        {

            DevicesBindingList = new BindingList<Device>();
            DevicesBindingList2 = new BindingList<Device>();
            DevicesBindingList3 = new BindingList<Device>();

            DataContext = this;
            InitializeComponent();
            StartRefresh();
            StartChart();
            
            //comboBoxController.ItemsSource = RealTimeProcessing.controllerListUI;
            comboBoxDevices.ItemsSource = RealTimeProcessing.devicesListUI;

        }

        public void StartRefresh()
        {
            Thread refresh = new Thread(() =>
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() => { RealTimeProcessing.ProcessingData(ams.AMSDatabase, DevicesBindingList); });


                    Thread.Sleep(3000);
                }
            });


            refresh.Start();
        }

        // premestanje charta u poseban thread zbog pauziranja thread-a
        public void StartChart()
        {
            Thread chartThread = new Thread(() =>
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() => { showColumnChart(); });
                    trigger.WaitOne();      
                    Thread.Sleep(3000);
                }
            });

            chartThread.Start();
        }




        private void comboBoxDevices_DropDownOpened(object sender, EventArgs e)
        {
            comboBoxDevices.ItemsSource = RealTimeProcessing.devicesListUI;
            comboBoxDevices.Items.Refresh();
        }

        //private void comboBoxController_DropDownOpened(object sender, EventArgs e)
        //{
        //    comboBoxController.ItemsSource = RealTimeProcessing.controllerListUI;
        //    comboBoxController.Items.Refresh();
        //}

        //private void comboBoxController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    selectedControllerID = Convert.ToInt32(comboBoxController.SelectedItem);
        //}

        private void comboBoxDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            trigger.Set();
            Device d = new Device();
            DevicesBindingList2.Clear();
            valueList.Clear();
            counter = 0;
            // biranje uredjaja iz cmb Box i smestanje u  promenljivu
            selectedDeviceID = Convert.ToString(comboBoxDevices.SelectedItem);
            if (dataGridTab2.Items.Count > 0)
            {
                dataGridTab2.ClearValue(ItemsControl.ItemBindingGroupProperty);

            }


            // prodji kroz sve uredjaje i za dati ID izaberi sve promene za taj uredjaj
            for (int i = 0; i < RealTimeProcessing.tuples.Count; i++)
            {

                if (selectedDeviceID == RealTimeProcessing.tuples[i].Item2.Id)
                {
                    DevicesBindingList2.Add(new Device(
                        RealTimeProcessing.tuples[i].Item2.Id,
                        RealTimeProcessing.tuples[i].Item2.TypeDevice,
                        RealTimeProcessing.tuples[i].Item2.Value,
                        RealTimeProcessing.tuples[i].Item2.Configuration,
                        Convert.ToString(RealTimeProcessing.tuples[i].Item2.WorkTime)));
                }
            }

            txtBlockDeviceChanges.Text = Convert.ToString(DevicesBindingList2.Count);       // ukupan broj promena za zadati uredjaj
            dataGridTab2.Items.Refresh();

        }



        private void showColumnChart()
        {

            valueList = new List<KeyValuePair<string, int>>();
            //if (!String.IsNullOrEmpty(startDatePicker.Text))
            //if (startDatePicker.Text == null)
                valueList.Add(new KeyValuePair<string, int>(startDatePicker.Text, 0));


            for (int i = 0; i < DevicesBindingList2.Count; i++)
            {
                valueList.Add(new KeyValuePair<string, int>(Convert.ToString(i), Convert.ToInt32(DevicesBindingList2[i].Value)));
            }

            //if (!String.IsNullOrEmpty(endDatePicker.Text))
                valueList.Add(new KeyValuePair<string, int>(endDatePicker.Text, 0));

            //Setting data for line chart

            lineChart.DataContext = valueList;



        }

        private void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            trigger.Set();              // nastavi sa radom  
        }

        private void btnShowChart_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(startDatePicker.Text) || String.IsNullOrEmpty(endDatePicker.Text))
            {
                MessageBox.Show("You must select time interval in order for chart to be displayed!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                trigger.Reset();        // pauziraj thread
                showColumnChart();
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            DevicesBindingList3.Clear();
            limitOfReport = textBoxReportValue.Text;
            if (String.Equals(selectedTypeReport, "Changes"))
            {
                //int numOdChanges = 0;
                if (String.Equals(selectedReportDevice, "All"))
                {
                    foreach (var item in RealTimeProcessing.changesListOdDevice.Values)
                    {
                        foreach (var item1 in item.Values)
                        {
                            if (Convert.ToInt32(limitOfReport) <= item1.Count)
                            {
                                DevicesBindingList3.Add(item1.Last().Item2);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in RealTimeProcessing.changesListOdDevice[Convert.ToInt32(selectedReportDevice)].Values)
                    {
                        if (Convert.ToInt32(limitOfReport) <= item.Count)
                        {
                            DevicesBindingList3.Add(item.Last().Item2);
                        }
                    }
                }
            }
            else
            {
                if (String.Equals(selectedReportDevice, "All"))
                {
                    foreach (var item in RealTimeProcessing.changesListOdDevice.Values)
                    {
                        foreach (var item1 in item.Values)
                        {
                            DateTime time1 = new DateTime(TimeSpan.FromMinutes(Convert.ToDouble(limitOfReport)).Ticks);
                            DateTime time2 = new DateTime(TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - Convert.ToInt32(item1.First().Item1)).Ticks);
                            if (time1 <= time2)
                            {
                                DevicesBindingList3.Add(item1.Last().Item2);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in RealTimeProcessing.changesListOdDevice[Convert.ToInt32(selectedReportDevice)].Values)
                    {
                        DateTime time1 = new DateTime(TimeSpan.FromMinutes(Convert.ToDouble(limitOfReport)).Ticks);
                        DateTime time2 = new DateTime(TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - Convert.ToInt32(item.First().Item1)).Ticks);
                        if (time1>=time2)
                        {
                            DevicesBindingList3.Add(item.Last().Item2);
                        }
                    }
                }
            
            }
        }

        private void comboBoxTypeRepors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTypeReport = Convert.ToString(comboBoxTypeRepors.SelectedItem);
        }

        private void comboBoxTypeRepors_DropDownOpened(object sender, EventArgs e)
        {
            List<string> typeReports = new List<string>() { "Changes", "Hours" };
            comboBoxTypeRepors.ItemsSource = typeReports;
            comboBoxTypeRepors.Items.Refresh();

        }

        private void comboBoxDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedReportDevice = Convert.ToString(comboBoxDevice.SelectedItem);
        }

        private void comboBoxDevice_DropDownOpened(object sender, EventArgs e)
        {
            List<string> controllersList = new List<string>();
            controllersList.Add("All");
            foreach (var item in RealTimeProcessing.controllerListUI)
            {
                controllersList.Add(Convert.ToString(item));
            }
            comboBoxDevice.ItemsSource = controllersList;
            comboBoxDevice.Items.Refresh();
        }
    }
}
