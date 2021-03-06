﻿using System;
using System.Runtime.InteropServices;
[assembly: System.Reflection.AssemblyTitle("Sample")]
[assembly: System.Reflection.AssemblyProduct("Sample")]
[assembly: System.Reflection.AssemblyVersion("1.0.0.0")]
[assembly: System.Reflection.AssemblyFileVersion("1.0.0.0")]
[assembly: ComVisible(false)]

static class Sample
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] delegate int AddDelegate(int a, int b);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] delegate void CallbackDelegate(int number);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] delegate void CallCallbackDelegate(CallbackDelegate callback, int number);

    static int Main(string[] args)
    {
        Console.WriteLine("Process is " + (DLLFromMemory.Is64BitProcess ? "64" : "32") + "bit\n");

        DLLFromMemory dll = new DLLFromMemory(GetSampleDLLBytes());

        AddDelegate addFunc = dll.GetDelegateFromFuncName<AddDelegate>("Add");
        Console.WriteLine("Calling add(1, 2): " + addFunc(1, 2) + "\n");

        CallCallbackDelegate callCallbackFunc = dll.GetDelegateFromFuncName<CallCallbackDelegate>("CallCallback");
        Console.WriteLine("Calling callCallback(TestCallback, 777)...");
        callCallbackFunc(TestCallback, 777);
        Console.WriteLine("Done!\n");

        dll.Close();
        return 0;
    }

    static void TestCallback(int number)
    {
        Console.WriteLine("    In callback with number: " + number);
    }

    static byte[] GetSampleDLLBytes()
    {
        byte[] buf;
        System.IO.Compression.DeflateStream ds;
        if (DLLFromMemory.Is64BitProcess)
        {
            buf = new byte[2048]; //decompress the 409 bytes below into this 2048 byte buffer (original dll file size)
            ds = new System.IO.Compression.DeflateStream(new System.IO.MemoryStream(new byte[409] {
                    0xE5, 0x94, 0x33, 0xBC, 0xDE, 0x50, 0x1C, 0x40, 0x4F, 0xF0, 0xAC, 0x6A, 0xEF, 0xBF, 0x9C, 0x6A, 0xB7, 0x4B, 0x6D, 0x5B, 0x4F, 0xC1, 0x7D, 0xFC, 0x14, 0xD4, 0x5C, 0x6A, 0xAE, 0xC5, 0xBE, 0xD6,
                    0x9C, 0x6A, 0x77, 0x9F, 0x8A, 0x7D, 0x29, 0xB7, 0xF2, 0x73, 0xED, 0x6E, 0x39, 0xBF, 0x9C, 0x5C, 0xE6, 0x3A, 0x77, 0xEE, 0xAA, 0xC3, 0x18, 0x80, 0x09, 0x7C, 0xF8, 0x00, 0x17, 0xC9, 0x31, 0x81,
                    0x5F, 0x73, 0x07, 0xA8, 0xED, 0x79, 0xB9, 0x96, 0xB3, 0x15, 0x0F, 0x7B, 0x5D, 0xD4, 0xE6, 0x3C, 0xEC, 0xB5, 0xA4, 0xAD, 0x3D, 0x90, 0x94, 0x9F, 0x6C, 0xF5, 0xAD, 0xB8, 0x38, 0x56, 0x22, 0x91,
                    0x0C, 0xC5, 0x56, 0xE2, 0xAF, 0x49, 0x48, 0x7B, 0x42, 0xA6, 0xCC, 0x5F, 0x2C, 0xF1, 0xA4, 0xAB, 0x06, 0xD5, 0xD4, 0x54, 0xF6, 0x25, 0xC7, 0xEC, 0xD8, 0xFB, 0xC7, 0x75, 0x35, 0x87, 0x0F, 0x14,
                    0xF4, 0xC3, 0x35, 0x07, 0x6A, 0xB3, 0xE1, 0x9C, 0x7C, 0x38, 0x2F, 0x1B, 0x2E, 0x6A, 0x77, 0xDA, 0x32, 0xE5, 0xFC, 0x80, 0x05, 0x53, 0xC1, 0xDD, 0xA9, 0xD3, 0xE5, 0xF5, 0xB4, 0x06, 0xF2, 0x3C,
                    0xA7, 0xB7, 0x54, 0xE9, 0xD5, 0xA0, 0x93, 0x13, 0x90, 0x2E, 0x40, 0x5A, 0x60, 0xBB, 0x46, 0x3E, 0xAE, 0x43, 0x49, 0xAE, 0x42, 0x31, 0x64, 0x08, 0x60, 0x02, 0x80, 0x41, 0xB3, 0x96, 0xAB, 0x48,
                    0x31, 0xF8, 0x36, 0x9D, 0x8B, 0x4E, 0x10, 0x58, 0xC9, 0x1F, 0x20, 0x30, 0x86, 0xFF, 0xC7, 0xA0, 0x50, 0xAD, 0x0F, 0x81, 0xFE, 0xC5, 0xB9, 0x01, 0xE6, 0x37, 0x5D, 0x36, 0x0F, 0xF2, 0x5D, 0x2B,
                    0xB4, 0xA0, 0x8B, 0x06, 0x48, 0xBE, 0x5E, 0x29, 0x5F, 0x30, 0x21, 0xFB, 0x44, 0x80, 0x08, 0x73, 0xC0, 0xEC, 0x7A, 0xFD, 0xC1, 0x67, 0xCC, 0xD8, 0x77, 0x75, 0xDF, 0xBD, 0x19, 0x1F, 0x9E, 0x16,
                    0xD2, 0x17, 0x35, 0xE0, 0x3A, 0x11, 0x20, 0xB2, 0x14, 0xEF, 0x4C, 0x1D, 0x68, 0x04, 0x8E, 0x4B, 0xDA, 0xD2, 0x2F, 0xCB, 0xAA, 0x81, 0x1E, 0xC0, 0x5B, 0x49, 0x5B, 0xFA, 0xFD, 0xEF, 0x37, 0x0B,
                    0x68, 0x80, 0x9E, 0xB7, 0x4D, 0x20, 0x25, 0xB0, 0x5E, 0x80, 0x2E, 0xD0, 0x25, 0xED, 0x6E, 0x81, 0xFD, 0x02, 0xA0, 0xB1, 0x44, 0x05, 0xE1, 0x94, 0x39, 0x73, 0x06, 0xB9, 0xB1, 0x18, 0x13, 0x5D,
                    0x97, 0xC9, 0x56, 0x2C, 0x96, 0xD1, 0xB6, 0x9C, 0x4E, 0x80, 0x45, 0x8B, 0xA7, 0x2C, 0x36, 0xFC, 0x77, 0x6B, 0x67, 0x4F, 0xDD, 0x3E, 0xF5, 0xA8, 0xB9, 0xBB, 0x2A, 0x3C, 0x75, 0x72, 0x8F, 0x06,
                    0x4C, 0x19, 0x57, 0xEF, 0xAA, 0xB5, 0xF5, 0x73, 0x37, 0x2C, 0xF0, 0x93, 0x1D, 0xCA, 0x09, 0x83, 0xFA, 0x74, 0x2B, 0xD3, 0xFC, 0x64, 0x7C, 0xAE, 0x8A, 0x27, 0xFD, 0x0D, 0x03, 0x13, 0x2A, 0xAC,
                    0xCF, 0xB7, 0x5D, 0xBF, 0x48, 0xC5, 0x94, 0x15, 0xA8, 0xFA, 0x51, 0x23, 0xEC, 0xF6, 0x62, 0xEE, 0xA0, 0x94, 0x6B, 0x93, 0x43, 0x23, 0xDA, 0x7C, 0x04
                }, false), System.IO.Compression.CompressionMode.Decompress, false);
        }
        else
        {
            buf = new byte[2048]; //decompress the 417 bytes below into this 2048 byte buffer (original dll file size)
            ds = new System.IO.Compression.DeflateStream(new System.IO.MemoryStream(new byte[417] {
                    0xED, 0xD4, 0x03, 0x8C, 0x1C, 0x01, 0x14, 0x80, 0xE1, 0x7F, 0x70, 0x36, 0x62, 0xF4, 0x5D, 0x52, 0xDB, 0x0A, 0x6A, 0xDB, 0x3A, 0x8D, 0x8E, 0xAB, 0xCC, 0xD6, 0x6E, 0x50, 0x2B, 0xD6, 0xC5, 0x4E,
                    0x6A, 0xDB, 0x46, 0x9C, 0x5A, 0xB1, 0x59, 0x1B, 0x83, 0xDA, 0x6D, 0x50, 0x7C, 0x0F, 0x63, 0x63, 0xC4, 0xD4, 0xF5, 0x68, 0x80, 0x0E, 0xBC, 0x7C, 0x09, 0x7B, 0xF0, 0xF5, 0xE6, 0xDB, 0xCE, 0x00,
                    0xB9, 0x8D, 0xF6, 0xE5, 0xB2, 0x23, 0xE3, 0x62, 0xC9, 0x1E, 0x65, 0xF8, 0xC5, 0x92, 0xF1, 0x35, 0xB5, 0x49, 0x49, 0xB8, 0xF1, 0x6A, 0xD7, 0x88, 0x8A, 0x65, 0xC4, 0x62, 0xF1, 0xE9, 0x62, 0x3A,
                    0xE2, 0xCE, 0x88, 0x49, 0x6D, 0x4C, 0xFA, 0x8F, 0x1A, 0x27, 0xD1, 0xB8, 0xED, 0xB4, 0xCD, 0xC9, 0xC9, 0x6C, 0x8C, 0x6F, 0x58, 0xE4, 0xC5, 0xAD, 0xBC, 0x9C, 0xF5, 0x6B, 0xC2, 0x72, 0xA7, 0xCF,
                    0x58, 0x93, 0xEB, 0x0D, 0x87, 0x07, 0xC3, 0x91, 0xDE, 0x70, 0x6C, 0xAD, 0x55, 0xF3, 0x66, 0x39, 0x5F, 0x30, 0x7A, 0x00, 0x0C, 0x57, 0x54, 0xF2, 0xEF, 0x0D, 0x2C, 0x23, 0x70, 0x07, 0xB5, 0x24,
                    0x4B, 0xC9, 0x06, 0x15, 0xBF, 0x00, 0xC9, 0x07, 0xDE, 0x94, 0x80, 0x37, 0x96, 0xEF, 0x2F, 0x4B, 0x41, 0x01, 0xDE, 0x0D, 0x69, 0x0F, 0xE8, 0x78, 0x34, 0x7A, 0x2B, 0x90, 0xEF, 0xE5, 0xBB, 0xA1,
                    0x3F, 0xE8, 0x2D, 0x30, 0x85, 0x1F, 0x20, 0xD0, 0x9D, 0xDF, 0xA7, 0xED, 0x74, 0x67, 0xF6, 0x74, 0xA0, 0x39, 0x10, 0x5E, 0x0B, 0x3A, 0x1F, 0x10, 0xA8, 0x6C, 0xEB, 0xDA, 0xC6, 0x74, 0x03, 0xF2,
                    0x15, 0x40, 0x82, 0xF5, 0x52, 0xF9, 0x40, 0x6F, 0x2F, 0xFF, 0xFB, 0x97, 0xAC, 0xEA, 0xDF, 0x58, 0xD7, 0xFA, 0x37, 0x4E, 0x3F, 0x7E, 0xC1, 0xF7, 0x72, 0x7A, 0xE3, 0xF4, 0x97, 0xE3, 0x1B, 0xA7,
                    0x4F, 0x09, 0x66, 0xEC, 0x51, 0x80, 0x63, 0xD9, 0xFC, 0xF7, 0xF7, 0x0A, 0xFF, 0x99, 0x2A, 0x50, 0x0E, 0x34, 0x08, 0x34, 0xA4, 0x7E, 0xB8, 0x2C, 0x1B, 0x28, 0x06, 0x9E, 0x09, 0x3C, 0x4B, 0xFD,
                    0xFC, 0xF6, 0xF3, 0x05, 0x14, 0x40, 0x0D, 0xAA, 0x46, 0x20, 0x21, 0x30, 0x5B, 0x82, 0xDF, 0x66, 0x3E, 0x2C, 0x17, 0x58, 0x2D, 0x00, 0x0A, 0xE3, 0x9D, 0xE4, 0xF4, 0xFE, 0xC3, 0x87, 0xB7, 0xB5,
                    0x23, 0x11, 0xFA, 0xD8, 0x36, 0xFD, 0x8C, 0x48, 0xE4, 0x4D, 0x99, 0x86, 0x55, 0x0F, 0x30, 0x76, 0x5C, 0xFF, 0x71, 0x37, 0xEE, 0x2F, 0xD9, 0x3A, 0xB7, 0xA0, 0x45, 0xBF, 0x65, 0x1B, 0x9F, 0x1F,
                    0x6A, 0x77, 0xF9, 0x49, 0xA5, 0x02, 0xF4, 0xEF, 0x59, 0x6A, 0x3B, 0x33, 0x4B, 0x47, 0xCC, 0x19, 0xED, 0xC6, 0xEB, 0x1C, 0x6B, 0x7A, 0xB2, 0xF4, 0xF5, 0x5E, 0x06, 0xBA, 0xF1, 0xE8, 0x08, 0x27,
                    0x1A, 0x77, 0xE7, 0xB4, 0x89, 0x39, 0xD3, 0x4B, 0x83, 0x7D, 0x97, 0x8E, 0x75, 0x22, 0x8E, 0x91, 0x74, 0x4A, 0x3B, 0x75, 0x34, 0x6B, 0xDF, 0xCE, 0x6D, 0x9B, 0xB0, 0x4D, 0x7C, 0x0A, 0xFF, 0xB6, 0x57
                }, false), System.IO.Compression.CompressionMode.Decompress, false);
        }
        ds.Read(buf, 0, buf.Length);
        ds.Dispose();
        return buf;
    }
}
