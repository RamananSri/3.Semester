using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace ModelLayer
{
//        [DataContract(IsReference = true)]
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PWord { get; set; }
        [DataMember]
        //lige her er salted :D
        public string Salt { get; set; }
        [DataMember]
		//[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
		// ErrorMessage = "karakterer ikke tilladt.")]
		public string Name { get; set; }
        [DataMember]
//		[RegularExpression(@"\D{8}",ErrorMessage = "Indtast korrekt tlf nummer")]
        public string Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
//		[RegularExpression(@"\D{4}", ErrorMessage = "Indtast korrekt postnummer nummer")]
		public string Zipcode { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public List<Bicycle> Bicycles { get; set; }
        [DataMember]
        public List<Advertisement> Advertisements { get; set; }

        public User()
        {
        }
    }
}