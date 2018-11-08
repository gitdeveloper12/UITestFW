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
    public class EntityWrapper : TestBaseTemplate
    {
        string area;
        string subarea;
        string subgrid;
        string name;
        string logicalName;
        Dictionary<string, string> entityDetails = new Dictionary<string, string>(); //Fetch the data from DatabaseEntity

        [TestInitialize]
        public override void TestSetup()
        {
            XrmTestBrowser.ThinkTime(500);
            logicalName = "Accounts";
        }

        [TestMethod]
        public void EntityOperation()
        {

            string account = "account";
            logicalName = logicalName.ToLower();
            if (logicalName.Contains(account)){
                area = "Sales";
                subarea = "Accounts";
                subgrid = "My Active Accounts";
            }
            DatabaseEntity entityData = new DatabaseEntity();
            Dictionary<string, string> entityDetails = entityData.RetrieveDataEntity(logicalName);
            CreateEntity_Trial1 entity = new CreateEntity_Trial1();
            entity.CreateEntityData(area, subarea, subgrid, entityDetails, XrmTestBrowser);
        }
    }
}
