using System.Diagnostics;
using System.Runtime.InteropServices;

namespace summoner
{
    public partial class SummonerWindow : Form
    {
        [LibraryImport("user32.dll")]
        private static partial IntPtr GetForegroundWindow();

        [LibraryImport("user32.dll")]
        private static partial IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsZoomed(IntPtr hWnd);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("dwmapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out int pvAttribute, int cbAttribute);

        private const short SWP_NOSIZE = 1;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int DWMWA_CLOAKED = 14;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public SummonerWindow()
        {
            InitializeComponent();
            InitApp();
        }

        private void InitApp() => RefreshLists(this, EventArgs.Empty);

        private void MoveWindowToTopLeftOfScreen(IntPtr h)
        {
            Rectangle rect;
            int screenIndex;

            if (ScreensLB.SelectedItem == null)
            {
                MessageBox.Show("You must select a screen");
                return;
            }

            screenIndex = ScreensLB.SelectedIndex;
            rect = Screen.AllScreens[screenIndex].Bounds;
            if (h != IntPtr.Zero)
            {
                bool isMaximised = IsZoomed(h); // Is it maximised before moving displays?

                ShowWindow(h, SW_RESTORE);

                SetWindowPos(h, 0, rect.X, rect.Y, 0, 0, SWP_NOSIZE | SWP_SHOWWINDOW);

                if (!IsZoomed(h) && isMaximised) // If was maximised before reposition, and is no longer maximized due to restore, then re-maximize 
                {
                    ShowWindow(h, SW_SHOWMAXIMIZED);
                }
            }
        }

        private void GetWindowTitles()
        {
            string s;
            Process[] processCollection = Process.GetProcesses();
            WindowsLB.Items.Clear();
            foreach (Process p in processCollection)
            {
                if (IsOnTaskbar(p.MainWindowHandle) && !IsCloaked(p.MainWindowHandle))
                {
                    s = p.MainWindowTitle;
                    if (s != string.Empty)
                    {
                        WindowsLB.Items.Add(s);
                    }
                }
            }
        }

        private void GetScreens()
        {
            ScreensLB.Items.Clear();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                Screen currentScreen = Screen.AllScreens[i];
                string shortName = currentScreen.DeviceName.Replace(@"\\.\", "");
                ScreensLB.Items.Add($"{shortName} - ({currentScreen.Bounds.Width}x{currentScreen.Bounds.Height})");
            }
        }

        private void RefreshLists(object sender, EventArgs e)
        {
            GetWindowTitles();
            GetScreens();
        }

        private static bool IsCloaked(IntPtr hWnd)
        {
            int cloaked = 0;
            DwmGetWindowAttribute(hWnd, DWMWA_CLOAKED, out cloaked, sizeof(int));
            return cloaked != 0;
        }

        private static bool IsOnTaskbar(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            if (!IsWindowVisible(hWnd))
                return false;

            // Exclude tool windows (like floating tool palettes)
            IntPtr exStyle = GetWindowLongPtr(hWnd, GWL_EXSTYLE);
            if (((int)exStyle & WS_EX_TOOLWINDOW) != 0)
                return false;

            return true;
        }

        private void ShowWindowBtn_Click(object sender, EventArgs e)
        {
            string? appname;
            IntPtr hWnd;

            if (WindowsLB.SelectedItem == null)
            {
                MessageBox.Show("You must select a window name!!!?");
            }
            else
            {
                appname = WindowsLB.SelectedItem.ToString();
                hWnd = IntPtr.Zero;

                foreach (Process pList in Process.GetProcesses())
                {
                    if (!string.IsNullOrEmpty(appname) && pList.MainWindowTitle == appname)
                    {
                        hWnd = pList.MainWindowHandle;
                    }
                }

                if (hWnd != IntPtr.Zero)
                {
                    MoveWindowToTopLeftOfScreen(hWnd);
                }
            }
        }
    }
}