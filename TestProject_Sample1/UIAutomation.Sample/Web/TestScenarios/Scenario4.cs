// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api.Pages;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Web
{
    [TestClass]
    public class Scenario4
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private static Api.Browser xrmBrowser;

        [TestMethod]
        public void WebTestScecnario4()
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

                    xrmBrowser.Grid.OpenRecord(0);
                    //xrmBrowser.CommandBar.ClickCommand("Transaction");
                    xrmBrowser.ThinkTime(500);

                    xrmBrowser.Dialogs.SwitchToPopupWindow();
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