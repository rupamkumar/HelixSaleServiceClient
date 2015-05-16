using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixSaleServiceClient.Model
{
  public  class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int quantity { get; set; }
        public double sale_amount { get; set; }
    }

  public class CurrencyCode
  {
      public string currency { get; set; }
      public CurrencyCode(string cur)
      {
          currency = cur;
      }
  }
}
