extern "C" __declspec(dllexport) int __cdecl Add(int a, int b)
{
	return a + b;
}

extern "C" __declspec(dllexport) void __cdecl CallCallback(void (__cdecl callback)(int number), int number)
{
	callback(number);
}

extern "C" long __stdcall DllMain(void* hDllHandle, long dwReason, void* lpreserved)
{
	return 1;
}
