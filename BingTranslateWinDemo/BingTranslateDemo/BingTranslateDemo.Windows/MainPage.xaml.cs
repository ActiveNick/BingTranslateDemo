//==========================================================================
//
// Author:  Nick Landry
// Title:   Senior Technical Evangelist - Microsoft US DX - NY Metro
// Twitter: @ActiveNick
// Blog:    www.AgeofMobility.com
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Disclaimer: Portions of this code may been simplified to demonstrate
// useful application development techniques and enhance readability.
// As such they may not necessarily reflect best practices in enterprise 
// development, and/or may not include all required safeguards.
// 
// This code and information are provided "as is" without warranty of any
// kind, either expressed or implied, including but not limited to the
// implied warranties of merchantability and/or fitness for a particular
// purpose.
//
// To learn more about Universal Windows app development using Cortana
// and the Speech SDK, watch the full-day course for free on
// Microsoft Virtual Acdemy (MVA) at http://aka.ms/cortanamva
//
//==========================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BingAPILibrary;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BingTranslateDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Translator tr;

        public MainPage()
        {
            this.InitializeComponent();

            Windows.System.Display.DisplayRequest dr = new Windows.System.Display.DisplayRequest();
            dr.RequestActive();

            tr = new Translator();


            //cboLanguage.Items.Add("de-DE");
            //cboLanguage.Items.Add("en-US");
            //cboLanguage.Items.Add("fr-FR");

            //cboLanguage.SelectedItem = "fr-FR";
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> languages = await tr.GetLanguagesForTranslate();

            foreach(string language in languages)
            {
                cboLanguage.Items.Add(language);
            }
            if (cboLanguage.Items.Count > 0)
                cboLanguage.SelectedItem = "fr";
        }

        private async void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Text = await tr.TranslateString(txtSource.Text, cboLanguage.SelectedValue.ToString());
        }
    }
}
