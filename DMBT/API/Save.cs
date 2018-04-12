using DMBT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace API
{
    public class Save
    {
        public static void SaveFont(string font)
        {
            File.WriteAllText("Font.ini", font);
        }

        public static void ReadFont(Window m)
        {
            if (File.Exists("Font.ini"))
            {
                string fonttext = File.ReadAllText("Font.ini");
                MainWindow.main.FontFamily = m.FindResource(fonttext) as FontFamily;
                MainWindow.main.ApplyTemplate();
            }
        }
    }
}
