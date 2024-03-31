using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class Size {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Size(string code) {
            Code = code;
            if (code == "00" || code == "01" || code == "02" ||
                code == "03" || code == "04" || code == "05") {
                switch (code) {
                    case "00":
                        Name = "Freesize";
                        break;
                    case "01":
                        Name = "S";
                        break;
                    case "02":
                        Name = "M";
                        break;
                    case "03":
                        Name = "L";
                        break;
                    case "04":
                        Name = "XL";
                        break;
                    case "05":
                        Name = "XXL";
                        break;
                }
            } else {
                Name = code;
            }
        }
    }
}
