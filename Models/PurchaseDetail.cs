using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTienda.Models
{
    [Table("PURCHASE_DETAIL", Schema = "dbo")]
    public class PurchaseDetail
    {
        [Key]
        [Column("ID_DETAIL", TypeName = "int")]
        public int Id { get; set; }
        [Column("ID_PURCHASE", TypeName = "int")]
        public int IdPurchase { get; set; }
        [Column("STORE", TypeName = "varchar(20)")]
        public string Store { get; set; }
        [Column("ID_PRODUCT", TypeName = "int")]
        public int IdProduct { get; set; }
        [Column("TITLE", TypeName = "varchar(500)")]
        public string Title { get; set; }
        [Column("UNIT_PRICE", TypeName = "decimal")]
        public decimal UnitPrice { get; set; }
        [Column("QUANTITY", TypeName = "int")]
        public int Quantity { get; set; }
        [Column("TOTAL_PRODUCT", TypeName = "decimal")]
        public decimal TotalProduct { get; set; }
    }
}
