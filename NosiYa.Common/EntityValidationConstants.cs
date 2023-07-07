namespace NosiYa.Common
{
    public static class EntityValidationConstants
    {
        public static class Region
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }
        public static class OutfitBase
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int DescriptionMaxLength = 1000;

            public const int PictureMaxLength = 2048;

            public const int RenterTypeMinValue= 1;
            public const int RenterTypeMaxValue = 5;

            public const int ColorMinLength = 3;
            public const int ColorMaxLength = 20;
        }

        public static class Event
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 2000;

            public const int LocationMinLength = 10;
            public const int LocationMaxLength = 500;
            

        }
    }
}
