namespace NosiYa.Common
{
    public static class ApplicationConstants
    {
        //Layout
        public const int ReleaseYear = 2023;

        //Pagination
        public const int DefaultFirstPage = 1;
        public const int DefaultResultsPerPage = 3;

        //Messages
        public const string DefaultOutfitImagePath = "";

        //Reservations
        public const string Reserved = "Резервирана";
        public const string WaitingForReview = "Изчаква потвърждение";
        

    }

    //Should match the related calling controller name !
    public static class EntityTypesConst 
    {
	    public const string OutfitSet = "outfitset";
	    public const string OutfitPart = "outfitpart";
	    public const string Region = "region";
	    public const string Event = "event";

    }

    public static class SeedingConstants
    {
        public const string AdminId = "7C34FB52-0FDB-4CD7-027F-08DB822AA1B7";
        public const string UserId = "2F29D591-89EF-45B2-89A9-08DB83CEB60E";

        public const string AdminRoleName = "Admin";
        public const string DevAdminEmail = "admin@nosiya.com";


    }
}