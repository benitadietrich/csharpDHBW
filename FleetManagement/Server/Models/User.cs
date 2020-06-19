using System.Runtime.Serialization;

namespace Server.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public string Firstname { get; set; }

        [DataMember]
        public string Lastname { get; set; }


    }
}
