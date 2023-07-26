namespace NosiYa.Common
{
    public static class ApplicationConstants
    {
        //Layout
        public const int ReleaseYear = 2023;

        //Pagination
        public const int DefaultFirstPage = 1;
        public const int DefaultResultsPerPage = 6;

        //Messages
        public const string DefaultOutfitImagePath = "";
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
        public const string FirstUserId = "7C34FB52-0FDB-4CD7-027F-08DB822AA1B7";
        public const string SecondUserId = "2F29D591-89EF-45B2-89A9-08DB83CEB60E";
    }
}