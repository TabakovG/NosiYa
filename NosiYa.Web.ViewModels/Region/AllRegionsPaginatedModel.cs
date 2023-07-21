using System.ComponentModel.DataAnnotations;

namespace NosiYa.Web.ViewModels.Region
{
	using static Common.ApplicationConstants;

    public class AllRegionsPaginatedModel
    {
        public AllRegionsPaginatedModel()
        {
            CurrentPage = DefaultOutfitsFirstPage;
            RegionsPerPage = DefaultOutfitsPerPage; 
            this.Regions = new HashSet<RegionAllViewModel>();
        }


        public int CurrentPage { get; set; }

        [Display(Name = "Показвай по:")]
        public int RegionsPerPage { get; set; }

        public int RegionsCount { get; set; }    

        public IEnumerable<RegionAllViewModel> Regions { get; set; }


    }
}
