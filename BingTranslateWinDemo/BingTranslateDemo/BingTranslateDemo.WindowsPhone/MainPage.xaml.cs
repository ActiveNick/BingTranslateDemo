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
using Windows.Media.SpeechSynthesis;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BingTranslateDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // The object for controlling the speech synthesis engine (voice).
        SpeechSynthesizer speech;

        // The media object for controlling and playing audio.
        MediaElement mediaplayer;

        // The Translator object from our PCL that will call the Bing Translate API
        Translator tr;

        public static uint HResultPrivacyStatementDeclined = 0x80045509;
        public static string instructions = "You can type or say what you want to translate. Then select your target language and tap the Translate button";

        public MainPage()
        {
            this.InitializeComponent();

            speech = new SpeechSynthesizer();
            mediaplayer = new MediaElement();

            lstLanguages.ItemsSource = SpeechSynthesizer.AllVoices;
            lstLanguages.SelectedValuePath = "Language";
            lstLanguages.SelectedValue = SpeechSynthesizer.DefaultVoice.Language;

            tr = new Translator();

            lblResult.Text = instructions;

            Windows.System.Display.DisplayRequest dr = new Windows.System.Display.DisplayRequest();
            dr.RequestActive();
        }

        private async void btnTalk_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of SpeechRecognizer.
            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

            // Compile the dictation grammar that is loaded by default.
            await speechRecognizer.CompileConstraintsAsync();

            // Start recognition.
            try
            {
                Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();
                // If successful, display the recognition result.
                if (speechRecognitionResult.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
                {
                    txtSource.Text = speechRecognitionResult.Text;
                }
            }
            catch (Exception exception)
            {
                if ((uint)exception.HResult == HResultPrivacyStatementDeclined)
                {
                    //this.resultTextBlock.Visibility = Visibility.Visible;
                    lblResult.Text = "I'm sorry, I was not able to use speech recognition. The speech privacy statement was declined.";
                }
                else
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                    messageDialog.ShowAsync().GetResults();
                }
            }
        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            Translate();
        }

        private async void Translate()
        {
            VoiceInformation voice = (VoiceInformation)lstLanguages.SelectedItem;
            string language = voice.Language;

            lblResult.Text = await tr.TranslateString(txtSource.Text, language);
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            ReadText(lblResult.Text);
        }

        private async void ReadText(string mytext)
        {
            //Retrieve the first female voice
            speech.Voice = (VoiceInformation)lstLanguages.SelectedItem;
            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await speech.SynthesizeTextToStreamAsync(mytext);

            // Send the stream to the media object.
            mediaplayer.SetSource(stream, stream.ContentType);
            mediaplayer.Play();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = "";
            lblResult.Text = instructions;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            // Nothing to do yet
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // Nothing to do yet
        }
    }
}
