namespace Database
{
    using ActivityRegister.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            ClientEquipments = new HashSet<ClientEquipments>();
            GeoLocations = new HashSet<GeoLocations>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Appartment { get; set; }

        public string Telephone { get; set; }

        public string RegNumber { get; set; }

        public bool isChecked { get; set; }

        public string Contract { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateOfLastCheck { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateForCheck { get; set; }

        public string EquipmentNumber { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        [Required]
        [StringLength(128)]
        public string ApplicationUserID { get; set; }

        public int ClientEquipmentsId { get; set; }

        [StringLength(40)]
        public string ClientToken { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientEquipments> ClientEquipments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeoLocations> GeoLocations { get; set; }
    }
}
