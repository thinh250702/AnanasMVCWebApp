namespace AnanasMVCWebApp.Utilities.AdapterPattern {
    public class ShippingFeeAdapter : IShippingFee {
        private GHTKShippingFee _ghtkShippingFee;
        public ShippingFeeAdapter() {
            _ghtkShippingFee = new GHTKShippingFee();
        }

        public int GetShippingFee(City city, District district, Ward ward, string address) {
            return _ghtkShippingFee.GetShippingFee(city.Name, district.Name, address);
        }
    }
    public class GHTKShippingFee {
        public int GetShippingFee(string province, string district, string address) {
            throw new NotImplementedException();
            // Call GHTK API
            // Params: string provinceID, string districtID
        }
    }
}
