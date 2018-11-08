using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Web.SampleTemplates
{
    /// <summary>
    /// Summary description for TestCreateAccount
    /// </summary>
    [TestClass]
    public class TestUpdateAccount
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private static Api.Browser xrmBrowser;
        private string accountName;
        private string webSiteURL;
        private string telephone;
        public TestUpdateAccount()
        {
            accountName = "Adobe Account_Updated";
            telephone = "123-123-2345";
            webSiteURL = "www.campaign.com";
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void UpdateAccountTest()
        {
            using (xrmBrowser = new Api.Browser(TestSettings.Options))
            {
                try
                {
                    xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                    xrmBrowser.ThinkTime(1000);
                    xrmBrowser.GuidedHelp.CloseGuidedHelp();

                    WebUpdateAccount(accountName, telephone, webSiteURL); 
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    WebDeleteAccounts(accountName);
                }
            }
        }

        public void WebUpdateAccount(string accountName, string telephone, string websiteurl)
        {
            xrmBrowser.ThinkTime(3000);
            xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

            xrmBrowser.ThinkTime(1000);

            xrmBrowser.Grid.Search("Test Adobe");
            xrmBrowser.ThinkTime(300);
            xrmBrowser.Grid.OpenRecord(0);
            xrmBrowser.Entity.SetValue("name", accountName);
            xrmBrowser.Entity.SetValue("telephone1", telephone);
            xrmBrowser.Entity.SetValue("websiteurl", websiteurl);
            xrmBrowser.Entity.Save();

            //xrmBrowser.CommandBar.ClickCommand("Save & Close");
            xrmBrowser.ThinkTime(2000);
        }

        private void WebDeleteAccounts(string accountName)
        {
            xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

            xrmBrowser.Grid.SwitchView("My Active Accounts");

            xrmBrowser.Grid.Search(accountName);
            xrmBrowser.ThinkTime(300);
            xrmBrowser.Grid.OpenRecord(0);
            xrmBrowser.CommandBar.ClickCommand("Delete");
            xrmBrowser.Dialogs.Delete();
            xrmBrowser.ThinkTime(3000);

        }
    }
}
