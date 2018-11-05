using Dapper.Contrib.Extensions;
using System;

namespace PAD.Models
{
    [Table("authors")]
    public class Authors
    {
        public string id{get;set;}

        public string first_name{get;set;}
  
        public string last_name{get;set;}

        public string email{get;set;}

        public string birthdate{get;set;}

        public DateTime added{get;set;}
    }
}