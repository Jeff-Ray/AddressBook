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
    public partial class ChangePasswordForm : Form
    {
        string NAME;
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        public ChangePasswordForm(string name)
        {
            InitializeComponent();
            NAME = txtName.Text = name;        //文本框显示当前联系人姓名
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string oldPwd = txtOldPwd.Text;
            string newPws = txtNewpwd.Text;
            string newCorrectpwd = txtCorrectpwd.Text;
            Dao dao = new Dao();

            //旧密码不能为空
            if (oldPwd.Equals(""))
            {
                MessageBox.Show("旧密码不能为空");
            }
            else if(newPws.Equals(""))
            {
                MessageBox.Show("新密码不能为空");
            }
            else if(newCorrectpwd.Equals(""))
            {
                MessageBox.Show("确认密码不能为空");
            }
            else
            {
                if(newPws != newCorrectpwd)
                {
                    MessageBox.Show("两次输入密码不同");
                }
                else
                {
                    string sql = "select * from LoginUser where loginPassword = '" + oldPwd + "'";       //查询SQL语句
                    IDataReader dc = dao.read(sql);     //查询是否有数据  返回的是bool类型 为true时说明读取到数据 
                    if (dc.Read())
                    {
                        //修改密码SQL语句
                        string sqlstr = "update LoginUser set loginPassword = '" + newCorrectpwd + "' where loginName = '" + NAME + "'";
                        if (dao.Execute(sqlstr) > 0)
                        {
                            MessageBox.Show("修改密码成功！即将跳转至登录界面!");

                            //修改密码成功后打开登录窗口的同时关闭当前修改密码窗口
                            Thread th = new Thread(delegate ()
                            {
                                new Login().ShowDialog();
                                
                            });
                            Application.Exit();
                            th.Start();
                            this.Dispose();
                            
                        }
                        else
                        {
                            MessageBox.Show("修改密码失败！");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("原密码错误，请重新输入!");
                    }
                    dc.Close();
                }
                dao.DaoClose();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
