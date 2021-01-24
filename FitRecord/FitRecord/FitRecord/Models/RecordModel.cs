using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FitRecord.Models
{
    public class RecordModel : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int RecordID { get; set; }
        public int PersonID { get; set; }
        public int ParameterID { get; set; }
        public string ParameterValue { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
