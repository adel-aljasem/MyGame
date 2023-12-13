//using AdilGame.Network.Data;
//using MessagePack;
//using MessagePack.Resolvers;
//using System;
//using System.IO;
//using System.IO.Compression;
//using System.Text;
//using ZstdNet;

//public class Compressing
//{
//    public Compressing()
//    {
   

//    }

//    public static byte[] CompressString(string str)
//    {
//        byte[] bytes = Encoding.UTF8.GetBytes(str);
//        using (var compressor = new Compressor(new CompressionOptions(CompressionOptions.MinCompressionLevel)))
//        {
//            return compressor.Wrap(bytes);
//        }
//    }
//    public static byte[] CompressFloat(float value)
//    {
//        byte[] bytes = BitConverter.GetBytes(value);
//        using (var compressor = new Compressor(new CompressionOptions(CompressionOptions.MinCompressionLevel)))
//        {
//            return compressor.Wrap(bytes);
//        }
//    }

//    public static byte[] CompressPlayer(Player player)
//    {
//        byte[] messagePackBytes = MessagePackSerializer.Serialize(player);
//        using (var compressor = new Compressor(new CompressionOptions(CompressionOptions.MinCompressionLevel)))
//        {
//            return compressor.Wrap(messagePackBytes);
//        }
//    }

//    public static Player DecompressPlayer(byte[] compressedBytes)
//    {
//        using (var decompressor = new Decompressor())
//        {
//            byte[] decompressedBytes = decompressor.Unwrap(compressedBytes);
//            return MessagePackSerializer.Deserialize<Player>(decompressedBytes);
//        }
//    }


//    public static float DecompressFloat(byte[] compressedBytes)
//    {
//        using (var decompressor = new Decompressor())
//        {
//            byte[] decompressedBytes = decompressor.Unwrap(compressedBytes);
//            return BitConverter.ToSingle(decompressedBytes, 0);
//        }
//    }

//    public static string DecompressString(byte[] compressed)
//    {
//        using (var decompressor = new Decompressor())
//        {
//            byte[] decompressedBytes = decompressor.Unwrap(compressed);
//            return Encoding.UTF8.GetString(decompressedBytes);
//        }
//    }




//}

