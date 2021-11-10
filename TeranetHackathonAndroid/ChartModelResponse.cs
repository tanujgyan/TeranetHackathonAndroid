using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeranetHackathonAndroid
{
    public class ChartModelResponse
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }
    }
}