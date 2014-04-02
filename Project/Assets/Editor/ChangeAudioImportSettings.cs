using UnityEngine;
using UnityEditor;

// /////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Batch audio import settings modifier.
//
// Modifies all selected audio clips in the project window and applies the requested modification on the
// audio clips. Idea was to have the same choices for multiple files as you would have if you open the
// import settings of a single audio clip. Put this into Assets/Editor and once compiled by Unity you find
// the new functionality in Custom -> Sound. Enjoy! :-)
//
// April 2010. Based on Martin Schultz's texture import settings batch modifier.
//
// /////////////////////////////////////////////////////////////////////////////////////////////////////////

public class ChangeAudioImportSettings : ScriptableObject {

//Audio Format	The specific format that will be used for the sound at runtime.
//Native	This option offers higher quality at the expense of larger file size and is best for very short sound effects.
//Compressed	The compression results in smaller files but with somewhat lower quality compared to native audio. This format is best for medium length sound effects and music.
//3D Sound	If enabled, the sound will play back in 3D space. Both Mono and Stereo sounds can be played in 3D.
//Force to mono	If enabled, the audio clip will be down-mixed to a single channel sound.
//Load Type	The method Unity uses to load audio assets at runtime.
//Decompress on load	Audio files will be decompressed as soon as they are loaded. Use this option for smaller compressed sounds to avoid the performance overhead of decompressing on the fly. Be aware that decompressing sounds on load will use about ten times more memory than keeping them compressed, so don't use this option for large files.
//Compressed in memory	Keep sounds compressed in memory and decompress while playing. This option has a slight performance overhead (especially for Ogg/Vorbis compressed files) so only use it for bigger files where decompression on load would use a prohibitive amount of memory. Note that, due to technical limitations, this option will silently switch to Stream From Disc (see below) for Ogg Vorbis assets on platforms that use FMOD audio.
//Stream from disc	Stream audio data directly from disc. The memory used by this option is typically a small fraction of the file size, so it is very useful for music or other very long tracks. For performance reasons, it is usually advisable to stream only one or two files from disc at a time but the number of streams that can comfortably be handled depends on the hardware.
//Compression	Amount of Compression to be applied to a Compressed clip. Statistics about the file size can be seen under the slider. A good approach to tuning this value is to drag the slider to a place that leaves the playback "good enough" while keeping the file small enough for your distribution requirements.
//Hardware Decoding	(iOS only) On iOS devices, Apple's hardware decoder can be used resulting in lower CPU overhead during decompression. Check out platform specific details for more info.
//Gapless looping	(Android/iOS only) Use this when compressing a seamless looping audio source file (in a non-compressed PCM format) to ensure perfect continuity is preserved at the seam. Standard MPEG encoders introduce a short silence at the loop point, which will be audible as a brief "click" or "pop".
	
	
    [MenuItem ("Tools/Sound/EFX")]
    static void ToggleCompression_Disable() {
        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.format = AudioImporterFormat.Compressed; // uncompressed
            audioImporter.compressionBitrate = 96000;//Set audio compression bitrate (kbps)/32"
			audioImporter.loadType = AudioImporterLoadType.DecompressOnLoad; //Tools/Sound/Toggle decompress on load/Compressed in memory"
			audioImporter.threeD = false;//3D
            audioImporter.forceToMono = true;//Mono
            audioImporter.hardware = true;//Hardware 
            AssetDatabase.ImportAsset(path);
        }
    }


    [MenuItem ("Tools/Sound/BG")]
    static void ToggleCompression_Enable() {
        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.format = AudioImporterFormat.Compressed; // uncompressed
            audioImporter.compressionBitrate = 96000;//Set audio compression bitrate (kbps)/32"
			audioImporter.loadType = AudioImporterLoadType.StreamFromDisc; //Tools/Sound/Toggle decompress on load/Compressed in memory"
			audioImporter.threeD = false;//3D
            audioImporter.forceToMono = false;//Mono
            audioImporter.hardware = true;//Hardware 
            AssetDatabase.ImportAsset(path);
        }
	}

    // ----------------------------------------------------------------------------
    //[MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/32")]
    static void SetCompressionBitrate_32kbps() {
        SelectedSetCompressionBitrate(32000);
    }


    //[MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/64")]
    static void SetCompressionBitrate_64kbps() {
        SelectedSetCompressionBitrate(64000);
    }

//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/96")]
    static void SetCompressionBitrate_96kbps() {
        SelectedSetCompressionBitrate(96000);
    }


//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/128")]
    static void SetCompressionBitrate_128kbps() {
        SelectedSetCompressionBitrate(128000);
    }


//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/144")]
    static void SetCompressionBitrate_144kbps() {
        SelectedSetCompressionBitrate(144000);
    }


//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/156 (default)")]
    static void SetCompressionBitrate_156kbps() {
        SelectedSetCompressionBitrate(156000);
    }


//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/160")]
    static void SetCompressionBitrate_160kbps() {
        SelectedSetCompressionBitrate(160000);
    }


//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/192")]
    static void SetCompressionBitrate_192kbps() {
        SelectedSetCompressionBitrate(192000);
    }


//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/224")]
    static void SetCompressionBitrate_224kbps() {
        SelectedSetCompressionBitrate(224000);
    }


//    [MenuItem ("Tools/Sound/Set audio compression bitrate (kbps)/240")]
    static void SetCompressionBitrate_240kbps() {
        SelectedSetCompressionBitrate(240000);
    }


    // ----------------------------------------------------------------------------

//    [MenuItem ("Tools/Sound/Toggle decompress on load/Compressed in memory")]
    static void ToggleDecompressOnLoad_CompressedInMemory() {
        SelectedToggleDecompressOnLoadSettings(AudioImporterLoadType.CompressedInMemory);
    }

//    [MenuItem("Tools/Sound/Toggle decompress on load/Decompress on load")]
    static void ToggleDecompressOnLoad_DecompressOnLoad() {
        SelectedToggleDecompressOnLoadSettings(AudioImporterLoadType.DecompressOnLoad);
    }

//    [MenuItem("Tools/Sound/Toggle decompress on load/Stream from disc")]
    static void ToggleDecompressOnLoad_StreamFromDisc() {
        SelectedToggleDecompressOnLoadSettings(AudioImporterLoadType.StreamFromDisc);
    }


    // ----------------------------------------------------------------------------

//    [MenuItem ("Tools/Sound/Toggle 3D sound/Disable")]
    static void Toggle3DSound_Disable() {
        SelectedToggle3DSoundSettings(false);
    }


//    [MenuItem ("Tools/Sound/Toggle 3D sound/Enable")]
    static void Toggle3DSound_Enable() {
        SelectedToggle3DSoundSettings(true);
    }


    // ----------------------------------------------------------------------------

//    [MenuItem ("Tools/Sound/Toggle mono/Auto")]
    static void ToggleForceToMono_Auto() {
        SelectedToggleForceToMonoSettings(false);
    }


//    [MenuItem ("Tools/Sound/Toggle mono/Forced")]
    static void ToggleForceToMono_Forced() {
        SelectedToggleForceToMonoSettings(true);
    }


    // ----------------------------------------------------------------------------

//    [MenuItem ("Tools/Sound/Toggle HW Decompression/Enable")]
    static void ToggleForceTo_HW_CompressionEnalbe() {
        SelectedToggleHardwareDecompress(true);
    }

//    [MenuItem ("Tools/Sound/Toggle HW Decompression/Disable")]
    static void ToggleForceTo_HW_CompressionDisable() {
        SelectedToggleHardwareDecompress(false);
    }



    // ----------------------------------------------------------------------------


    static void SelectedToggleCompressionSettings(AudioImporterFormat newFormat) {

        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.format = newFormat;
            AssetDatabase.ImportAsset(path);
        }
    }


    static void SelectedSetCompressionBitrate(int newCompressionBitrate) {


        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.compressionBitrate = newCompressionBitrate;
            AssetDatabase.ImportAsset(path);
        }
    }


    static void SelectedToggleDecompressOnLoadSettings(AudioImporterLoadType loadType) {

        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.loadType = loadType;
            AssetDatabase.ImportAsset(path);
        }
    }

    static void SelectedToggle3DSoundSettings(bool enabled) {

        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.threeD = enabled;
            AssetDatabase.ImportAsset(path);
        }
    }


    static void SelectedToggleForceToMonoSettings(bool enabled) {

        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.forceToMono = enabled;
            AssetDatabase.ImportAsset(path);
        }
    }

    static void SelectedToggleHardwareDecompress(bool enabled) {

        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips) {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.hardware = enabled;
            AssetDatabase.ImportAsset(path);
        }
    }


 /*
      * // C#: Use hardware voice/decoder if available when importing Audio.
    class UseHardwareDecoder extends AssetPostprocessor {
        function OnPreprocessAudio () {
            var audioImporter : AudioImporter = assetImporter;
            audioImporter.hardware = true;
        }
     */

    static Object[] GetSelectedAudioclips()
    {
        return Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
    }
}