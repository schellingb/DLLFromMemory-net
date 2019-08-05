# DLLFromMemory.Net
A C# library to load a native DLL from memory without the need to allow unsafe code.

By default C# can load external libraries only via files on the filesystem.
A common workaround for this problem is to write the DLL into a temporary file first and import it from there.
This library can be used to load a DLL completely from memory - without storing on the disk first.

It supports both 32bit and 64bit processes/DLLs, as well as AnyCPU builds.  
For AnyCPU, both 32bit and 64bit DLLs must be available in memory (see [Sample](Sample.cs))

## Example
```C#
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] delegate int AddDelegate(int a, int b);

static void RunAdd(byte[] dllBytes)
{
    DLLFromMemory dll = new DLLFromMemory(dllBytes);

    AddDelegate addFunc = dll.GetDelegateFromFuncName<AddDelegate>("Add");
    Console.WriteLine("Calling add(1, 2): " + addFunc(1, 2) + "\n");

    dll.Close();
}
```

## Contributions
DLLFromMemory.Net is based on Memory Module.net 0.2  
Copyright (C) 2012 - 2018 by Andreas Kanzler  
https://github.com/Scavanger/MemoryModule.net

Memory Module.net is based on Memory DLL loading code Version 0.0.4  
Copyright (C) 2004 - 2015 by Joachim Bauch  
https://github.com/fancycode/MemoryModule

## License
DLLFromMemory.Net is available under the [MPL 2.0](https://www.mozilla.org/en-US/MPL/2.0/).
