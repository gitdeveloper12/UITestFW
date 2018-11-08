using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Phase2Templates
{
    public class CreateEntity_Trial1
    {
        string area;
        string subarea;
        string subgrid;
        string name;
        Dictionary<string, string> entityDetails = new Dictionary<string, string>(); //Fetch the data from DatabaseEntity


        [TestMethod]
        public void CreateEntityData(string area, string subarea, string subgrid, Dictionary<string, string> entityDetails, Api.Browser XrmTestBrowser)
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

            try
            {
                XrmTestBrowser.CommandBar.ClickCommand("Save & Close");
                XrmTestBrowser.ThinkTime(2000);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception encountered while saving entity");
               
                //TODO :
                //Xpath for div which contains inline validation message
                //div[@class='ms-crm-Inline-Validation']
                //Eg: driver.findElement(By.xpath("//div[@class='notesData']/div[@class='notesDate']")).getText();
                Console.WriteLine(e.Message);
            }

            XrmTestBrowser.Navigation.OpenSubArea(area, subarea);

            XrmTestBrowser.ThinkTime(2000);

            //Currently hard coding it - need to find a way to dynamically get the record name
            string name = "Test Adobe";

            XrmTestBrowser.Grid.Search(name);
            XrmTestBrowser.ThinkTime(300);
            
            try
            {
                //TODO
                //Find a way to assert the record name and verify that the entity created can be read - Assert.Equal
                //XrmTestBrowser.Entity.CollapseTab("Summarry");
                //Assert.Fail("Summarry is an invalid Tab name and hence should have failed");
                XrmTestBrowser.Entity.GetValue("name");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception encountered while retrieving saved entity");
                Console.WriteLine(e.Message);
            }
            XrmTestBrowser.ThinkTime(200);
        }
    }
}
