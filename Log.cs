using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sale_system_new
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        private void FAdmin()
        {
            Admin AdminLogin = new Admin();
            this.Hide();
            AdminLogin.ShowDialog();
            this.Dispose();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            string userName = this.txtAccount.Text;
            string userPassword = this.txtPassword.Text;
            if (userName.Equals("") || userPassword.Equals(""))
            {
                MessageBox.Show("用户名或密码不能为空！");
            }
            else
            {
                if (userName == "xxx" && userPassword == "xxx")
                {
                    MessageBox.Show("欢迎你，管理员！");
                    txtAccount.Clear();
                    txtPassword.Clear();
                    MethodInvoker MethodAdmin = new MethodInvoker(FAdmin);  //不带参数的委托
                    BeginInvoke(MethodAdmin);
                }
                else
                {
                    MessageBox.Show("暂未设计");
                }
            }
        }

        private void Log_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog; //设置边框为不可调节
            this.MaximizeBox = false;   //取消最大化按键
            this.MinimizeBox = false;   //取消最小化按键
        }
    }
}
