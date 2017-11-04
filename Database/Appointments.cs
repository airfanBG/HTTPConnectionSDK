namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointments
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        [StringLength(128)]
        public string ApplicationUser_Id { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
