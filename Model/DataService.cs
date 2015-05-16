using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Documents;

using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;
using System.Diagnostics;

namespace HelixSaleServiceClient.Model
{
   public class DataService
    {
       
       /// <summary>
       /// Get All Sales
       /// </summary>
       /// <returns></returns>
       public IEnumerable<Sale> GetSalesData()
       {
           IEnumerable<Sale> allsales = null;
           try
           {               
               if (ResponseHttp.SalesApi().IsSuccessStatusCode)
               {
                   allsales = ResponseHttp.SalesApi().Content.ReadAsAsync<IEnumerable<Sale>>().Result;
               }               
           }
           catch(HttpRequestException ex )
           {
               MessageBox.Show(ex.Message);
           }          
               
           return allsales;
       }

       /// <summary>
       /// Get All Products
       /// </summary>
       /// <returns></returns>
       public IEnumerable<Product> GetAllProduct()
       {
           IEnumerable<Product> allproduct = null;
           try
           {
               if (ResponseHttp.ProductApi().IsSuccessStatusCode)
               {
                   allproduct = ResponseHttp.ProductApi().Content.ReadAsAsync<IEnumerable<Product>>().Result;
               }
           }
           catch(HttpRequestException ex)
           {
               MessageBox.Show(ex.Message);
           }           
           return allproduct;
       }

       /// <summary>
       /// Add Sale 
       /// </summary>
       /// <param name="saleID"></param>
       /// <param name="location"></param>
       /// <param name="salesperson"></param>
       /// <param name="totalsales"></param>
       /// <param name="cmb"></param>
       /// <param name="currency"></param>
       public void AddSale(string location, string salesperson, string totalsales,ComboBox cmb, ComboBox currency)
       {
           try
           {
               var sale = new Sale();
               var inv = Guid.NewGuid();              
               sale.timestamp = DateTime.UtcNow.Ticks ;
               sale.location_name = location;
               sale.sales_person_name = salesperson;
               sale.ProductId = ((Product)cmb.SelectionBoxItem).ID;
               sale.Productname = ((Product)cmb.SelectionBoxItem).Name;
               sale.total_sale_amount = Double.Parse(totalsales);
               sale.currency = ((CurrencyCode)currency.SelectionBoxItem).currency;
               sale.sale_invoice_number = salesperson.Substring(0, 3) + inv.ToString().Substring(0, 6) + location.Substring(0, 2);
               ResponseHttp.GetSale(sale);
           }
           catch(FormatException  ex)
           {
               MessageBox.Show(ex.Message);
           }
           
           
       }
    }

    public static class ResponseHttp
    {
        
        private static string uristring = ConfigurationSettings.AppSettings["uristring"];

        private static Sale newsale;
        public static HttpResponseMessage SalesApi()
        {  
            
            HttpResponseMessage response = BaseAddress().GetAsync("api/Sales").Result;
            return response;
        }

        public static HttpResponseMessage ProductApi()
        {
             HttpResponseMessage response = null;
            try
            {
               response = BaseAddress().GetAsync("api/Products").Result;
            }
            catch(ArgumentException  ex)
            {
                throw ex;
            }
            
            return response;
        }

        public static HttpResponseMessage PostSale()
        {
            HttpResponseMessage response =  BaseAddress().PostAsJsonAsync("api/Sales", newsale ).Result;
            return response;
        }
        public static Sale GetSale(Sale sale)
        {
            newsale = sale;
            return newsale ;
        }
        public static string CalculateUNIXTime()
        {
            //Calculate UNIX time
                DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = DateTime.UtcNow - epochStart;
                string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();
                return requestTimeStamp;
        }

      

        public static HttpClient BaseAddress()
        {
            
            CustomDelegatingHandler customDelegate = new CustomDelegatingHandler();
            HttpClient client = HttpClientFactory.Create(customDelegate);
                       
            client.BaseAddress = new Uri(uristring);
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }

    public class CustomDelegatingHandler : DelegatingHandler
    {
        //Obtained from the server earlier, APIKey MUST be stored securely and in App.Config
        
        private string APPId = ConfigurationSettings.AppSettings["APPId"];
        private string APIKey = ConfigurationSettings.AppSettings["APIKey"];
            //"A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            HttpResponseMessage response = null;
            string requestContentBase64String = string.Empty;
            string requestUri = request.RequestUri.AbsoluteUri.ToLower();
            string timestamp = ResponseHttp.CalculateUNIXTime();                        

            string requestHttpMethod = request.Method.Method;           

            requestContentBase64String = APIKey;

            //Creating the raw signature string
            string signatureRawData = String.Format("{0}{1}{2}{3}{4}", APPId, requestUri, requestHttpMethod, timestamp, requestContentBase64String);

            var secretKeyByteArray = Convert.FromBase64String(APIKey);

            byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);
                string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
                //Setting the values in the Authorization header using custom scheme (amx)
                request.Headers.Authorization = new AuthenticationHeaderValue("Hell", string.Format("{0}:{1}:{2}", APPId, requestSignatureBase64String, timestamp));
            }

            try
            {
                response =  base.SendAsync(request, cancellationToken).Result ;
            }
            catch(HttpRequestException ex)
            {
                throw ex;
            }
            
            
            //Debug.WriteLine("Process");

            return response;
        }

    }
}
