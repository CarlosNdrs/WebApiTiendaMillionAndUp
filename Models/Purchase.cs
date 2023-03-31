using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTienda.Models
{
    [Table("PURCHASE", Schema = "dbo")]
    public class Purchase
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }
        [Column("DATE_PURCHASE", TypeName = "datetime")]
        public DateTime Date_Pruchase { get; set; }
        [Column("TOTAL", TypeName = "decimal")]
        public decimal Total { get; set; }

    }
}
