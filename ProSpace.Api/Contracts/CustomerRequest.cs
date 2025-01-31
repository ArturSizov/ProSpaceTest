namespace ProSpace.Api.Contracts
{
    /// <summary>
    /// Customer request
    /// </summary>
    public class CustomerRequest
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Address { get; set; }
        public decimal? Discount { get; set; }
    }
}
