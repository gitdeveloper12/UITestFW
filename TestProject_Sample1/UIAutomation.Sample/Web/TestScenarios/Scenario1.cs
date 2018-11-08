using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Web.TestScenarios
{
    [TestClass]
    public class Scenario1
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private static Api.Browser xrmBrowser;

        [TestMethod]
        public void WebTestScenario1()
        {
            using (xrmBrowser = new Api.Browser(TestSettings.Options))
            {
                string accountName1 = "Adobe Account";
                string accountName2 = "Microsoft Account";

                try
                {
                    xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                    xrmBrowser.ThinkTime(1000);
                    xrmBrowser.GuidedHelp.CloseGuidedHelp();

                    WebCreateAccount(accountName1, "123-123-2345", "www.campaign.com");
                    WebCreateAccount(accountName2, "234-678-1234", "www.exchange.com");

                    WebCreateContact("Adobe", "Contact", "test1@adobe1.com", accountName1);

                    WebSearchAccount(accountName1);
                    WebSearchAccount(accountName2);

                    WebCreateContact("Microsoft", "Contact", "test2@adobe1.com", accountName2);
                    WebSearchAccount(accountName2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    WebDeleteAccounts(accountName1, accountName2);
                }
            }
        }



        public void WebCreateAccount(string accountName, string telephone, string websiteurl)
        {
            xrmBrowser.ThinkTime(3000);
            xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

            xrmBrowser.ThinkTime(2000);

            xrmBrowser.CommandBar.ClickCommand("New");

            xrmBrowser.ThinkTime(1000);
            xrmBrowser.Entity.SetValue("name", accountName);
            xrmBrowser.Entity.SetValue("telephone1", telephone);
            xrmBrowser.Entity.SetValue("websiteurl", websiteurl);

            xrmBrowser.CommandBar.ClickCommand("Save & Close");
            xrmBrowser.ThinkTime(2000);
        }

        public void WebSearchAccount(string accountName)
        {
            xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
            xrmBrowser.Grid.SwitchView("My Active Accounts");
            xrmBrowser.Grid.Search(accountName);
            xrmBrowser.Grid.OpenRecord(0);
            xrmBrowser.ThinkTime(1000);

            string lookupValue = xrmBrowser.Entity.GetValue(new LookupItem { Name = "primarycontactid" });
            Console.WriteLine(lookupValue);
        }

        public void WebCreateContact(string contactFirstName, string contactLastName, string emailaddress, string accountName)
        {

            xrmBrowser.ThinkTime(1000);
            xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

            xrmBrowser.ThinkTime(500);
            xrmBrowser.Grid.SwitchView("Active Contacts");

            xrmBrowser.ThinkTime(1000);
            xrmBrowser.CommandBar.ClickCommand("New");

            xrmBrowser.ThinkTime(5000);

            var fields = new List<Field>
                {
                    new Field() {Id = "firstname", Value = contactFirstName},
                    new Field() {Id = "lastname", Value = contactLastName}
                };
            xrmBrowser.Entity.SetValue(new CompositeControl() { Id = "fullname", Fields = fields });
            xrmBrowser.Entity.SetValue("emailaddress1", emailaddress);
            xrmBrowser.Entity.SetValue("parentcustomerid_d", accountName);
            //xrmBrowser.Entity.SetValue("mobilephone", "555-555-5555");
            //xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1980"));
            xrmBrowser.Entity.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Email" });

            xrmBrowser.CommandBar.ClickCommand("Save");

        }

        private void WebDeleteContacts(string contact1, string contact2)
        {
            xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

            xrmBrowser.Grid.SwitchView("My Active Contacts");

            xrmBrowser.Grid.Search(contact1);
            xrmBrowser.Grid.OpenRecord(0);
            xrmBrowser.CommandBar.ClickCommand("Delete");
            xrmBrowser.Dialogs.Delete();
            xrmBrowser.ThinkTime(3000);

            xrmBrowser.Grid.Search(contact2);
            xrmBrowser.Grid.OpenRecord(0);
            xrmBrowser.CommandBar.ClickCommand("Delete");
            xrmBrowser.Dialogs.Delete();
        }

        private void WebDeleteAccounts(string accountName1, string accountName2)
        {
            xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

            xrmBrowser.Grid.SwitchView("My Active Accounts");

            xrmBrowser.Grid.Search(accountName1);
            xrmBrowser.ThinkTime(300);
            xrmBrowser.Grid.OpenRecord(0);
            xrmBrowser.CommandBar.ClickCommand("Delete");
            xrmBrowser.Dialogs.Delete();
            xrmBrowser.ThinkTime(3000);

            xrmBrowser.Grid.SwitchView("My Active Accounts");
            xrmBrowser.Grid.Search(accountName2);
            xrmBrowser.ThinkTime(300);
            xrmBrowser.Grid.OpenRecord(0);
            xrmBrowser.CommandBar.ClickCommand("Delete");
            xrmBrowser.Dialogs.Delete();

            xrmBrowser.ThinkTime(3000);
        }
    }
}