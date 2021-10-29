using mnb_kai0ym.Entities;
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
using System.Xml;

namespace mnb_kai0ym
{
    public partial class Form1 : Form
    {
        private BindingList<RateData>Rates; 
        public Form1()
        {
            InitializeComponent();
            
                Rates = new BindingList<RateData>();
        }

        void createList()
         {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            
            var response = mnbService.GetExchangeRatesAsync(request);

            var result = response;

        }

        void processXml()
        {
            var xml = new XmlDocument();
            xml.LoadXml("Result");


            foreach (XmlElement element in xml.DocumentElement)
            {

                var rate = new RateData();
                Rates.Add(rate);

                //Dátum
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                //Valuta
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                //Értékk
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }



    }
}
