# BingTranslateDemo
Mobile Cross-Platform Translator Demo powered by Bing.

Contains 3 solutions:
* Portable Class Library (PCL) solution for the Bing Tranlator API wrapper
* Xamarin.Forms solution for iOS, Android and Windows Phone 8 Silverlight
* Universal Windows 8.1 (WinRT) app for Windows Store 8.1 and Windows Phone 8.1 (also runs on Windows 10)

Note that the Windows Phone 8.1 project also includes support for the Speech SDK, allowing the user to speak the text they want to translate (Speech Recognition) and hear the translation via Speech Synthesis.

To learn more about app development with the Microsoft Translator services, visit:
https://www.microsoft.com/en-us/translator/products.aspx 

Microsoft Translator API in Azure DataMarket (up to 2M characters / month free):
http://datamarket.azure.com/dataset/bing/microsofttranslator

You need to obtain a valid application key from the Azure Marketplace, see http://msdn.microsoft.com/en-us/library/hh454950.aspx for details.

THE TRANSLATOR CALLS WILL FAIL UNTIL YOU REPLACE THE APP ID & SECRET VALUES WITH YOUR OWN. If the code already includes credentials, those are mine. Please get your own or you might hit the 2M characters quota sooner than you think.

You can find them in the class Translator.cs (BingAPILibrary PCL project). See the links above for instructions.

## Follow Me
* Twitter: [@ActiveNick](http://twitter.com/ActiveNick)
* Blog: [AgeofMobility.com](http://AgeofMobility.com)
* SlideShare: [http://www.slideshare.net/ActiveNick](http://www.slideshare.net/ActiveNick)
