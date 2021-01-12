using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class UpdateForm : Form
    {
        public static string NAME;
        string SEX;
        public UpdateForm()
        {
            InitializeComponent();
        }

        //修改联系人textbox框显示当前联系人的个人信息
        public UpdateForm(string name,string sex,string phone,string addr,string memo)
        {
            
            InitializeComponent();
            NAME = txtName.Text = name;
            selectSex.SelectedValue = sex;
            SEX = sex;
            txtPhone.Text = phone;
            txtAddress.Text = addr;
            txtMemo.Text = memo;
            
            
        }

        //确认修改联系人
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //SQL更新语句
            string sql = "update AddressInfo set [Name] = '"+ txtName.Text + "',Sex = '"+ selectSex.SelectedItem + "',Phone = '"+ txtPhone.Text + "',Address = '"+ txtAddress.Text + "',Memo = '"+ txtMemo.Text + "' where Name = '"+ NAME + "'";
            Dao dao = new Dao();

            if(dao.Execute(sql) > 0)
            {
                MessageBox.Show("修改成功！");
                this.Close();
            }
            //selectSex.SelectedItem = this.selectSex.SelectedItem.ToString();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            updateComboxsex();
        }

        //性别选择框只显示男跟女
        private void updateComboxsex()
        {
            //combox下拉框内容
            selectSex.Items.Add("男");
            selectSex.Items.Add("女");

            //当修改联系人信息时，combox默认显示当前数据库对应的性别
            selectSex.SelectedIndex = selectSex.Items.IndexOf(SEX);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
