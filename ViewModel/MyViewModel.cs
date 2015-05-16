using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HelixSaleServiceClient.Model;
using System.Net.Http;

namespace HelixSaleServiceClient.ViewModel
{
  public  class MyViewModel : INotifyPropertyChanged 
    {

      
      public MyViewModel()    
      {        
          IList<Product> Productlist = new List<Product>();
          var dt = new DataService() ;
         
              if (ResponseHttp.ProductApi().IsSuccessStatusCode)
              {
                  Productlist = dt.GetAllProduct().ToList(); 
              }
              
         
                 
          _products = new CollectionView(Productlist);

          IList<CurrencyCode> currencylist = new List<CurrencyCode>();
          currencylist.Add(new CurrencyCode("AUD"));
          currencylist.Add(new CurrencyCode("USD"));
          currencylist.Add(new CurrencyCode("SGD"));
          _currency = new CollectionView(currencylist);
      }
      
      private readonly CollectionView _products;
      private readonly CollectionView _currency;
      private string _currenc;
      private string _product;

    public CollectionView Products
    {
        get { return _products; }
    }

      public  CollectionView Currency    
      {
          get { return _currency; }    
      }
      public string _Currency
      {
          get { return _currenc; }
          set
          {
              if (_currenc == value) return;
              _currenc = value;
              OnPropertyChanged("_Currency");
          }
      }

    public string _Product
    {
        get { return _product; }
        set
        {
            if (_product == value) return;
            _product = value;
            OnPropertyChanged("_Products");
        }
    }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    
}
