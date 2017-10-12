using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace ModelLayer
{
    [DataContract(IsReference = true)]
//    [DataContract]
    public class Bicycle
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public Brand Brand { get; set; }

        [DataMember]
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }

        [DataMember]
        public BicycleType Type { get; set; }

        [DataMember]
        [ForeignKey("Type")]
        public int? TypeId { get; set; }

        [DataMember]
        public Wheel WheelSize { get; set; }

        [DataMember]
        [ForeignKey("WheelSize")]
        public int? WheelSizeId { get; set; }

        [DataMember]
        public Frame FrameSize { get; set; }

        [DataMember]
        [ForeignKey("FrameSize")]
        public int? FrameSizeId { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [DataMember]
        public List<Advertisement> AdsList { get; set; }

    }
}