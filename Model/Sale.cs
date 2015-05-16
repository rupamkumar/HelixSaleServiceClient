using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixSaleServiceClient.Model
{
   public class Sale
    {
        public int ID { get; set; }
        public long timestamp { get; set; }
        public string location_name { get; set; }
        public string sales_person_name { get; set; }

        //Foreign Key
        public int ProductId { get; set; }
        public string Productname { get; set; }
        //Navigation property
        //public Product Product { get; set; }

        public double total_sale_amount { get; set; }
        public string currency { get; set; }
        public string sale_invoice_number { get; set; }
    }
}
