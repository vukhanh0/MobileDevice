namespace MobileDevice.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            Carts = new HashSet<Cart>();
            PermissionDetails = new HashSet<PermissionDetail>();
        }

        [Key]
        public int ID_Product { get; set; }

        public int ID_Category { get; set; }

        public int ID_Color { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Screen { get; set; }

        [StringLength(100)]
        public string SystemOperation { get; set; }

        [StringLength(50)]
        public string RearCamera { get; set; }

        [StringLength(50)]
        public string FrontCamera { get; set; }

        [StringLength(50)]
        public string Ram { get; set; }

        [StringLength(50)]
        public string Rom { get; set; }

        [StringLength(50)]
        public string Sim { get; set; }

        [StringLength(50)]
        public string Battery { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(255)]
        public string ShortDescription { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public int? Amount { get; set; }

        public float? sellnumber { get; set; }

        [StringLength(255)]
        public string Origin { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual Category Category { get; set; }

        public virtual Color Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermissionDetail> PermissionDetails { get; set; }
    }
}
