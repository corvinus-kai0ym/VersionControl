using MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mnb_kai0ym
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void test()
         {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            
            var response = mnbService.GetExchangeRatesAsync(request);

           
            //var result = response.GetExchangeRatesResult;

        }
        

    }
}
