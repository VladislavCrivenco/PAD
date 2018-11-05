using Dapper.Contrib.Extensions;
using System;

namespace PAD.Models
{
    [Table("posts")]
    public class Posts
    {
        public string id {get;set;}
        public string author_id {get;set;}
        public string title {get;set;}
        public string description {get;set;}
        public string content {get;set;}
        public DateTime date {get;set;}
    }
}