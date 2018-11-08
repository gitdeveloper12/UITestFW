using Microsoft.Dynamics365.UIAutomation.Sample.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Phase2Templates
{
    [TestClass]
    public class CreateEntity : TestBaseTemplate
    {
        string area;
        string subarea;
        string subgrid;
        string name;
        Dictionary<string, string> entityDetails = new Dictionary<string, string>(); //Fetch the data from DatabaseEntity

        [TestInitialize]
        public override void TestSetup()
        {
            XrmTestBrowser.ThinkTime(500);
            //testDataAccount();

            testDataOpportunity();
            //testDataContact();
        }

        private void testDataAccount()
        {
            area = "Sales";
            subarea = "Accounts";
            subgrid = "My Active Accounts";
            name = "Test Adobe Account";
            entityDetails.Add("name", "Test Adobe Account");
            entityDetails.Add("telephone1", "1122334455");
            entityDetails.Add("websiteurl", "www.testadobe.com");
        }

        private void testDataOpportunity()
        {
            area = "Sales";
            subarea = "Opportunities";
            subgrid = "My Open Opportunities";
            name = "Test Adobe Opportunity";
            entityDetails.Add("name", "Test Adobe Opportunity");
            //entityDetails.Add("budgetamount_cl", "10000");
            entityDetails.Add("description", "Test Opportunity Creation");
            entityDetails.Add("budgetamount", "10000");
        }

        [TestMethod]
        public void CreateEntityData()
        {
            XrmTestBrowser.ThinkTime(3000);
            XrmTestBrowser.Navigation.OpenSubArea(area, subarea);

            XrmTestBrowser.ThinkTime(2000);

            XrmTestBrowser.CommandBar.ClickCommand("New");

            List<string> fields = entityDetails.Keys.ToList();
            XrmTestBrowser.ThinkTime(1000);

            foreach (string field in fields)
            {
                string fieldValue = entityDetails[field];
                XrmTestBrowser.Entity.SetValue(field, entityDetails[field]);
            }

            XrmTestBrowser.CommandBar.ClickCommand("Save & Close");
            XrmTestBrowser.ThinkTime(2000);
        }

        //Try and test for similar contact data as well
        //Cannot work for Contact since name is an composite field
        private void testDataContact()
        {
            area = "Sales";
            subarea = "Contacts";
            subgrid = "My Open Contacts";
            name = "Test Adobe Contact";
            entityDetails.Add("fullname", "Test Adobe Contact");
            //entityDetails.Add("budgetamount_cl", "10000");
            entityDetails.Add("emailaddress1", "test@contoso.com");
            entityDetails.Add("mobilephone", "555-555-5555");
        }
    }
}
