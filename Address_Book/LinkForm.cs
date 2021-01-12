using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    
    public partial class LinkForm : Form
    {
        public static LinkForm linkForm;
        public LinkForm()
        {
            InitializeComponent();
            linkForm = this;
        }

        //状态条显示当前时间
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            StatusLabelTimesDis.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");        //以年月日 时分秒的形式显示时间
        }

        private void LinkForm_Load(object sender, EventArgs e)
        {
            Table();        //刷新表格数据
            StatusLabelUsername.Text = Login.myName;            //登录成功后 状态条左下角显示当前登录用户
            //多选框添加信息
            ComboBoxQuery.Items.Add("姓名");
            ComboBoxQuery.Items.Add("手机号");
            ComboBoxQuery.Items.Add("模糊查询");
            ComboBoxQuery.SelectedIndex = 0;        //默认显示姓名查询

            


        }

        //从数据库读取数据显示在表格中
        public void Table()
        {
            dataGridView1.Rows.Clear();     //清空旧数据 
            Dao dao = new Dao();
            string sql = "select * from AddressInfo";
            IDataReader dc = dao.read(sql);
            while (dc.Read())       //读取数据
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(),dc[5].ToString());
            }



            //翻页
            BindingSource bs = new BindingSource();
            bs.DataSource = dao.read(sql);
            bindingNavigator1.BindingSource = bs;

            dc.Close();
            dao.DaoClose();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();        //获取姓名

                //弹出对话框是否确认删除
                DialogResult dialogResult = MessageBox.Show("确认删除?", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)     //确认删除
                {
                    string sqlstr = "delete from AddressInfo where Id = '"+ name + "'";       //SQL删除语句
                    Dao dao = new Dao();
                    if (dao.Execute(sqlstr) > 0)
                    {
                        MessageBox.Show("删除成功！");
                        Table();                        //刷新表格数据
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                    dao.DaoClose();
                }
            }

            catch
            {
                MessageBox.Show("请先在表格中选中要删除的联系人!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //修改联系人
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //获取联系人信息
                string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string sex = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string phone = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                string addr = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                string memo = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                UpdateForm updateForm = new UpdateForm(name,sex,phone,addr,memo);
                updateForm.ShowDialog();
                Table();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }


        //查询联系人
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string queryTerm = ComboBoxQuery.SelectedItem.ToString();       //获取查询条件

            //按姓名查询
            if (queryTerm == "姓名")
            {
                FindName();
            }
            //按手机号查询
            else if (queryTerm == "手机号")
            {
                FindPhone();
            }
            //模糊查询
            else if (queryTerm == "模糊查询")
            {
                FuzzyQuery();
            }
        }

        //姓名查询
        public void FindName()
        {
            dataGridView1.Rows.Clear();     //清空旧数据 
            Dao dao = new Dao();
            string sql = "select * from AddressInfo where Name = '"+ txtFind .Text+ "'";        //姓名查询SQL语句
            IDataReader dc = dao.read(sql);
            
            //先判断文本框是否为空
            if (txtFind.Text.Equals(""))
            {
                MessageBox.Show("查询不能为空,请重新输入！");
                Table();
            }
            //查询到数据
            else if (dc.Read())       //读取数据
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString());
            }
            else
            {
                MessageBox.Show("没有找到!");
            }

            dc.Close();
            dao.DaoClose();
        }

        //按手机号查询
        public void FindPhone()
        {
            dataGridView1.Rows.Clear();     //清空旧数据 
            Dao dao = new Dao();
            string sql = "select * from AddressInfo where Phone = '" + txtFind.Text + "'";        //姓名查询SQL语句
            IDataReader dc = dao.read(sql);

            //先判断文本框是否为空
            if (txtFind.Text.Equals(""))
            {
                MessageBox.Show("查询不能为空,请重新输入！");
                Table();
            }
            //查询到数据
            else if (dc.Read())       //读取数据
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString());
            }
            else
            {
                MessageBox.Show("没有找到!");
            }

            dc.Close();
            dao.DaoClose();
        }


        //模糊查询
        public void FuzzyQuery()
        {
            Boolean flag = true;
            dataGridView1.Rows.Clear();     //清空旧数据 
            Dao dao = new Dao();
            string sql = "select * from AddressInfo where [Name] like '%"+txtFind.Text+"%' or Phone like '%"+txtFind.Text+"%'";        //姓名查询SQL语句
            IDataReader dc = dao.read(sql);

            while (dc.Read())       //读取数据
            {
                flag = false;
                //先判断文本框是否为空
                if (txtFind.Text.Equals(""))
                {
                    MessageBox.Show("查询不能为空,请重新输入！");
                    Table();
                    break;
                }
                //查询到数据     
                else
                {
                    dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString());
                }
            }
            if(flag)
            {
                MessageBox.Show("没有找到!");
            }



            dc.Close();
            dao.DaoClose();
        }

        //切换用户
        private void 切换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("确定退出？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question))
            {
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location + "");
                Process.GetCurrentProcess().Kill();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //获取联系人信息
                string name = Login.userName;
                ChangePasswordForm changePasswordForm = new ChangePasswordForm(name);
                changePasswordForm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void 注销账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogoutForm logoutForm = new LogoutForm();
            logoutForm.ShowDialog();
        }
    }
}
