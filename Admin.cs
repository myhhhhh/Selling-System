using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Data.Common;

using NPOI;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace sale_system_new
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private string SQLNum = "Data Source=xxx;Initial Catalog=SaleSystem;User ID=sa;Password=xxx";

        private SqlConnection conn;

        private SaleSystemEntities0 sale = new SaleSystemEntities0();

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Customer newCust = new Customer();
            Machine newMachine = new Machine();
            newCust.custName = txtcustName.Text;
            newCust.custType = txtcustType.Text;
            newCust.saleWay = cmbsaleWay.Text;
            newCust.custCon = txtcustCon.Text;
            newCust.phoneNum = txtphoneNum.Text;
            newCust.custAddr = txtcustAddr.Text;
            newCust.machineType = cmb1.Text;
            newMachine.custName = txtcustName.Text;
            newMachine.typeNum = txttypeNum.Text;
            newMachine.fixDate = dtpfixDate.Text;
            newMachine.salePrice = txtsalePrice.Text;
            DateTime startTime = dtpchangeDate.Value;
            newMachine.changeDate = dtpchangeDate.Text;
            newMachine.MoreInfo = txtMore.Text;
            if(cmb1.Text == "净水器")
            {
                newMachine.LvXing1_Last = startTime.AddDays(180).ToString("yyyy年MM月dd日");
                newMachine.LvXing2_Last = startTime.AddDays(730).ToString("yyyy年MM月dd日");
                newMachine.LvXing3_Last = startTime.AddDays(365).ToString("yyyy年MM月dd日");
            }
            else if(cmb1.Text == "空气净化器")
            {
                newMachine.LvWang1_Last = startTime.AddDays(180).ToString("yyyy年MM月dd日");
                newMachine.LvWang2_Last = startTime.AddDays(365).ToString("yyyy年MM月dd日");
                newMachine.LvWang3_Last = startTime.AddDays(180).ToString("yyyy年MM月dd日");
                newMachine.LvWang4_Last = startTime.AddDays(365).ToString("yyyy年MM月dd日");
            }
            sale.Customer.Add(newCust);
            sale.Machine.Add(newMachine);
            sale.SaveChanges();
            MessageBox.Show("信息添加成功！");
            txtcustName.Clear();
            txtcustType.Clear();
            cmbsaleWay.Text = "";
            txtcustCon.Clear();
            txtphoneNum.Clear();
            txtcustAddr.Clear();
            cmb1.Text = "";
            txttypeNum.Clear();
            txtsalePrice.Clear();
            txtMore.Clear();
        }

        public delegate void ShowFormHandler(string UserName);  //传参的委托
        private void FWaterMessage(string UserName)
        {
            MachineMessages wm = new MachineMessages(UserName);
            wm.Owner = this;
            wm.ShowDialog();
        }

        private void btnQuary_Click(object sender, EventArgs e)
        {
            string Name = cmbQuaryCust.Text;
            ShowFormHandler delShowForm = new ShowFormHandler(FWaterMessage);   //带参数的委托
            this.BeginInvoke(delShowForm, new Object[] { Name });
            cmbQuaryCust.Items.Clear();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string[] content = new string[7];
            string sel = "select * from Machine where custName = '" + txtchangeCust.Text.ToString() + "'";
            SqlCommand comm = new SqlCommand(sel, conn);        //定义执行SQL语句的对象，并将SQL语句赋予对象
            DataSet ds = new DataSet();         //定义存储查询结果的数据容器
            SqlDataAdapter da = new SqlDataAdapter(comm);       //执行连接
            da.Fill(ds);
            for(int i = 5; i < ds.Tables[0].Columns.Count - 1; i++)
                content[i-5] = ds.Tables[0].Rows[0][i].ToString();
            Update FUpdate = new Update(content, txtchangeCust.Text.ToString());
            txtchangeCust.Clear();
            FUpdate.Show();
        }

        private void btnDele_Click(object sender, EventArgs e)
        {
            SaleSystemEntities0 sale = new SaleSystemEntities0();
            Customer deleNum = new Customer() { custName  = txtDeleCust.Text };
            Machine deleMachine = new Machine() { custName = txtDeleCust.Text };
            sale.Customer.Attach(deleNum);
            sale.Customer.Remove(deleNum);
            sale.SaveChanges();
            MessageBox.Show("删除成功");
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

        private void Admin_Load(object sender, EventArgs e)
        {
            BackgroundWorker wk = new BackgroundWorker();
            wk.DoWork += bwLoad_DoWork;
            wk.RunWorkerCompleted += bwLoad_RunWorkerCompleted;
            wk.RunWorkerAsync();    //异步加载数据库
            l.ShowDialog();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            string sel = "select * from Customer inner join Machine on Customer.custName = Machine.custName";
            SqlCommand comm = new SqlCommand(sel, conn);        //定义执行SQL语句的对象，并将SQL语句赋予对象
            DataSet ds = new DataSet();         //定义存储查询结果的数据容器
            SqlDataAdapter da = new SqlDataAdapter(comm);       //执行连接
            da.Fill(ds);
            //string path = "E:\\VS C#\\总表.xls";
            string path = txtSave.Text.ToString() + "\\总表.xls";
            WriteExcel(ds, path);
        }

        public static void WriteExcel(DataSet ds, string path)
        {
            try
            {
                long totalCount = ds.Tables[0].Rows.Count;
                Thread.Sleep(1000);
                long rowRead = 0;
                float percent = 0;

                StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                {
                    sb.Append(ds.Tables[0].Columns[k].ColumnName.ToString() + "\t");
                }
                sb.Append(Environment.NewLine);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rowRead++;
                    percent = ((float)(100 * rowRead)) / totalCount;

                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        sb.Append(ds.Tables[0].Rows[i][j].ToString() + "\t");
                    }
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                MessageBox.Show("已经生成指定Excel文件!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbQuaryCust_TextChanged(object sender, EventArgs e)
        {
            string key = cmbQuaryCust.Text.ToString().Trim();
            string sel = "select custCon from Customer where custCon like '%" + key + "%'";
            SqlCommand comm = new SqlCommand(sel, conn);        //定义执行SQL语句的对象，并将SQL语句赋予对象
            DataSet ds = new DataSet();         //定义存储查询结果的数据容器
            SqlDataAdapter da = new SqlDataAdapter(comm);       //执行连接
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                cmbQuaryCust.Items.Add(ds.Tables[0].Rows[i][0].ToString());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string UName = txtcustName.Text.ToString();
            var updateCustomer = sale.Customer.FirstOrDefault(u => u.custName == UName);
            var updateMachine = sale.Machine.FirstOrDefault(u => u.custName == UName);
            if(updateCustomer != null && updateMachine != null)
            {
                if (updateCustomer.custType != txtcustType.Text.ToString() && txtcustType.Text.ToString() != "")
                    updateCustomer.custType = txtcustType.Text.ToString();
                if (updateCustomer.saleWay != cmbsaleWay.Text.ToString() && cmbsaleWay.Text.ToString() != "")
                    updateCustomer.saleWay = cmbsaleWay.Text.ToString();
                if (updateCustomer.custCon != txtcustCon.Text.ToString() && txtcustCon.Text.ToString() != "")
                    updateCustomer.custCon = txtcustCon.Text.ToString();
                if (updateCustomer.phoneNum != txtphoneNum.Text.ToString() && txtphoneNum.Text.ToString() != "")
                    updateCustomer.phoneNum = txtphoneNum.Text.ToString();
                if (updateCustomer.custAddr != txtcustAddr.Text.ToString() && txtcustAddr.Text.ToString() != "")
                    updateCustomer.custAddr = txtcustAddr.Text.ToString();
                if (updateCustomer.machineType != cmb1.Text.ToString() && cmb1.Text.ToString() != "")
                    updateCustomer.machineType = cmb1.Text.ToString();
                if (updateMachine.typeNum != txttypeNum.Text.ToString() && txttypeNum.Text.ToString() != "")
                    updateMachine.typeNum = txttypeNum.Text.ToString();
                if (updateMachine.fixDate != dtpfixDate.Text.ToString() && cbfix.Checked)
                    updateMachine.fixDate = dtpfixDate.Text.ToString();
                if (updateMachine.salePrice != txtsalePrice.Text.ToString() && txtsalePrice.Text.ToString() != "")
                    updateMachine.salePrice = txtsalePrice.Text.ToString();
                if (updateMachine.changeDate != dtpchangeDate.Text.ToString() && cbchange.Checked)
                {
                    updateMachine.changeDate = dtpchangeDate.Text.ToString();
                    DateTime startTime = dtpchangeDate.Value;
                    if (updateCustomer.machineType.ToString().Trim() == "净水器")
                    {
                        updateMachine.LvXing1_Last = startTime.AddDays(180).ToString("yyyy年MM月dd日");
                        updateMachine.LvXing2_Last = startTime.AddDays(730).ToString("yyyy年MM月dd日");
                        updateMachine.LvXing3_Last = startTime.AddDays(365).ToString("yyyy年MM月dd日");
                    }
                    else if (updateCustomer.machineType.ToString().Trim() == "空气净化器")
                    {
                        updateMachine.LvWang1_Last = startTime.AddDays(180).ToString("yyyy年MM月dd日");
                        updateMachine.LvWang2_Last = startTime.AddDays(365).ToString("yyyy年MM月dd日");
                        updateMachine.LvWang3_Last = startTime.AddDays(180).ToString("yyyy年MM月dd日");
                        updateMachine.LvWang4_Last = startTime.AddDays(365).ToString("yyyy年MM月dd日");
                    }
                }
            }
            sale.SaveChanges();
            MessageBox.Show("订单编号" + txtcustName.Text.ToString() + "修改成功！");
            txtcustName.Clear();
            txtcustType.Clear();
            cmbsaleWay.Text = "";
            txtcustCon.Clear();
            txtphoneNum.Clear();
            txtcustAddr.Clear();
            cmb1.Text = "";
            txttypeNum.Clear();
            txtsalePrice.Clear();
            txtMore.Clear();
            cbfix.Checked = false;
            cbchange.Checked = false;
        }

        private void cmbsaleWay_DropDownClosed(object sender, EventArgs e)
        {
            cmbsaleWay.Text = "";
        }

        private void cmb1_DropDownClosed(object sender, EventArgs e)
        {
            cmb1.Text = "";
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            var checkCust = sale.Customer.FirstOrDefault(u => u.custName == txtcustName.Text.ToString().Trim());
            var checkMachine = sale.Machine.FirstOrDefault(u => u.custName == txtcustName.Text.ToString().Trim());
            //MessageBox.Show(checkCust.custType.ToString().Trim());
            if (checkCust != null && checkMachine != null)
            {
                txtcustType.Text = checkCust.custType.ToString().Trim();
                cmbsaleWay.Text = checkCust.saleWay.ToString().Trim();
                txtcustCon.Text = checkCust.custCon.ToString().Trim();
                txtphoneNum.Text = checkCust.phoneNum.ToString().Trim();
                txtcustAddr.Text = checkCust.custAddr.ToString().Trim();
                cmb1.Text = checkCust.machineType.ToString().Trim();
                txttypeNum.Text = checkMachine.typeNum.ToString().Trim();
                txtsalePrice.Text = checkMachine.salePrice.ToString().Trim();
                if(checkMachine.MoreInfo != null)
                    txtMore.Text = checkMachine.MoreInfo.ToString().Trim();
                dtpfixDate.Value = Convert.ToDateTime(checkMachine.fixDate.ToString());
                dtpchangeDate.Value = Convert.ToDateTime(checkMachine.changeDate.ToString());
            }
            else
                MessageBox.Show("查无此订单，请检查您的订单编号是否存在？");
        }
    }
}
