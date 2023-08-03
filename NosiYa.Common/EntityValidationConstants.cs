namespace NosiYa.Common
{
    public static class EntityValidationConstants
    {
        public static class Region
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 2000;

            public const int ImageMaxLength = 2048;
        }
        public static class Outfit
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int DescriptionMaxLength = 1000;

            public const int ImageMaxLength = 2048;

            public const int RenterTypeMinValue= 0; 
            public const int RenterTypeMaxValue = 4;

            public const int NumberOfPartsMinValue = 1;
            public const int NumberOfPartsMaxValue = 10;

            public const int ColorMinLength = 3;
            public const int ColorMaxLength = 20;

            public const int SizeMinLength = 3;
            public const int SizeMaxLength = 10;

            public const int OwnerEmailMinLength = 3;
            public const int OwnerEmailMaxLength = 50; //TODO regex
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

        public static class Comment
        {
            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 2000;
        }

        public static class Image
        {
            public const int ImageUrlMinLength = 3;
            public const int ImageUrlMaxLength = 1500;

            public const int ImageResizeMaxHeight = 1080;
            public const int ImageResizeMaxWidth = 1920;
        }

    }
}
