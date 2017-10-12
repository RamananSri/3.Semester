using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ModelLayer
{
    [DataContract]
    public class Advertisement
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        //[MinLength(10)]
        public string Title { get; set; }

        [DataMember]
        [Required]
        //[Range(50, 250, ErrorMessage = "Fail - Description must be within 30 and 250 characters")]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public double Price { get; set; }

        [DataMember]
        [Required]
        public DateTime StartDate { get; set; }

        [DataMember]
        [Required]
        public DateTime EndDate { get; set; } 
           
        [DataMember]
        public Bicycle Bike { get; set; }

        [DataMember]
        [ForeignKey("Bike")]
        public int? BikeId { get; set; }

        [DataMember]
        [ForeignKey("User")]
        public int? UserID { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public List<Booking> Bookings { get; set; }
    }
}