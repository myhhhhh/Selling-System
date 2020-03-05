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
    public partial class MachineMessages : Form
    {
        public MachineMessages()
        {
            InitializeComponent();
        }

        protected string SQLNum = "Data Source=xxx;Initial Catalog=SaleSystem;User ID=sa;Password=xxx";
        
        private SqlConnection conn;

        private static string UName = null;    //用来接收用户名

        private Loading l = new Loading();

        public MachineMessages(string text)   //需要传参（传入用户名）的Users构造函数
            : this()
        {
            UName = text;   //保存为UName
            BackgroundWorker wk = new BackgroundWorker();
            wk.DoWork += bwLoad_DoWork;
            wk.RunWorkerCompleted += bwLoad_RunWorkerCompleted;
            wk.RunWorkerAsync();    //异步加载数据库
            l.ShowDialog();
        }

        private void WaterMessage_Load(object sender, EventArgs e)
        {
            lblName.Text = UName;

            this.FormBorderStyle = FormBorderStyle.FixedDialog; //设置边框为不可调节
            this.MaximizeBox = false;   //取消最大化按键
            this.MinimizeBox = false;   //取消最小化按键

            string sel = "select Machine.custName, custType, saleWay, custCon, phoneNum, custAddr, typeNum, fixDate, salePrice," +
                " changeDate, LvXing1_Last, LvXing2_Last, LvXing3_Last " +
                "from Machine, Customer where Machine.custName = Customer.custName and Customer.machineType = '净水器' and " +
                "Customer.custCon = '" + UName + "' order by fixDate";
            SqlCommand comm = new SqlCommand(sel, conn);        //定义执行SQL语句的对象，并将SQL语句赋予对象
            DataSet ds = new DataSet();         //定义存储查询结果的数据容器
            SqlDataAdapter da = new SqlDataAdapter(comm);       //执行连接
            da.Fill(ds);                                        //将数据存入数据容器
            dgvShowWater.DataSource = ds.Tables[0];  //在dgv中显示结果
            dgvShowWater.ColumnHeadersVisible = true;
            dgvShowWater.Columns[0].HeaderText = "订单编号";
            dgvShowWater.Columns[1].HeaderText = "客户种类";
            dgvShowWater.Columns[2].HeaderText = "销售形式";
            dgvShowWater.Columns[3].HeaderText = "客户联系人";
            dgvShowWater.Columns[4].HeaderText = "联系方式";
            dgvShowWater.Columns[5].HeaderText = "客户地址";
            dgvShowWater.Columns[6].HeaderText = "型号";
            dgvShowWater.Columns[7].HeaderText = "安装日期";
            dgvShowWater.Columns[8].HeaderText = "销售价格";
            dgvShowWater.Columns[9].HeaderText = "更换时间";
            dgvShowWater.Columns[10].HeaderText = "前置滤芯下次更换时间";
            dgvShowWater.Columns[11].HeaderText = "RO滤芯下次更换时间";
            dgvShowWater.Columns[12].HeaderText = "后置滤芯下次更换时间";

            sel = "select Machine.custName, custType, saleWay, custCon, phoneNum, custAddr, typeNum, fixDate, salePrice," +
                " changeDate, LvWang1_Last, LvWang2_Last, LvWang3_Last, LvWang4_Last " +
                "from Machine, Customer where Machine.custName = Customer.custName and Customer.machineType = '空气净化器' and " +
                "Customer.custCon = '" + UName + "' order by fixDate";
            comm = new SqlCommand(sel, conn);        //定义执行SQL语句的对象，并将SQL语句赋予对象
            ds = new DataSet();         //定义存储查询结果的数据容器
            da = new SqlDataAdapter(comm);       //执行连接
            da.Fill(ds);                                        //将数据存入数据容器
            dgvShowAir.DataSource = ds.Tables[0];  //在dgv中显示结果
            dgvShowAir.ColumnHeadersVisible = true;
            dgvShowAir.Columns[0].HeaderText = "订单编号";
            dgvShowAir.Columns[1].HeaderText = "客户种类";
            dgvShowAir.Columns[2].HeaderText = "销售形式";
            dgvShowAir.Columns[3].HeaderText = "客户联系人";
            dgvShowAir.Columns[4].HeaderText = "联系方式";
            dgvShowAir.Columns[5].HeaderText = "客户地址";
            dgvShowAir.Columns[6].HeaderText = "型号";
            dgvShowAir.Columns[7].HeaderText = "安装日期";
            dgvShowAir.Columns[8].HeaderText = "销售价格";
            dgvShowAir.Columns[9].HeaderText = "更换时间";
            dgvShowAir.Columns[10].HeaderText = "前置滤网下次更换时间";
            dgvShowAir.Columns[11].HeaderText = "HEPA滤网下次更换时间";
            dgvShowAir.Columns[12].HeaderText = "定制滤网下次更换时间";
            dgvShowAir.Columns[13].HeaderText = "后置滤网下次更换时间";
        }

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
    }
}
