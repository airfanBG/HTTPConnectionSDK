namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientEquipments
    {
        public int Id { get; set; }

        public string TradeMark { get; set; }

        public string Model { get; set; }

        public string SerialModel { get; set; }

        public string Note { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        public int? Client_Id { get; set; }

        public virtual Clients Clients { get; set; }
    }
}
