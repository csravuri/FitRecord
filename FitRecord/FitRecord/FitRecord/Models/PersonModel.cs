using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FitRecord.Models
{
    public class PersonModel : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string AadharNo { get; set; }

    }
}
