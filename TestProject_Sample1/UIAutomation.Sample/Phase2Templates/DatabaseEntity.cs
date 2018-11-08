using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Phase2Templates
{
    public class DatabaseEntity
    {
        private Dictionary<string, string> entityDetails;

        public Dictionary<string, string> EntityDetails { get => entityDetails; set => entityDetails = value; }

        //Retrieve the key value pairs of Entity from MongoDB

        public Dictionary<string, string> RetrieveDataEntity(string logicalName)
        {
            if (logicalName.Equals("Opportunity"))
            {
                EntityDetails = testDataCreateAccount();
            }
            else
            {
                EntityDetails = testDataCreateOpportunity();
                Console.WriteLine(logicalName);
            }
            
            return EntityDetails;
        }


        private Dictionary<string, string> testDataCreateAccount()
        {
            EntityDetails = new Dictionary<string, string>();
            EntityDetails.Add("name", "Test Adobe Account");
            EntityDetails.Add("telephone1", "1122334455");
            EntityDetails.Add("websiteurl", "www.testadobe.com");
            return EntityDetails;
        }

        private Dictionary<string, string> testDataCreateOpportunity()
        {
            EntityDetails = new Dictionary<string, string>();
            EntityDetails.Add("name", "Test Adobe Opportunity");
            //entityDetails.Add("budgetamount_cl", "10000");
            EntityDetails.Add("description", "Test Opportunity Creation");
            EntityDetails.Add("budgetamount", "10000");
            return EntityDetails;
        }
    }
}
