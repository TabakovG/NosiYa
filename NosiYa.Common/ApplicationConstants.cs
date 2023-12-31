﻿namespace NosiYa.Common
{
    public static class ApplicationConstants
    {
        //Layout
        public const int ReleaseYear = 2023;

        //Pagination
        public const int DefaultFirstPage = 1;
        public const int DefaultResultsPerPage = 3;
        public const int DefaultUsersPerPage = 20;

        //Messages
		public const string DefaultImagePath = "";

        //Reservations
        public const string Reserved = "Резервирана";
        public const string WaitingForReview = "Изчаква потвърждение";

        //Area
        public const string AdminAreaName = "Admin";

		//Cache
		public const string UsersCacheKey = "UsersCache";
		public const string RentsCacheKey = "RentsCache";
		public const int UsersCacheDurationMinutes = 5;
		public const int RentsCacheDurationMinutes = 10;

		public const int AllowedDaysBeforeRentStartOnUserDelete = 5;

        
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

        public const string UserRoleName = "User";
        public const string DevUserEmail = "user@nosiya.com";

        public const int InMaintenanceSetContainerId = 73;
    }
}