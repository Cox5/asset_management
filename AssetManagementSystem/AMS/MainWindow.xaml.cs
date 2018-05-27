using AMS_Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace AMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public ObservableCollection<>
        AMSClass ams = new AMSClass();
        public Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>> AMSDatabase { get; set; }
        public static BindingList<Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>>> DevicesBindingList { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();
            if (DevicesBindingList == null)
            {
                //DevicesBindingList = new BindingList<Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>>>(AMSDatabase);
            }
            DataContext = this;
        }
    }
}
