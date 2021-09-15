using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.Models
{
    public class CafeViewModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public bool Billiards { get; set; }
        public string Bio { get; set; }
        public string CityId { get; set; }
        public string Name { get; set; }
        public string Capacity { get; set; }
        public bool Betting { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string AreaId { get; set; }
        public string Location { get; set; }
        public string Music { get; set; }
        public bool Dart { get; set; }
        public int StartingWorkTime { get; set; }
        public string Age { get; set; }
        public bool Smoking { get; set; }
        public string Picture { get; set; }
        public int EndingWorkTime { get; set; }
        public bool Terrace { get; set; }
        public bool Hookah { get; set; }
        public bool Playground { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
    }
}
