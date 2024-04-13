namespace AnanasMVCWebApp.Utilities.AdapterPattern {
    public interface IShippingFee {
        public int GetShippingFee(City city, District district, Ward ward, string address);
    }
    public class GHNShippingFee : IShippingFee {
        public int GetShippingFee(City city, District district, Ward ward, string address) {
            throw new NotImplementedException();
            // Call GHN API
            // Params: int districtId, string wardCode
        }
    }
    public class City {
        public int Id { get; set; }
        public string Name { get; set; }
    }
        public class District {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Ward {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
