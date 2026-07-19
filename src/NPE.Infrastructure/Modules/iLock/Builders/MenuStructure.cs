using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.iLock.Builders
{
    public static class MenuStructure
    {
        // LEVEL3
        public static readonly IReadOnlyDictionary<string, string> Level3Parent = BuildLevel3();

        // LEVEL 1

        public static readonly HashSet<string> Level1 = new(StringComparer.OrdinalIgnoreCase)
        {
                //"Appointment",
                "Reception",
                "SampleCollection",
                "Laboratory",
                "Management",
                //"Reports",
                "User",
                //"Radiology",
                //"BloodBank",
                //"UpdateNexusPro"
        };

        // LEVEL2 → LEVEL1

        public static readonly Dictionary<string, string> Level2Parent = new(StringComparer.OrdinalIgnoreCase)
        {
                // Reception
                //{ "Patients", "Reception" },
                { "PatientList", "Reception" },
                //{ "PatientWizard", "Reception" },
                //{ "Expenses","Reception" },
                //{ "Cash","Reception" },

                // Sample Collection
                { "Dispatch","SampleCollection" },
                //processing

                // Management
                { "ReferenceInvoiceManagement","Management" },
                { "CenterManagement","Management" },
                { "UsersManagement","Management" },
                { "LocationsManagement","Management" },
                { "Security","Management" },
                //{ "LabelSecurityCenter","Management" },
                //{ "TestManagement","Management" },
                { "Report","Management" },
                { "NexusManagement","Management" },
                //{ "Clinics","Management" },

                // Reports
                { "ReportLayout", "Reports" },
                { "19", "Reports"},                  //Cash Reports
                { "5", "Reports"},                   //Center's Reports
                { "7", "Reports"},                   //Consultants Reports 
                { "26", "Reports"},                  //Expense Reports 
                { "18", "Reports"},                  //Patient's  Information Reports 
                { "16", "Reports"},                  //Rate Types Reports 
                { "6", "Reports"},                   //Reference Reports 
                { "ReportsLists", "Reports"},
                { "ReportsTestCount", "Reports"},
                { "9", "Reports"},                   //Test BOM Reports 
                { "8", "Reports"},                   //Test Department Reports 
                { "10", "Reports"},                  //Test Groups Reports
                { "11", "Reports"},                  //Test Regents Reports
                { "15", "Reports"},                  //Test Remarks Reports
                { "12", "Reports"},                  //Test Specimen Reports
                { "17", "Reports"},                  //Test's Reports
                { "13", "Reports"},                  //User's Reports
        };

        // LEVEL3 → LEVEL2
        private static void Add(Dictionary<string, string> dict, string key, string parent)
        {
            if (dict.ContainsKey(key))
            {
                throw new Exception($"Duplicate menu key: {key}");
            }

            dict.Add(key, parent);
        }

        //public static readonly Dictionary<string, string> Level3Parent = new(StringComparer.OrdinalIgnoreCase)
        private static Dictionary<string, string> BuildLevel3()
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            #region Reception

            //Add(dict, "Patients", "Reception");
            //Add(dict, "PatientList", "Reception");
            //Add(dict, "PatientWizard", "Reception");

            //--------------------------------
            // Expenses
            //--------------------------------

            //Add(dict, "NewExpense", "Expenses");
            //Add(dict, "ExpenseList", "Expenses");

            //--------------------------------
            // Cash
            //--------------------------------

            //Add(dict, "CashSummary", "Cash");
            //Add(dict, "CashLog", "Cash");
            //Add(dict, "Denomination", "Cash");
            //Add(dict, "DenominationList", "Cash");

            #endregion

            #region SampleCollection SMD

            //-----------------------------------
            // SampleCollection
            //-----------------------------------

            Add(dict, "ReceivedSample", "SampleCollection");
            //Add(dict, "SampleCanceled", "SampleCollection");
            //Add(dict, "SampleToCome", "SampleCollection");
            //Add(dict, "SampleSendToLab", "SampleCollection");
            //Add(dict, "SampleDelayed", "SampleCollection");
            //Add(dict, "SampleConducted", "SampleCollection");
            //Add(dict, "SampleApproved", "SampleCollection");
            //Add(dict, "SampleDelivered", "SampleCollection");

            //Add(dict, "SampleDrawn", "SampleCollection");
            //Add(dict, "ProcessSample", "SampleCollection");
            //Add(dict, "SendToHeadOffice", "SampleCollection");

            //Add(dict, "ConductTest", "SampleCollection");
            //Add(dict, "PendingTest", "SampleCollection");
            //Add(dict, "PendingTestByCenter", "SampleCollection");
            //Add(dict, "TestProcess", "SampleCollection");

            //--------------------------------
            // Dispatch
            //--------------------------------

            Add(dict, "DispatchList", "Dispatch");
            Add(dict, "LoyaltyCardWorkSheet", "Dispatch");

            #endregion

            #region Laboratory              
            Add(dict, "TestHistory", "Laboratory");
            Add(dict, "ResultEntry", "Laboratory");
            Add(dict, "DoctorApproval", "Laboratory");
            #endregion

            #region Management

            //-----------------------------------
            // Management
            //-----------------------------------

            Add(dict, "References", "Management");
            //Add(dict, "Announcements", "Management");
            //Add(dict, "CardTypeList", "Management");
            //Add(dict, "CardInfoList", "Management");
            //Add(dict, "RateTypes", "Management");
            //Add(dict, "SignatureSetting", "Management");
            //Add(dict, "AlertList", "Management");
            //Add(dict, "TestRateList", "Management");

            //--------------------------------
            // CenterManagement
            //--------------------------------

            Add(dict, "Centers", "CenterManagement");

            //--------------------------------
            // TestManagement
            //--------------------------------

            Add(dict, "Tests", "TestManagement");
            Add(dict, "Departments", "TestManagement");
            Add(dict, "TestGroups", "TestManagement");
            Add(dict, "TestSpecimen", "TestManagement");
            Add(dict, "TestBOMs", "TestManagement");
            Add(dict, "TestRegents", "TestManagement");
            Add(dict, "TestTemplates", "TestManagement");
            Add(dict, "AntimicrobialList", "TestManagement");
            Add(dict, "Remarks", "TestManagement");
            Add(dict, "TextCode", "TestManagement");

            //--------------------------------
            // UsersManagement
            //--------------------------------

            //Add(dict, "Doctors", "UsersManagement");
            Add(dict, "Consultants", "UsersManagement");

            //--------------------------------
            // Locations
            //--------------------------------

            Add(dict, "Regions", "LocationsManagement");

            //--------------------------------
            // Security
            //--------------------------------

            Add(dict, "SecurityCenter", "Security");
            Add(dict, "ReportAccessSettings", "Security");

            //--------------------------------
            // Clinics
            //--------------------------------
            Add(dict, "14", "Clinics");

            #endregion

            #region Report

            ////--------------------------------
            //// Report
            ////--------------------------------

            //Add(dict, "ReportLayout", "Report");
            //Add(dict, "19", "Report");                  //Cash Reports
            //Add(dict, "5", "Report");                   //Center's Reports
            //Add(dict, "7", "Report");                   //Consultants Reports 
            //Add(dict, "26", "Report");                  //Expense Reports 
            //Add(dict, "18", "Report");                  //Patient's  Information Reports 
            //Add(dict, "16", "Report");                  //Rate Types Reports 
            //Add(dict, "6", "Report");                   //Reference Reports 
            //Add(dict, "ReportsLists", "Report");
            //Add(dict, "ReportsTestCount", "Report");
            //Add(dict, "9", "Report");                   //Test BOM Reports 
            //Add(dict, "8", "Report");                   //Test Department Reports 
            //Add(dict, "10", "Report");                  //Test Groups Reports
            //Add(dict, "11", "Report");                  //Test Regents Reports
            //Add(dict, "15", "Report");                  //Test Remarks Reports
            //Add(dict, "12", "Report");                  //Test Specimen Reports
            //Add(dict, "17", "Report");                  //Test's Reports
            //Add(dict, "13", "Report");                  //User's Reports

            #endregion

            #region Radiology
            //-----------------------------------
            // Radiology
            //-----------------------------------

            Add(dict, "AppointmentList", "Radiology");
            Add(dict, "ModalityList", "Radiology");
            Add(dict, "ServiceList", "Radiology");
            Add(dict, "TimingList", "Radiology");
            Add(dict, "RISAppointment", "Radiology");
            Add(dict, "ReceptionRadiology", "Radiology");
            Add(dict, "RadItemsList", "Radiology");
            Add(dict, "InProcessToken", "Radiology");

            #endregion

            #region Update NexusPro
            //-----------------------------------
            // Update
            //-----------------------------------

            Add(dict, "UpdateNow", "UpdateNexusPro");
            Add(dict, "UpdateInfo", "UpdateNexusPro");
            #endregion

            return dict;
        }

        public static readonly Dictionary<string, int> Level1SortOrder = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Reception", 1 },
            { "SampleCollection", 2 },
            { "Laboratory", 3 },
            { "Management", 4 },
            { "Reports", 5 },
            { "Radiology", 6 },
            { "BloodBank", 7 },
            { "User", 8 },
            { "UpdateNexusPro", 9 }
        };

        public static readonly Dictionary<string, int> ChildSortOrder = new(StringComparer.OrdinalIgnoreCase)
        {
            // Reception children
            { "PatientList", 1 },
            { "Patients", 2 },
            { "PatientWizard", 3 },
            { "Cash", 4 },
            { "Expenses", 5 },

            //// Cash children
            //{ "CashLog", 1 },
            //{ "CashSummary", 2 },
            //{ "Denomination", 3 },
            //{ "DenominationList", 4 },

            // Expenses children
            { "NewExpense", 1 },
            { "ExpenseList", 2 },

            // SampleCollection children
            { "ReceivedSample", 1 },
            { "SampleCanceled", 2 },
            { "SampleToCome", 3 },
            { "SampleSendToLab", 4 },
            { "SampleDelayed", 5 },
            { "SampleConducted", 6 },
            { "SampleApproved", 7 },
            { "SampleDelivered", 8 },
            { "SampleDrawn", 9 },
            { "ProcessSample", 10 },
            { "SendToHeadOffice", 11 },
            { "ConductTest", 12 },
            { "PendingTest", 13 },
            { "PendingTestByCenter", 14 },
            { "TestProcess", 15 },

            // Laboratory children
            { "TestHistory", 1 },
            { "ResultEntry", 2 },
            { "DoctorApproval", 3 },

            // Management children
            { "References", 1 },
            { "Announcements", 2 },
            { "CardTypeList", 3 },
            { "CardInfoList", 4 },
            { "RateTypes", 5 },
            { "SignatureSetting", 6 },
            { "AlertList", 7 },
            { "TestRateList", 8 },
            { "CenterManagement", 9 },
            { "TestManagement", 10 },
            { "UsersManagement", 11 },
            { "LocationsManagement", 12 },
            { "Security", 13 },
            { "LabelSecurityCenter", 14 },
            { "Clinics", 15 },
            { "Report", 16 },

            // TestManagement children
            { "Tests", 1 },
            { "Departments", 2 },
            { "TestGroups", 3 },
            { "TestSpecimen", 4 },
            { "TestBOMs", 5 },
            { "TestRegents", 6 },
            { "TestTemplates", 7 },
            { "AntimicrobialList", 8 },
            { "Remarks", 9 },
            { "TextCode", 10 },

            // UsersManagement children
            { "Doctors", 1 },
            { "Consultants", 2 },

            // Reports children
            { "8", 1 },

            // LocationsManagement children
            { "Regions", 1 },

            // Security children
            { "SecurityCenter", 1 },
            { "ReportAccessSettings", 2 },

            // Radiology children
            { "AppointmentList", 1 },
            { "ModalityList", 2 },
            { "ServiceList", 3 },
            { "TimingList", 4 },
            { "RISAppointment", 5 },
            { "ReceptionRadiology", 6 },
            { "RadItemsList", 7 },
            { "InProcessToken", 8 }
        };
    }
}
