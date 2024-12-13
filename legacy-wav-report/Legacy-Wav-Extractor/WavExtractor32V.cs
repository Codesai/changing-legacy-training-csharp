using System;
using System.IO;
using System.Buffers.Binary;
using System.Collections.Generic;

namespace Legacy_Wav_Extractor;

public class WavExtractor32V
{
    public void Execute(List<string> filenames, List<string> optional)
    {
        List<string> data = new();
        data.Add("== Wave Extractor Report");
        foreach (var filename in filenames)
        {
            // Read-File //
            ReadOnlySpan<byte> bytes = File.ReadAllBytes(filename);

            if (bytes.Length < 44) throw new Exception("File is not supported");

            // File-Format //
            string fileFormat = $"{(char)bytes[0]}{(char)bytes[1]}{(char)bytes[2]}{(char)bytes[3]}";

            if (fileFormat != "RIFF") throw new Exception("File format is not in RIFF");

            // File-Type //
            string fileType = $"{(char)bytes[8]}{(char)bytes[9]}{(char)bytes[10]}{(char)bytes[11]}";

            if (fileType != "WAVE") throw new Exception("File type is not WAVE");

            // Format-Chunk  //
            string fmtChunck = $"{(char)bytes[12]}{(char)bytes[13]}{(char)bytes[14]}{(char)bytes[15]}";

            if (fmtChunck != "fmt ") throw new Exception("Format chunk missing");

            // Format-Chunk-Length //
            int fmtLenghth = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(16, 20));

            if (fmtLenghth != 16) throw new Exception("Format chunck lenghth not PCM"); // Should be 16 for PCM

            // Audio-Format //
            int audioFormat = BinaryPrimitives.ReadInt16LittleEndian(bytes.Slice(20, 22));

            if (audioFormat != 1) throw new Exception("Audio format not PCM"); // Should be 1 for PCM Linear quantization
            
            var name = Path.GetFileName(filename);
            var channels = BinaryPrimitives.ReadInt16LittleEndian(bytes.Slice(22, 24));
            var sampleRate = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(24, 28));
            var byteRate = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(28, 32));
            data.Add(
                $"Name:{name}, Channels:{channels}, SampleRate:{sampleRate}, ByteRate:{byteRate}");
        }
        data.Add($"== Total files: {filenames.Count}");

        File.WriteAllText("../../../wav-extractor-data.txt", string.Join(Environment.NewLine, data));
    }
}