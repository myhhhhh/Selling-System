using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace sale_system_new
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        private string[] data = new string[7];

        public Update(string[] content, string Name)
            :this()
        {
            data = content;
            UName = Name;
            BackgroundWorker wk = new BackgroundWorker();
            wk.DoWork += bwLoad_DoWork;
            wk.RunWorkerCompleted += bwLoad_RunWorkerCompleted;
            wk.RunWorkerAsync();    //异步加载数据库
            l.ShowDialog();
        }

        protected string SQLNum = "Data Source=xxx;Initial Catalog=SaleSystem;Persist Security Info=True;User ID=sa;Password=xxx";

        private SqlConnection conn;

        private SaleSystemEntities0 sale = new SaleSystemEntities0();

        private void Update_Load(object sender, EventArgs e)
        {
            lblName.Text = UName;
            if(data[0] != "")
            {
                lbl11.Text = "前置滤芯下次安装时间";
                lbl22.Text = "RO滤芯下次安装时间";
                lbl33.Text = "后置滤芯下次安装时间";
                lbl44.Text = "";
                lbl1.Text = data[0];
                lbl2.Text = data[1];
                lbl3.Text = data[2];
                lbl4.Text = "";
                cmb1.Items.Add("前置滤芯");
                cmb1.Items.Add("RO滤芯"); 
                cmb1.Items.Add("后置滤芯");
            }
            else
            {
                lbl11.Text = "前置滤网下次安装时间";
                lbl22.Text = "HEPA滤网下次安装时间";
                lbl33.Text = "定制滤网下次安装时间";
                lbl44.Text = "后置滤网下次安装时间";
                lbl1.Text = data[3];
                lbl2.Text = data[4];
                lbl3.Text = data[5];
                lbl4.Text = data[6];
                cmb1.Items.Add("前置滤网");
                cmb1.Items.Add("HEPA滤网");
                cmb1.Items.Add("定制滤网");
                cmb1.Items.Add("后置滤网");
            }
        }

        private static string UName = null;    //用来接收用户名

        private Loading l = new Loading();

        private void bwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            conn = new SqlConnection(SQLNum);
            try
            {
                conn.Open();   //打开数据库连接
            }
            catch (Exception ex)     //捕获异常，并输出异常问题
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void bwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            l.Close();
            l.Dispose();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            DateTime nowtime = DateTime.Now;
            var changeMachine = sale.Machine.FirstOrDefault(u => u.custName == UName);
            if(changeMachine != null)
            {
                if (cmb1.Items[0].ToString() == "前置滤芯")
                {
                    if (cmb1.Text.ToString() == "前置滤芯")
                        changeMachine.LvXing1_Last = nowtime.AddDays(180).ToString("yyyy年MM月dd日");
                    else if (cmb1.Text.ToString() == "RO滤芯")
                        changeMachine.LvXing2_Last = nowtime.AddDays(730).ToString("yyyy年MM月dd日");
                    else if(cmb1.Text.ToString() == "后置滤芯")
                        changeMachine.LvXing3_Last = nowtime.AddDays(365).ToString("yyyy年MM月dd日");
                }
                else if(cmb1.Items[0].ToString() == "前置滤网")
                {
                    if (cmb1.Text.ToString() == "前置滤网")
                        changeMachine.LvXing1_Last = nowtime.AddDays(180).ToString("yyyy年MM月dd日");
                    else if (cmb1.Text.ToString() == "HEPA滤网")
                        changeMachine.LvXing2_Last = nowtime.AddDays(365).ToString("yyyy年MM月dd日");
                    else if (cmb1.Text.ToString() == "定制滤网")
                        changeMachine.LvXing3_Last = nowtime.AddDays(180).ToString("yyyy年MM月dd日");
                    else if(cmb1.Text.ToString() == "后置滤网")
                        changeMachine.LvXing2_Last = nowtime.AddDays(365).ToString("yyyy年MM月dd日");
                }
                sale.SaveChanges();
                MessageBox.Show("修改成功！");
            }
        }
    }
}
