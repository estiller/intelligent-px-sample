using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Media.Render;
using Windows.Storage;
using IntelligentPx.Services;

namespace IntelligentPx.UWP.Services
{
    internal class WavRecorder : IWavRecorder
    {
        private string _filePath;
        private AudioGraph _graph;
        private AudioFileOutputNode _outputNode;

        public async Task StartRecordAsync()
        {
            _filePath = Path.GetTempFileName();
            var file = await StorageFile.GetFileFromPathAsync(_filePath);

            var result = await AudioGraph.CreateAsync(new AudioGraphSettings(AudioRenderCategory.Speech));
            if (result.Status != AudioGraphCreationStatus.Success)
            {
                throw new Exception("Couldn't open recorder!");
            }
            _graph = result.Graph;

            var microphone = await DeviceInformation.CreateFromIdAsync(MediaDevice.GetDefaultAudioCaptureId(AudioDeviceRole.Default));
            var outProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.Low);
            outProfile.Audio = AudioEncodingProperties.CreatePcm(16000, 1, 16);

            var outputResult = await _graph.CreateFileOutputNodeAsync(file, outProfile);
            if (outputResult.Status != AudioFileNodeCreationStatus.Success)
            {
                throw new Exception("Couldn't create output!");
            }

            _outputNode = outputResult.FileOutputNode;
            var inProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);
            var inputResult = await _graph.CreateDeviceInputNodeAsync(
                MediaCategory.Speech,
                inProfile.Audio,
                microphone);

            if (inputResult.Status != AudioDeviceNodeCreationStatus.Success)
            {
                throw new Exception("Couldn't create device node!");
            }

            inputResult.DeviceInputNode.AddOutgoingConnection(_outputNode);
            _graph.Start();
        }

        public async Task<string> EndRecordAsync()
        {
            _graph.Stop();
            await _outputNode.FinalizeAsync();
            _outputNode = null;
            _graph.Dispose();
            _graph = null;

            return _filePath;
        }
    }
}