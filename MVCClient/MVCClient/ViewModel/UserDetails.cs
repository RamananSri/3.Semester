using System.Collections.Generic;
using MVCClient.Service;

namespace MVCClient.ViewModel
{
    public class UserDetails
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string Age { get; set; }
        public List<Bicycle> Bicycles { get; set; }
    }
}