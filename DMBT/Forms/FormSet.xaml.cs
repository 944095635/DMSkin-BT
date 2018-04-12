using API;
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

namespace DMBT.Forms
{
    /// <summary>
    /// FormSet.xaml 的交互逻辑
    /// </summary>
    public partial class FormSet : UserControl
    {
        public FormSet()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string str = btn.Content.ToString();
            MainWindow.main.FontFamily = this.FindResource(str) as FontFamily;
            MainWindow.main.ApplyTemplate();

            //保存当前字体
            Save.SaveFont(str);
        }

        private void DMSkin_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.dmskin.com");
        }

        /// <summary>
        /// 软件升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://sighttp.qq.com/authd?IDKEY=41449ed2961c9b9e41b84db0dce6d9e73fd8cb71b5bdd6c1");
        }
    }
}
