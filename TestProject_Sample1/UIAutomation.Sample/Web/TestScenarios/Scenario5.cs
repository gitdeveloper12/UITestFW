// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using System.Collections.Generic;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Web
{
    [TestClass]
    public class Scenario5
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private static Api.Browser xrmBrowser;

        [TestMethod]
        public void WebTestScecnario5()
        {
            using (xrmBrowser = new Api.Browser(TestSettings.Options))
            {
                try
                {
                    xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);

                    xrmBrowser.ThinkTime(1000);

                    xrmBrowser.GuidedHelp.CloseGuidedHelp();

                    WebCreateContact("Adobe", "Contact_New", "test1@adobe1.com", "Adobe Account");

                    xrmBrowser.Grid.OpenRecord(0);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    WebDeleteContacts("Contact_New");
                }
            }
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
            xrmBrowser.Entity.SetValue("mobilephone", "555-555-5555");
            xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1980"));
            xrmBrowser.Entity.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Email" });

            xrmBrowser.CommandBar.ClickCommand("Save");

        }

        private void WebDeleteContacts(string contact)
        {
            xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

            xrmBrowser.Grid.SwitchView("My Active Contacts");

            xrmBrowser.Grid.Search(contact);
            xrmBrowser.Grid.OpenRecord(0);

            //Click the Delete button from the command bar
            //xrmBrowser.CommandBar.ClickCommand("Delete", "", true, 3000); //Use this option if Delete is under the More Commands menu
            xrmBrowser.CommandBar.ClickCommand("Delete"); //Use this option if Delete is directly visible on the command bar

            //Click the Delete button on the dialog
            xrmBrowser.Dialogs.Delete();
        }
    }
}