namespace ShopForHome.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }   // ✅ FK
        public decimal TotalAmount { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "cod";
        public decimal Total { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Email { get; set; }


        public DateTime OrderDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        // ✅ relationship
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

        public class OrderItem
        {
            public int Id { get; set; }
            public int OrderId { get; set; }   // ✅ FK
            public int ProductId { get; set; } // ✅ FK
        public Product? Product { get; set; } = null!;// ✅ navigation property

            public Order? Order { get; set; }  // ✅ navigation property

            public string ProductName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public int UnitPrice { get; internal set; }
        }
    }
