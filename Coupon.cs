namespace ShopForHome.Api.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Assigned Users (many-to-many relationship)
        public List<UserCoupon> UserCoupons { get; set; } = new();
    }

    public class UserCoupon
    {
        public int Id { get; set; }
        public int UserId { get; set; }   // FK to User
        public int CouponId { get; set; } // FK to Coupon
        public int Discount { get; set; }

        public Coupon Coupon { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
