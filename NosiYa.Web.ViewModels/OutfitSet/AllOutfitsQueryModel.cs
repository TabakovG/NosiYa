namespace NosiYa.Web.ViewModels.OutfitSet
{
    using System.ComponentModel.DataAnnotations;

    using Enums;
    using NosiYa.Data.Models.Enums;
    using static Common.ApplicationConstants;

    public class AllOutfitsQueryModel
    {
        public AllOutfitsQueryModel()
        {
            CurrentPage = DefaultOutfitsFirstPage;
            OutfitsPerPage = DefaultOutfitsPerPage; 
            this.Outfits = new HashSet<OutfitSetAllViewModel>();
            this.Regions = new HashSet<string>();
        }
        public string? Category { get; set; }

        [Display(Name = "Търси...:")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Цвят:")]
        public string? Color { get; set; }

        [Display(Name = "Регион:")]
        public string? Region { get; set; }
        public IEnumerable<string> Regions { get; set; }

        [Display(Name = "Размер:")]
        public SizeOptions Size { get; set; }

        [Display(Name = "Подходяща за:")]
        public RenterType RenterType { get; set; }

        [Display(Name = "Сортирай по:")]
        public OutfitsSorting OutfitSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Показвай по:")]
        public int OutfitsPerPage { get; set; }

        public int OutfitsCount { get; set; }    

        public IEnumerable<OutfitSetAllViewModel> Outfits { get; set; }


    }
}
