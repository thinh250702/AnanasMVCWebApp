namespace AnanasMVCWebApp.Models {
    public abstract class CouponDecorator : AbstractOrder {
        protected AbstractOrder wrappee;
        public CouponDecorator(AbstractOrder order) {
            wrappee = order;
        }
        public override int calculatePrice() {
            return wrappee.calculatePrice();
        }
    }
    public class ConcreteCoupon : CouponDecorator {
        public Coupon Coupon { get; set; }
        public ConcreteCoupon(AbstractOrder order) : base(order) {}
        public override int calculatePrice() {
            if (Coupon != null) {
                return base.calculatePrice() - (base.calculatePrice() * Coupon.Percentage / 100);
            }
            return base.calculatePrice();
        }
    }

    public class Coupon {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Limit { get; set; }
        public int TotalUsage { get; set; } = 0;
        public int MinimumAmount { get; set; }
        public int MaximumDiscount { get; set; }
        public Coupon() {
            Code = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
        }
    }
}
