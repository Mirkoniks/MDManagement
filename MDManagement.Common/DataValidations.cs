namespace MDManagement.Common
{
    public static class DataValidations
    {
        public static class Adress
        {
            public const int AddressMaxValue = 150;
        }

        public static class Company
        {
            public const int CompanyCodeMaxValue = 15;
            public const int NameMaxValue = 30;
        }

        public static class Employee
        {
            public const int NameMaxValue = 30;
            public const string SalaryDeciamlSecifications = "decimal (18,4)";
        }

        public static class Department
        {
            public const int NameMaxValue = 30;
        }

        public static class JobTitle
        {
            public const int NameMaxValue = 30;
            public const int DescriptionMaxValue = 100;
        }

        public static class Project
        {
            public const int NameMaxValue = 30;
            public const int DescriptionMaxValue = 100;
        }

        public static class Town
        {
            public const int NameMaxValue = 30;
            public const int PostCodeMaxValue = 4;
        }
    }
}