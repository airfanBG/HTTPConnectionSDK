namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExaminerLogs
    {
        public int Id { get; set; }

        public DateTime LastExamination { get; set; }
    }
}
