using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FitRecord.Models
{
    public class ParameterModel : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int ParameterID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public bool IsActive { get; set; }
    }
}
