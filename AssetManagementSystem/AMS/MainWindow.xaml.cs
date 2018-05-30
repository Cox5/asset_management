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
        public static BindingList<Device> DevicesBindingList { get; set; }
        private Object lockThis = new Object();
        

        public MainWindow()
        {
            
            DevicesBindingList = new BindingList<Device>();

            DataContext = this;
            InitializeComponent();
            StartRefresh();
            comboBoxController.ItemsSource = RealTimeProcessing.controllerListUI;
            comboBoxDevices.ItemsSource = RealTimeProcessing.devicesListUI;
            // za svaki kontroler iscitaj uredjaje koji njemu pripadaju
            // za svaki uredjaj uzmi njegove informacije i prikazi na grafiku
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
            var index = Convert.ToInt32(comboBoxController.SelectedItem);
        }
    }
}
