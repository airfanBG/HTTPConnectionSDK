namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GeoLocations
    {
        public int Id { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        public int ClientId { get; set; }

        public int? Client_Id { get; set; }

        [StringLength(128)]
        public string ApplicationUser_Id { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Clients Clients { get; set; }
    }
}
