using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SkymeyJobsLibs.Models.ActualPrices.Okex
{
    public class OkexCurrentPricesView
    {
        public string code { get; set; }
        public List<Datum> data { get; set; }
        public string msg { get; set; }
    }
    public class Datum
    {
        public string instId { get; set; }
        public string instType { get; set; }
        public double markPx { get; set; }
        public string ts { get; set; }
    }
}
