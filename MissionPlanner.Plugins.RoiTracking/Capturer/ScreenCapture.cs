namespace MissionPlanner.Plugins.RoiTracking.Capturer
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class ScreenCapture
    {
        /// <summary>
        /// Captures the screenshot of defined process window.
        /// </summary>
        /// <param name="procName">Process name which window should be captured. For example 'gst-launch-1.0'</param>
        /// <param name="destJpgFile">Destination path and file name where screenshot should be stored.</param>
        /// <returns>Returns true, if screen is successfully captured. Otherwise returns false.</returns>
        public static bool Capture(string procName, string destJpgFile)
        {
            var procList = Process.GetProcessesByName(procName);
            if (procList.Length == 0)
                return false;

            var screenShot = PrintWindow(procList[0].MainWindowHandle);
            screenShot.Save(destJpgFile, ImageFormat.Jpeg);

            return true;
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            RECT rc;
            GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            PrintWindow(hwnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }
    }
}
