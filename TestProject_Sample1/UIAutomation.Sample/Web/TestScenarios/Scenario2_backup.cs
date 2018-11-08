// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Web
{
    [TestClass]
    public class Scenario2_Update
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private static Api.Browser xrmBrowser;

        [TestMethod]
        public void WebTestScecnario2_Update()
        {
            using (xrmBrowser = new Api.Browser(TestSettings.Options))
            {
                try
                {
                    xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                    xrmBrowser.GuidedHelp.CloseGuidedHelp();

                    xrmBrowser.ThinkTime(500);
                    xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                    xrmBrowser.Grid.SwitchView("Active Accounts");

                    xrmBrowser.Grid.SelectRecord(0);
                    xrmBrowser.Grid.SelectRecord(1);
                    bool result = false;
                    ReadOnlyCollection<IWebElement> commands = xrmBrowser.CommandBar.GetCommands();
                    IWebElement element = commands[5];
                    string value = "MERGE";
                    if (element.Text != null && element.Text.Contains(value))
                    {

                        result = true;
                    }
                    Assert.AreEqual(result, true);
                    //TODO - check if Merge keyword is in the collection of getCommands
                    //xrmBrowser.CommandBar.ClickCommand("Merge");
                    xrmBrowser.ThinkTime(500);

                    xrmBrowser.Grid.SelectRecord(0);
                    xrmBrowser.Grid.SelectRecord(1);

                    //bool resultAfter = false;
                    //element = commands[5];
                    //if (element.Text != null && element.Text.Contains(value))
                    //{
                    //    resultAfter = true;
                    //}
                    //Assert.AreEqual(resultAfter, false);
                    //xrmBrowser.CommandBar.ClickCommand("Merge");

                    xrmBrowser.ThinkTime(500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}