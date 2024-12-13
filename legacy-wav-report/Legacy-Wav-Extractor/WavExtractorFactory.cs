namespace Legacy_Wav_Extractor;

public static class WavExtractorFactory
{
    public static WavExtractor32V Get()
    {
        return new WavExtractor32V();
    }
}