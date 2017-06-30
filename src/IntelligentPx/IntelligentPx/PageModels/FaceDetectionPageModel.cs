using FreshMvvm;
using IntelligentPx.Models;
using IntelligentPx.Services;
using Microsoft.ProjectOxford.Face.Contract;

namespace IntelligentPx.PageModels
{
    public class FaceDetectionPageModel : FreshBasePageModel
    {
        private readonly IFaceDetectionService _faceDetectionService;

        public FaceDetectionPageModel(IFaceDetectionService faceDetectionService)
        {
            _faceDetectionService = faceDetectionService;
        }

        public override void Init(object initData)
        {
            Photo = (Photo)initData;
            DetectFaces();
        }

        public Photo Photo { get; private set; }

        public Face[] Faces { get; private set; }

        private async void DetectFaces()
        {
            Faces = await _faceDetectionService.DetectFaces(Photo.Images.FullImage.HttpsUrl);
        }
    }
}