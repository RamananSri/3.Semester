using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVCClient.Service;

namespace MVCClient.ViewModel
{
    public class BikeDetails
    {
        public Brand Brand { get; set; }
        public Type BicycleType { get; set; }
        public string Year { get; set; }
        public Wheel WheelSize { get; set; }
        public Frame FrameSize { get; set; }
        public int Id { get; set; }

        public List<Bicycle> bikes { get; set; }

        public List<SelectListItem> Bicycles { get; set; }
        public Bicycle SelectedBike { get; set; }

    }

}