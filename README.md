# IntelligentPX Sample Application
This sample application demonstrates the usage of [Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/), and is a companion to my session "Why Don't You Understand Me? Build Intelligence into Your Apps".

## Prerequisites
### Environment
The sample has been built and tested with Visual Studio 2017 Update 2 and above, and requires the "Mobile Development with .NET" (a.k.a. Xamarin) and "Azure" workloads to be installed. The requirements change according to the desired platform:
* For Android you will need an [Android Emulator](https://www.visualstudio.com/vs/msft-android-emulator/) or an Android device.
* For iOS you will need a connected Mac with an iPhone simulator or connected iPhone.
* For UWP you will need a Windows 10 machine.

### API Keys
In addition, you will need to obtain the following API keys:
* An API key for the [500px API](https://github.com/500px/api-documentation). Registering an application and obtaining an API key can be done via [this page](http://500px.com/settings/applications).
* An API key for each of the [Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/). In order to obtain this key, login to the [Azure Portal](https://portal.azure.com), create a new Cognitive Services resource of the required API type and copy the API key from the portal.
    * [Computer Vision API](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)
    * [Face API](https://azure.microsoft.com/en-us/services/cognitive-services/face/)
    * [Emotion API](https://azure.microsoft.com/en-us/services/cognitive-services/emotion/)
    * [Text Analysis API](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/)
    * [Translator Text API](https://azure.microsoft.com/en-us/services/cognitive-services/translator-text-api/)
    * [Bing Autosuggest API](https://azure.microsoft.com/en-us/services/cognitive-services/autosuggest/)
    * [Bing Speech APIs](https://azure.microsoft.com/en-us/services/cognitive-services/speech/)
    
    All these resources can be automatically deployed to a resource group in your subscription by using the deployment template link below.

    [![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Festiller%2Fintelligent-px-sample%2Fmaster%2Fsrc%2FIntelligentPxResourceGroup%2Fazuredeploy.json)

* A training key and prediction key for an [Azure Custom Vision](https://customvision.ai) service.

Once you have obtained your keys, make a copy of the file *src/IntelligentPx/IntelligentPx/Secrets.default.config* and rename it to *src/IntelligentPx/IntelligentPx/Secrets.config*. Then, enter your provided API keys there. At this stage you should be able to compile and run the sample by opening the Solution file *src/IntelligentPx.sln*.