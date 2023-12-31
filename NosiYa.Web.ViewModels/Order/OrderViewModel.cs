﻿namespace NosiYa.Web.ViewModels.Order
{
    public class OrderViewModel
    {
        public string OrderId { get; set; } = null!;

        public int OutfitId { get; set; }

        public string Name { get; set; } = null!;

        public string SetImage { get; set; } = null!;

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsApproved { get; set; }
    }
}