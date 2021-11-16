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
    public class DashboardViewModel
    {
        public List<KeyValue> TotalTransactionByTime { get; set; }


        public int TotalRegistrations { get; set; }

        public int TotalSearchs { get; set; }


        public List<KeyValue> TotalSearchsByProvince { get; set; }


        public List<KeyValue> TotalRegistrationsByProvince { get; set; }


        public List<PendingTxnByDate> PendingTransactionByDate { get; set; }


        public List<KeyValue> TotalRegistrationsByServiceType { get; set; }


        public List<KeyValue> TotalSearchesByServiceType { get; set; }
    }
    public class KeyValue
    {
        public string Count { get; set; }
        public string Key { get; set; }

    }
    public class PendingTxnByDate
    {
        public string Date { get; set; }
        public string ServiceType { get; set; }
        public string TxnCount { get; set; }
    }
}