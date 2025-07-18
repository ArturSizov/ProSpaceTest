﻿namespace ProSpace.Domain.Models
{
    /// <summary>
    /// Product/Item
    /// </summary>
    public class ItemModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Category { get; set; }
    }
}
