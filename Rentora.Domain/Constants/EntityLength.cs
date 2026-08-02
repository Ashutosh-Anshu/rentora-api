namespace Rentora.Domain.Constants
{
    public static class EntityLength
    {
        // General
        public const int Name = 100;
        public const int DisplayName = 100;
        public const int Title = 150;
        public const int ShortCode = 50;
        public const int Code = 100;

        // Text
        public const int Description = 500;
        public const int Notes = 1000;
        public const int Address = 500;

        // Contact
        public const int Email = 256;          // ASP.NET Identity default
        public const int PhoneNumber = 20;     // Supports country code
        public const int MobileNumber = 20;

        // User
        public const int FullName = 100;

        // UI
        public const int Icon = 50;
        public const int Route = 200;
        public const int Url = 500;

        // Location
        public const int Country = 100;
        public const int State = 100;
        public const int City = 100;
        public const int ZipCode = 20;

        // Property
        public const int PropertyCode = 30;
        public const int UnitCode = 30;

        // Payment
        public const int TransactionId = 100;
        public const int ReferenceNumber = 100;

        // File
        public const int FileName = 255;
        public const int FileExtension = 20;
    }
}
