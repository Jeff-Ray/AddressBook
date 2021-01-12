using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    
    public partial class Login : Form
    {
        public static string userName;
        public Login()
        {
            InitializeComponent();
        }
        public static string myName;
        #region 窗口可移动
        Point mouseOff;         //鼠标移动变量位置
        bool leftFlag;          //左键标志位
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)       //左键是否按下
            {
                mouseOff = new Point(-e.X, -e.Y);     //得到变量的值
                leftFlag = true;                    //左键按下时标志位为true
            }
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if(leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);    //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            if(leftFlag)
            {
                leftFlag = false;           //释放鼠标后标志位为false
            }
        }
        #endregion

        private void btnMinisize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        

        //退出程序
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*------登录验证--------*/
        private void btnLogin_Click(object sender, EventArgs e)
        {
            userName = txtName.Text;     //用户名
            string password = txtPwd.Text;      //密码
            

            //判断用户名或密码是否为空
            if (userName.Equals(""))
            {
                MessageBox.Show("用户名不能为空!");
            }
            else if (password.Equals(""))
            {
                MessageBox.Show("密码不能为空!");
            }
            else
            {

                Dao dao = new Dao();        //实例化数据库连接对象
                string sqlstr = "select * from LoginUser where loginName = '"+ userName + "' and loginPassword = '"+ password + "'";     //SQL查询语句
                IDataReader dc = dao.read(sqlstr);      //调用读取数据库方法
                
                if (dc.Read())      //查询是否有数据  返回的是bool类型 为true时说明读取到数据
                {
                    myName = userName;
                    //MessageBox.Show("登录成功");
                    //登录成功后打开登录窗口的同时关闭当前注册窗口
                    Thread th = new Thread(delegate ()
                    {
                        new LinkForm().ShowDialog();
                    });
                    th.Start();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误!");
                }
                dc.Close();         //关闭数据读取
                dao.DaoClose();     //关闭数据库连接
            }
            
        }

        private void RegisterLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Thread th = new Thread(delegate ()
            {
                new RegisterForm().ShowDialog();
            });
            th.Start();
            this.Close();
        }
    }
}
