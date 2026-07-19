namespace NPE.Core.Common.Security
{
    public static class Permissions
    {
        public static class Patients
        {
            public const string Read = "patients.read";
            public const string Create = "patients.create";
            public const string Update = "patients.update";
        }

        public static class Cases
        {
            public const string Create = "cases.create";
        }

        public static class Tests
        {
            public const string Read = "tests.read";
            public const string Create = "tests.create";
        }
    }
}
