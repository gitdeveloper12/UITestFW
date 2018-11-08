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
    public class UpdateEntity : TestBaseTemplate
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
            //testDataOpportunity();
            testDataContact();
        }

        private void testDataAccount()
        {
            area = "Sales";
            subarea = "Accounts";
            subgrid = "My Active Accounts";
            //entityDetails.Add("name", "Test Adobe UpdatedValue");
            //entityDetails.Add("telephone1", "5555556665");
            //entityDetails.Add("websiteurl", "www.update1.com");
            entityDetails.Add("sic_c", "Code");
            entityDetails.Add("fax", "1232114");

        }

        private void testDataOpportunity()
        {
            area = "Sales";
            subarea = "Opportunities";
            subgrid = "My Open Opportunities";
            entityDetails.Add("description", "Test Opportunity New Update");
            entityDetails.Add("budgetamount", "50600");
        }

        private void testDataContact()
        {
            area = "Sales";
            subarea = "Contacts";
            subgrid = "My Open Contacts";
            name = "Test Adobe Contact";
            //entityDetails.Add("budgetamount_cl", "10000");
            entityDetails.Add("emailaddress1", "test@contoso.com");
            entityDetails.Add("mobilephone", "555-555-5555");
        }

        [TestMethod]
        public void UpdateDataEntity()
        {
            XrmTestBrowser.ThinkTime(5000);
            XrmTestBrowser.Navigation.OpenSubArea(area, subarea);

            XrmTestBrowser.ThinkTime(2000);

            //Currently hard coding it - need to find a way to dynamically get the record name
            string name = "Test Adobe";

            XrmTestBrowser.Grid.Search(name);
            XrmTestBrowser.ThinkTime(300);
            XrmTestBrowser.Grid.OpenRecord(0);

            List<string> fields = entityDetails.Keys.ToList();
            XrmTestBrowser.ThinkTime(1000);

            foreach (string field in fields)
            {
                string fieldValue = entityDetails[field];
                XrmTestBrowser.Entity.SetValue(field, fieldValue);
                //XrmTestBrowser.ThinkTime(1000);
                //XrmTestBrowser.Entity.Save();
                //XrmTestBrowser.ThinkTime(1000);
            }
            XrmTestBrowser.Entity.Save();
            XrmTestBrowser.ThinkTime(2000);
        }
    }
}
