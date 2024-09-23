using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class OrderDetail {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        //public byte[] ImageName { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int SubTotal {
            get { return Quantity * Price; }
        }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
