using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using CurrencyChart.DAL;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace CurrencyChart.Controllers
{

    public class HomeController : Controller
    {
        private CurrencyChartContext db = new CurrencyChartContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RazorPartial()
        {
            Highcharts columnChart = new Highcharts("columnchart");
            var transactions = db.Transactions.Include(t => t.Currency);
            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2

            });

            columnChart.SetTitle(new Title()
            {
                Text = "График"
            });

            //columnChart.SetSubtitle(new Subtitle()
            //{
            //    Text = "Played 9 Years Together From 2004 To 2012"
            //});
            var dates = transactions.ToList().OrderBy(x=>x.Date).Select(x=>x.Date.ToShortDateString()).ToArray();
            
            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Время", Style = "fontWeight: 'bold', fontSize: '17px'" },
                Categories = dates
            });

            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Цена",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });

            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });
            object[] prices = transactions.ToList().OrderBy(x => x.Date).Select(x => (object)x.Price).ToArray();
            columnChart.SetSeries(new Series[]
            {
                new Series{

                    Name = "Сделка",
                    Data = new Data(prices)
                },
            }
            );

            return View("_Razor",columnChart);
        }

        public ActionResult AjaxPartial()
        {
            return null;
        }

        public ActionResult TransactionInfoPartial()
        {
            var transactions = db.Transactions.Include(t => t.Currency);
            return View("_TransactionInfo",transactions.ToList());
        }
    }
}