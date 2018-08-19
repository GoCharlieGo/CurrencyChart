using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using CurrencyChart.DAL;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace CurrencyChart.Controllers
{
    public class ChartJS
    {
        public string label { get; set; }
        public int y { get; set; }
    }
    public class HomeController : Controller
    {
        private CurrencyChartContext db = new CurrencyChartContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Razor()
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
            var prices = transactions.ToList().OrderBy(x => x.Date).Select(x => (object)x.Price).ToArray();
            columnChart.SetSeries(new Series[]
            {
                new Series{

                    Name = "Сделка",
                    Data = new Data(prices)
                },
            }
            );

            return View("Razor",columnChart);
        }

        public JsonResult Ajax()
        {
            var model = db.Transactions.ToList().OrderBy(x=>x.Date);
            List<ChartJS> arr = new List<ChartJS>();
            foreach (var m in model)
            {
                arr.Add(new ChartJS() {label = m.Date.ToShortDateString(), y = m.Price});
            }
            return Json(arr);
        }

        public ActionResult TransactionInfoPartial()
        {
            var transactions = db.Transactions.Include(t => t.Currency);
            return View("_TransactionInfo",transactions.ToList());
        }
    }
}