using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class LogoutForm : Form
    {
        public LogoutForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //获取将要注销的用户名和密码
            string logout_Name = logoutName.Text;
            string logut_Password = logoutPwd.Text;
            
            //判断是否输入了用户名和密码
            if ((logout_Name.Equals("")) || (logut_Password.Equals("")))
            {
                MessageBox.Show("请输入要注销的用户名和密码！");
                return;
            }
            else
            {
                Dao dao = new Dao();
                string sql = "select * from LoginUser where loginName = '" + logout_Name + "'and Loginpassword = '" + logut_Password + "'";
                IDataReader dc = dao.read(sql);
                if(dc.Read())
                {
                    //判断用户名和密码正确，提示是否注销，确定之后将该用户从数据库中删除
                    MessageBox.Show("是否要注销该用户？");
                }

                //验证通过,将该用户从数据库中删除
                string sqlstr = "delete LoginUser where loginName = '" + logout_Name + "'";
                if(dao.Execute(sqlstr) > 0)
                {
                    MessageBox.Show("注销成功！即将跳转登录界面");
                    //注销成功转至登录界面
                    Thread th = new Thread(delegate ()
                    {
                        new Login().ShowDialog();
                    });
                    Application.Exit();
                    th.Start();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("输入的用户名或者密码不正确，请重新输入！");
                }
                dc.Close();
                dao.DaoClose();
            }

            //清空所有文本框
            foreach (Control item in this.Controls)
            {
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void LogoutForm_Load(object sender, EventArgs e)
        {
            logoutName.Text = Login.myName;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;               //最小化
        }

        #region 窗口可移动
        Point mouseOff;         //鼠标移动变量位置
        bool leftFlag;          //左键标志位
        private void LogoutForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)       //左键是否按下
            {
                mouseOff = new Point(-e.X, -e.Y);     //得到变量的值
                leftFlag = true;                    //左键按下时标志位为true
            }
        }

        private void LogoutForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);    //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void LogoutForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;           //释放鼠标后标志位为false
            }
        }
        #endregion
    }
}
