using HZH_Controls.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack_UnityHub.GUI
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            ImageList image = new ImageList();
            image.ImageSize = new Size(1, 35);//设置每次点击view时以图片的形式
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "版本";
            ch.Width = splitContainer1.Panel1.Width;
            ch.TextAlign = HorizontalAlignment.Center;
            listView1.Columns.Add(ch);//设置listview的列名，没啥用处

            //设置每个view的显示形式
            listView1.SmallImageList = image;

            // 加载一级栏目
            this.LoadList();
            listView1.Width = ch.Width;//设置每个view的宽度都一致
            //listView1.Items[0].BackColor = Color.LightGray;//设置主页的选中后的颜色
            //启动首先展示主页
            //splitContainer1.Panel2.Controls.Clear();//每次执行时清空panel2
        }

        /// <summary>
        /// 一级菜单
        /// </summary>
        private void LoadList()
        {
            listView1.Items.Clear();//清空菜单
            //添加菜单
            listView1.Items.Add(new ListViewItem("     Unity 2017"));
            listView1.Items.Add(new ListViewItem("     Unity 2019"));
            listView1.Items.Add(new ListViewItem("     Unity 2020"));
            listView1.Items.Add(new ListViewItem("     Unity 2021"));
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            List<object> lstSource = new List<object>();
            for (int i = 0; i < 6; i++)
            {
                lstSource.Add("项-" + i);
            }
           
            //不使用分页控件
            this.ucListView1.DataSource = lstSource;
        }

        private void ucListView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void ucListView1_SelectedItemEvent(object sender, EventArgs e)
        {
            // 设置选项点击事件
            var aa = sender as UCListViewItem;
            MessageBox.Show(aa.DataSource.ToString());
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }
    }
}
