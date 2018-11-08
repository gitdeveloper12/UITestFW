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
    public class Scenario3
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private static Api.Browser xrmBrowser;

        [TestMethod]
        public void WebTestScecnario3()
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
                    //Scenario 3
                    xrmBrowser.Grid.SelectRecord(0);
                    xrmBrowser.Grid.SelectRecord(1);
                    //xrmBrowser.CommandBar.ClickCommand("Merge");
                    //xrmBrowser.ThinkTime(500);

                    //xrmBrowser.Grid.SelectRecord(0);
                    //xrmBrowser.Grid.SelectRecord(1);
                    //xrmBrowser.CommandBar.ClickCommand("Merge");


                    //xrmBrowser.ThinkTime(500);
                    //ID for the dialog
                    //xrmBrowser.Related.SwitchToDialogFrame();
                    ////TODO - try the different options here - check the selenium component associated
                    //xrmBrowser.Related.SwitchToContentFrame();

                    //xrmBrowser.Grid.SelectRecord(0);
                    //xrmBrowser.Grid.SelectRecord(1);

                    //xrmBrowser.Dialogs.MergeAccounts();



                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}