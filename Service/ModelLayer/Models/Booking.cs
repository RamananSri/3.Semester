using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ModelLayer
{
    [DataContract]
    public class Booking
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public double TotalPrice { get; set; }

        [DataMember]
        [ForeignKey("RentUser")]
        public int RentUserId { get; set; }

        [DataMember]
        public User RentUser { get; set; }

        [DataMember]
        [ForeignKey("Advertisement")]
        public int AdvertismentId { get; set; }
        [DataMember]
        public Advertisement Advertisement { get; set; }

    }
}
