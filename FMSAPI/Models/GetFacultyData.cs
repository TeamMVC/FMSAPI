using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMSAPI.Models
{
    public class GetFacultyData
    {
        public int FacultyID { get; set; }
        public string Faculty_Name { get; set; }
        public string Faculty_Qualification { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public Nullable<int> Dept_ID { get; set; }
        public string Assign_Course { get; set; }
        public string Dept_Name { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public string createdBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<int> Assign_Course_ID { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
    }
}