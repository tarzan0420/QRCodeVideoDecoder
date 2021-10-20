
using ComponentAce.Compression.Libs.zlib;
using System;
using System.IO;
using System.IO.Compression;

namespace QRCodeEncoderLibrary
{
	internal static class ZLibCompression
	{
		internal static void CopyStream(System.IO.Stream input, System.IO.Stream output)
		{
			byte[] buffer = new byte[2000];
			int len;
			while ((len = input.Read(buffer, 0, 2000)) > 0)
			{
				output.Write(buffer, 0, len);
			}
			output.Flush();
		}

		internal static void compressFile(string inFile, string outFile)
		{
			FileStream outFileStream = new FileStream(outFile, FileMode.Create);
			ZOutputStream outZStream = new ZOutputStream(outFileStream, zlibConst.Z_DEFAULT_COMPRESSION);
			System.IO.FileStream inFileStream = new System.IO.FileStream(inFile, System.IO.FileMode.Open);
			try
			{
				CopyStream(inFileStream, outZStream);
			}
			finally
			{
				outZStream.Close();
				outFileStream.Close();
				inFileStream.Close();
			}
		}

		internal static void decompressFile(string inFile, string outFile)
		{
			FileStream outFileStream = new FileStream(outFile, FileMode.Create);
			ZOutputStream outZStream = new ZOutputStream(outFileStream);
			FileStream inFileStream = new FileStream(inFile, FileMode.Open);
			try
			{
				CopyStream(inFileStream, outZStream);
			}
			finally
			{
				outZStream.Close();
				outFileStream.Close();
				inFileStream.Close();
			}
		}


	}
}
