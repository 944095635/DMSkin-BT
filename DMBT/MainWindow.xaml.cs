using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMBT
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main;

        public MainWindow()
        {
            InitializeComponent();
            main = this;
            API.Save.ReadFont(this);
            labVer.Text = "版本号:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void DMTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void PART_Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }



        private void PART_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOpenQQ_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://sighttp.qq.com/authd?IDKEY=41449ed2961c9b9e41b84db0dce6d9e73fd8cb71b5bdd6c1");
        }
    }
}
