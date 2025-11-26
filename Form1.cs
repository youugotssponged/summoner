using System.Diagnostics;
using System.Runtime.InteropServices;

namespace summoner
{
    public partial class Form1 : Form
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
        private static partial bool IsIconic(IntPtr hWnd);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsZoomed(IntPtr hWnd);

        private const short SWP_NOSIZE = 1;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWMAXIMIZED = 3;

        public Form1()
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
                if (IsIconic(h))
                {
                    ShowWindow(h, SW_RESTORE);
                }

                SetWindowPos(h, 0, rect.X, rect.Y, 0, 0, SWP_NOSIZE | SWP_SHOWWINDOW);

                if (IsZoomed(h))
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
                s = p.MainWindowTitle;
                if (s != string.Empty)
                {
                    WindowsLB.Items.Add(s);
                }
            }
        }

        private void GetScreens()
        {
            ScreensLB.Items.Clear();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                Screen currentScreen = Screen.AllScreens[i];
                ScreensLB.Items.Add($"{currentScreen.DeviceName} - ({currentScreen.Bounds.Width}x{currentScreen.Bounds.Height})");
            }
        }

        private void RefreshLists(object sender, EventArgs e)
        {
            GetWindowTitles();
            GetScreens();
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

                foreach(Process pList in Process.GetProcesses())
                {
                    if(!string.IsNullOrEmpty(appname) && pList.MainWindowTitle == appname)
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