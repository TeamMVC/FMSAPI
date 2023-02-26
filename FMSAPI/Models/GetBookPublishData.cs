using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMSAPI.Models
{
    public class GetBookPublishData
    {
        public int PublishID { get; set; }
        public int FacultyID { get; set; }
        public string Faculty_Name { get; set; }
        public string BookName { get; set; }     
        public Nullable<System.DateTime> Publish_Date { get; set; }
    }
}