# BingTranslateDemo
Mobile Cross-Platform Translator Demo powered by Bing.

Contains 3 solutions:
1. Portable Class Library (PCL) solution for the Bing Tranlator API wrapper
2. Xamarin.Forms solution for iOS, Android and Windows Phone 8 Silverlight
3. Universal Windows (WinRT) app for Windows Store 8.1 and Windows Phone 8.1

Note that the Windows Phone 8.1 project also includes support for the Speech SDK, allowing the user to speak the text they want to translate (Speech Recognition) and hear the translation via Speech Synthesis.

To learn more about app development with the Bing translate services, visit:
http://www.bing.com/dev/en-us/translator

Microsoft Translator API in Azure data Market (up to 2M characters / month free):
http://datamarket.azure.com/dataset/bing/microsofttranslator

You need to obtain a valid application key from the Azure Marketplace, see http://msdn.microsoft.com/en-us/library/hh454950.aspx for details.
THE TRANSLATOR CALLS WILL FAIL UNTIL YOU REPLACE THE TWO VALUES BELOW WITH YOUR OWN ID & SECRET in the class Translator.cs (BingAPILibrary PCL project).
SEE THE LINKS ABOVE FOR INSTRUCTIONS
