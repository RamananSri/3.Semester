using System.Runtime.Serialization;

namespace ModelLayer
{
    public class BicycleType
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string TypeName { get; set; }
    }
}