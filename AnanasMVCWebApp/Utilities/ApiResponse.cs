using Microsoft.Extensions.Options;

namespace AnanasMVCWebApp.Utilities {
    public class ApiResponse<T> {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class GHNRequestData {
        public int service_type_id { get; set; }
        public int to_district_id { get; set; }
        public string to_ward_code { get; set; }
        public int weight { get; set; }
    }
    public class GHNResponse {
        public int code { get; set; }
        public string message { get; set; }
        public GHNResponseData data { get; set; }
    }
    public class GHNResponseData {
        public int total { get; set; }
        public int service_fee { get; set; }
    }
    public class GHTKResponse {
        public bool success { get; set; }
        public string message { get; set; }
        public GHTKResponseData fee { get; set; }
    }
    public class GHTKResponseData {
        public int fee { get; set; }
        public int insurance_fee { get; set; }
        public int ship_fee_only { get; set; }
    }

}
