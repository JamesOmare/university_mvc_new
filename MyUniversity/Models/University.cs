using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MyUniversity.Models
{
    public class University
    {
        public int ID { get; set; }
        public string Institute { get; set; }

        [Display(Name = "Reporting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateofreporting { get; set; }
        public string Course { get; set; }
        public string Department { get; set; }
        public decimal year { get; set; }
    }

    public class UniversityDBContext : DbContext
    {
        public DbSet<University> Universities { get; set; }
    }

}