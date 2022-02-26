using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataAccessLibrary.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(10)")] // using explicit datatype as we don't want uni-coded zipcode it use only ascii
        public string State { get; set; }
    }
}
