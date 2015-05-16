using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Net.Http.Headers;
using HelixSaleServiceClient.Model;
using HelixSaleServiceClient.ViewModel;

namespace HelixSaleServiceClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyViewModel vm = new MyViewModel();
            cmbProduct.DataContext = vm;
            cmbCurrency.DataContext = vm;
            GetData();
        }

        /// <summary>
        /// Get Sales Data
        /// </summary>

        private void GetData()
        {
            
            var dt = new DataService();

            if (dt.GetSalesData() != null)
            {
                var results = ResponseHttp.SalesApi().Content.ReadAsAsync<IEnumerable<Sale>>().Result;
                SalesGrid.ItemsSource = results;
            }
            else
            {
                MessageBox.Show("Error Code" + ResponseHttp.SalesApi().StatusCode + " : Message - " + ResponseHttp.SalesApi().ReasonPhrase);
            }
        }

      
        /// <summary>
        /// Add Sales Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dt = new DataService();
            dt.AddSale( txtLocationName.Text, txtSalesPerson.Text, txtTotalSales.Text, cmbProduct, cmbCurrency );

            if(ResponseHttp.PostSale().IsSuccessStatusCode)
            {
                MessageBox.Show("Sales Added");
                txtLocationName.Text = "";
                txtSalesPerson.Text = "";
                txtTotalSales.Text = "";
                GetData();
            }
            else
            {
                MessageBox.Show("Error Code" + ResponseHttp.PostSale().StatusCode + " : Message - " + ResponseHttp.PostSale().ReasonPhrase);
            }

        }
    }
}
