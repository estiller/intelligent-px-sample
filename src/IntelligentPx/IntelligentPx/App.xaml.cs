using System.Reflection;
using FreshMvvm;
using IntelligentPx.PageModels;
using IntelligentPx.Services;
using PCLAppConfig;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace IntelligentPx
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            InitializeConfigurationManager();

            RegisterDependencies();

            var page = FreshPageModelResolver.ResolvePageModel<PhotosPageModel>();
            MainPage = new FreshNavigationContainer(page);
        }

        private void InitializeConfigurationManager()
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            ConfigurationManager.Initialise(assembly.GetManifestResourceStream("IntelligentPx.Secrets.config"));
        }

        private static void RegisterDependencies()
        {
            FreshIOC.Container.Register<IPhotoService, PhotoService>();
            //FreshIOC.Container.Register<IPhotoService, Services.Mocks.MockPhotoService>();

            FreshIOC.Container.Register<IComputerVisionService, ComputerVisionService>();
            FreshIOC.Container.Register<ICustomVisionServiceTraining, CustomVisionServiceTraining>();
            FreshIOC.Container.Register<ICustomVisionServicePrediction, CustomVisionServicePrediction>();
            FreshIOC.Container.Register<IEmotionService, EmotionService>();
            FreshIOC.Container.Register<IFaceDetectionService, FaceDetectionService>();
        }
    }
}
