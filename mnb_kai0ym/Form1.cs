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
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            
                Rates = new BindingList<RateData>();
            processXml();
            dataGridView1.DataSource = Rates;
            //chartRateData.DataSource = Rates;
            comboBox1.DataSource = Currencies;
            refreshData();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void refreshData()
        {
            //chartRateData.DataSource = Rates;

            //var series = chartRateData.Series[0];
            //series.ChartType = SeriesChartType.Line;
            //series.XValueMember = "Date";
            //series.YValueMembers = "Value";
            //series.BorderWidth = 2;

            //var legend = chartRateData.Legends[0];
            //legend.Enabled = false;

            //var chartArea = chartRateData.ChartAreas[0];
            //chartArea.AxisX.MajorGrid.Enabled = false;
            //chartArea.AxisY.MajorGrid.Enabled = false;
            //chartArea.AxisY.IsStartedFromZero = false;
        }

        private string GetCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();
            //var response = mnbService.GetCurrencies(request);
            //var result = response.GetCurrenciesResult;
            var result = "";
            return result;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            refreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshData();
        }

        
    }
}
