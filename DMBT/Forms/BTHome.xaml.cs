using API;
using DMBT.Models;
using Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// BTHome.xaml 的交互逻辑
    /// </summary>
    public partial class BTHome : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        ObservableCollection<BT> _showList;
        public ObservableCollection<BT> ShowList
        {
            get
            {
                if (_showList == null)
                {
                    _showList = new ObservableCollection<BT>();
                }
                return _showList;
            }
            set
            {
                _showList = value;
                if (PropertyChanged != null)//有改变  
                { PropertyChanged(this, new PropertyChangedEventArgs("ShowList")); }
            }
        }

        ObservableCollection<Models.Page> _pageList;
        public ObservableCollection<Models.Page> PageList
        {
            get
            {
                if (_pageList == null)
                {
                    _pageList = new ObservableCollection<Models.Page>();
                }
                return _pageList;
            }
            set
            {
                _pageList = value;
                if (PropertyChanged != null)//有改变  
                { PropertyChanged(this, new PropertyChangedEventArgs("PageList")); }
            }
        }

        int pageindex;
        public int PageIndex
        {
            get
            {
                return pageindex;
            }
            set
            {
                pageindex = value;
                if (PropertyChanged != null)//有改变  
                { PropertyChanged(this, new PropertyChangedEventArgs("PageIndex")); }
            }
        }


        public BTHome()
        {
            InitializeComponent();

            DataContext = this;

            dgvShow.ItemsSource = ShowList;
            dgvPageList.ItemsSource = PageList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSerach_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = 1;

            TextBox tb = (TextBox)tbKey.Template.FindName("ctbKey", tbKey);
            string key = tb.Text;

            Serach(1,key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        public void Serach(int mode,string key)
        {
            ShowList.Clear();
            PageList.Clear();

            //启动动画
            myProgress.Visibility = Visibility.Visible;
            myProgress.Start();

            switch (Auth.Type)
            {
                case BTType.BT蚂蚁:
                    new BTMY().Serach(mode,key, new Action<List<BT>,List<Models.Page>,string>((list,pagelist,info) =>
                      {
                          if (list != null)
                          {

                              Dispatcher.Invoke(new Action(() =>
                              {
                                      for (int i = 0; i < list.Count; i++)
                                      {
                                          ShowList.Add(list[i]);
                                      }
                              }));
                              Dispatcher.Invoke(new Action(() =>
                              {
                                  for (int i = 0; i < pagelist.Count; i++)
                                  {
                                      PageList.Add(new Models.Page() { Name = pagelist[i].Name,Path = pagelist[i].Path,Type = BTType.BT蚂蚁});
                                  }
                              }));
                          }
                          Dispatcher.Invoke(new Action(() =>
                          {
                              myProgress.Stop();
                              myProgress.Visibility = Visibility.Collapsed;
                          }));
                      }));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 切换分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPage_Click(object sender, RoutedEventArgs e)
        {
            //获取点击的分页页数
            RadioButton ra = sender as RadioButton;
            if (ra!=null)
            {
               Models.Page pa =  ra.Tag as Models.Page;
                string[] listtxt = pa.Path.Split('-');
                string pagetxt = listtxt.LastOrDefault();
                try
                {
                    PageIndex = Convert.ToInt32(pagetxt);
                }
                catch (Exception)
                { }

                Serach(2, pa.Path);
            }
        }

        /// <summary>
        /// Enter 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctbKey_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PageIndex = 1;

                TextBox tb = (TextBox)tbKey.Template.FindName("ctbKey", tbKey);
                string key = tb.Text;

                Serach(1, key);
            }
        }

        private void XunleiClick(object sender, RoutedEventArgs e)
        {
            Button ra = sender as Button;
            if (ra != null)
            {
                BT bt = ra.Tag as BT;
                Process.Start(bt.Xunlei);
            }
        }

        private void MagnetClick(object sender, RoutedEventArgs e)
        {
            Button ra = sender as Button;
            if (ra != null)
            {
                BT bt = ra.Tag as BT;
                Process.Start(bt.Magnet);
            }
        }
    }
}
