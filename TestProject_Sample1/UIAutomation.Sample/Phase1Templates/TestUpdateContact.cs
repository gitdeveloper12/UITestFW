// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Collections.Generic;
using System.Security;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Web
{
    [TestClass]
    public class TestUpdateContact
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void WEBTestUpdateContact()
        {
            using (var xrmBrowser = new Api.Browser(TestSettings.Options))
            {
                string contact = "Adobe Contact";
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                xrmBrowser.Grid.SwitchView("My Active Contacts");

                xrmBrowser.Grid.Search(contact);
                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.ThinkTime(5000);

                var fields = new List<Field>
                {
                    new Field() {Id = "firstname", Value = "Adobe_Update"},
                    new Field() {Id = "lastname", Value = "Contact"}
                };
                xrmBrowser.Entity.SetValue(new CompositeControl() { Id = "fullname", Fields = fields});
                xrmBrowser.Entity.SetValue("emailaddress1", "test@contoso.com");
                xrmBrowser.Entity.SetValue("mobilephone", "666-666-6666");
                xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1990"));
                xrmBrowser.Entity.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Phone"});

                //XrmTestBrowser.Entity.Save();

            }
        }
    }
}