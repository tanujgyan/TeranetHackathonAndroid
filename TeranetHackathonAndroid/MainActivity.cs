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

namespace TeranetHackathonAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ChartView chartView;
        HttpClient client = new HttpClient();
        List<ChartModelResponse> orderTypeData = new List<ChartModelResponse>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
                                   
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            client.BaseAddress = new Uri("https://teranethackathoncosmos.azurewebsites.net/api/Orders/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HubConnection connection = new HubConnectionBuilder()
                   .WithUrl("https://azurefunctionsdemo20211105203829.azurewebsites.net/api")
                   .Build();
            connection.StartAsync();
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            GetOrderType();
            connection.On<string>("transferchartdata", async (value) => {
                await GetOrderType();
            });
            chartView = (ChartView)FindViewById(Resource.Id.chartView1);
           
        }
        public async Task GetOrderType()
        {
            try
            {
                var response = await client.GetStringAsync("GetOrderTypeChart");
                orderTypeData = (List<ChartModelResponse>)JsonConvert.DeserializeObject(response, typeof(List<ChartModelResponse>));
                DrawChart();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void DrawChart()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach(var d in orderTypeData)
            {
                entries.Add(new ChartEntry(d.Data[0])
                {
                    Label = d.Label,
                    ValueLabel = d.Data[0].ToString(),
                    Color = SKColor.Parse("#266489")
                });
            }
           
            var barchart = new BarChart { Entries = entries,LabelTextSize=30f };
            chartView.Chart = barchart;

        }
       
    }
}