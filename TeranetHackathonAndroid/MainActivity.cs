using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Microcharts.Droid;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Android.Views;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using OxyPlot.Xamarin.Android;
namespace TeranetHackathonAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ChartView chartView;
        ChartView chartView2;
        ChartView chartView3;
        ChartView chartView4;
        ChartView chartView5;
        HttpClient client = new HttpClient();
        public DashboardViewModel InitialData { get; set; } = new DashboardViewModel();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
                                   
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            HubConnection connection = new HubConnectionBuilder()
                   .WithUrl("https://smartcookie.azurewebsites.net/api")
                   .Build();
            connection.StartAsync();
            GetDataInitial();
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            connection.On<string>("target2",  (value) => {
                InitialData = JsonConvert.DeserializeObject<DashboardViewModel>(value);
                DrawChartForTotalRegByProvince();
                DrawChartForTotalSearchByProvince();
                DrawChartForTotalRegistrationByServiceType();
                DrawChartForTotalSearchesByServiceType();
                DrawChartForTransactionsPending();
            });
            chartView = (ChartView)FindViewById(Resource.Id.chartView1);
            chartView2 = (ChartView)FindViewById(Resource.Id.chartView2);
            chartView3 = (ChartView)FindViewById(Resource.Id.chartView3);
            chartView4 = (ChartView)FindViewById(Resource.Id.chartView4);
            chartView5 = (ChartView)FindViewById(Resource.Id.chartView5);

        }
        public async Task GetDataInitial()
        {
            try
            {
                client.BaseAddress = new Uri("https://smartcookie.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetStringAsync("DashboardLoad2");
                InitialData = JsonConvert.DeserializeObject<DashboardViewModel>(response);
                DrawChartForTotalRegByProvince();
                DrawChartForTotalSearchByProvince();
                DrawChartForTotalRegistrationByServiceType();
                DrawChartForTotalSearchesByServiceType();
                DrawChartForTransactionsPending();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DrawChartForTotalSearchByProvince()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
           
            foreach (var d in InitialData.TotalSearchsByProvince)
            {
                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                entries.Add(new ChartEntry(Convert.ToInt64(d.Count))
                {
                    Label = d.Key,
                    ValueLabel = d.Count.ToString(),
                    Color = SKColor.Parse(color)
                });
            }

            var barchart = new BarChart { Entries = entries, LabelTextSize = 30f };
            chartView2.Chart = barchart;
        }
        private void DrawChartForTotalRegistrationByServiceType()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach (var d in InitialData.TotalRegistrationsByServiceType)
            {
                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                entries.Add(new ChartEntry(Convert.ToInt64(d.Count))
                {
                    Label = d.Key,
                    ValueLabel = d.Count.ToString(),
                    Color = SKColor.Parse(color),
                    ValueLabelColor = SKColor.Parse(color)
                });
            }

            var pieChart = new PieChart { Entries = entries, LabelTextSize= 30f };
            chartView3.Chart = pieChart;
        }

        private void DrawChartForTotalSearchesByServiceType()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach (var d in InitialData.TotalSearchesByServiceType)
            {
                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                entries.Add(new ChartEntry(Convert.ToInt64(d.Count))
                {
                    Label = d.Key,
                    ValueLabel = d.Count.ToString(),
                    Color = SKColor.Parse(color),
                    ValueLabelColor = SKColor.Parse(color)
                });
            }

            var pieChart = new PieChart { Entries = entries, LabelTextSize = 30f };
            chartView4.Chart = pieChart;
        }

        private void DrawChartForTotalRegByProvince()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach (var d in InitialData.TotalRegistrationsByProvince)
            {
                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                entries.Add(new ChartEntry(Convert.ToInt64(d.Count))
                {
                    Label = d.Key,
                    ValueLabel = d.Count.ToString(),
                    Color = SKColor.Parse(color)
                });
            }

            var barchart = new BarChart { Entries = entries, LabelTextSize = 30f };
            chartView.Chart = barchart;

        }

        private void DrawChartForTransactionsPending()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach (var d in InitialData.TotalTransactionByTime)
            {
                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                entries.Add(new ChartEntry(Convert.ToInt64(d.Count))
                {
                    Label = d.Key,
                    ValueLabel = d.Count.ToString(),
                    Color = SKColor.Parse(color)
                });
            }

            var linechart = new LineChart { Entries = entries, LabelTextSize = 30f };
            chartView5.Chart = linechart;

        }
    }
}