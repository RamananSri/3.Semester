using System.Collections.Generic;
using System.Web.Mvc;
using MVCClient.Service;

namespace MVCClient.ViewModel
{
    public class DropdownDetails
    {
        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> Wheels { get; set; }
        public List<SelectListItem> Frames { get; set; }
        public Brand SelectedBrand { get; set; }
        public BicycleType SelectedType { get; set; }
        public Wheel SelectedWheel { get; set; }
        public Frame SelectedFrame { get; set; }
        public string SelectedYear { get; set; }

    }
}