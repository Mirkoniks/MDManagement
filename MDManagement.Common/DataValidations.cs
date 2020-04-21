namespace MDManagement.Common
{
    public static class DataValidations
    {
        public static class Adress
        {
            public const int AddressMaxLength = 150;
        }

        public static class Company
        {
            public const int NameMaxLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const int CompanyCodeMaxValue = 15;
        }

        public static class Employee
        {
            public const int NameMaxLength = 30;
            public const string SalaryDeciamlSecifications = "decimal (18,4)";
        }

        public static class Department
        {
            public const int NameMaxLength = 30;
        }

        public static class JobTitle
        {
            public const int NameMaxLength = 30;
            public const int DescriptionMaxLength = 100;
        }

        public static class Project
        {
            public const int NameMaxLength = 30;
            public const int DescriptionMaxLength = 100;
        }

        public static class Town
        {
            public const int NameMaxLength = 30;
            public const int PostCodeMaxLength = 4;
        }
    }
}