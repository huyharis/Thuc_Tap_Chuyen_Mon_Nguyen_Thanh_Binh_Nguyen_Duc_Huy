using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataClasses1DataContext db = new DataClasses1DataContext();
        Bitmap bitmap;
        int ma;
        int dem = 0;
        int choose;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (open.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(open.FileName);
                pictureBox1.Image = bitmap;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            choose = 1;
            txtTenSP.Enabled = true;
            txtGia.Enabled = true;
            panelSave_Delete.Visible = true;
            gunaGroupBox1.Visible = false;
            sanPham sp = new sanPham();
            if (sp == null)
            {
                ma = 1;
            }
            else
            {
                foreach (var a in db.sanPhams)
                {
                    dem++;
                }
                ma = dem + 1;
                label1.Text = ma.ToString();
                dem = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'loaiSP.nhomSP' table. You can move, or remove it, as needed.
            this.nhomSPTableAdapter.Fill(this.loaiSP.nhomSP);
            // TODO: This line of code loads data into the 'loaiSP.nhomSP' table. You can move, or remove it, as needed.
            this.nhomSPTableAdapter.Fill(this.loaiSP.nhomSP);

            comboBox1.Text = "";
            txtTenSP.Text = "";
            txtGia.Text = "";
            txtTenSP.Enabled = false;
            txtGia.Enabled = false;
            comboBox1.DisplayMember = "tenLoai";
            comboBox1.ValueMember = "maLoai";
            comboBox1.DataSource = db.nhomSPs;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = db.select_SP();
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            pictureBox1.Image.Save(stream, ImageFormat.Jpeg);
            if (choose == 1)
            {

                if (txtTenSP.Text == "" || txtGia.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenSP.Focus();
                }
                else
                {
                    db.addSP("sp" + label1.Text, txtTenSP.Text, Convert.ToDouble(txtGia.Text), stream.ToArray(), 0, null, comboBox1.SelectedValue.ToString());
                    Form1_Load(sender, e);
                }
            }
            if (choose == 2)
            {
                if (txtTenSP.Text == "" || txtGia.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenSP.Focus();
                }
                else
                {
                    db.updateSP(label1.Text, txtTenSP.Text, Convert.ToDouble(txtGia.Text), stream.ToArray(), comboBox1.SelectedValue.ToString());
                    Form1_Load(sender, e);
                }
            }


        }

        private void gunaImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelSave_Delete.Visible = false;
            gunaGroupBox1.Visible = true;
            Form1_Load(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            label1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTenSP.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtGia.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.Rows[i].Cells[4].Value.ToString();

            sanPham sp = db.sanPhams.Where(s => s.maSP == label1.Text).FirstOrDefault();
            if (sp == null || sp.anh == null)
            {

            }
            else
            {
                MemoryStream img = new MemoryStream(sp.anh.ToArray());
                Image image = Image.FromStream(img);
                if (image == null) { return; }
                else
                {
                    pictureBox1.Image = image;
                }
            }
            btnAdd.Enabled = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            choose = 2;
            gunaGroupBox1.Visible = false;
            panelSave_Delete.Visible = true;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            txtTenSP.Text = "";
            txtGia.Text = "";
            comboBox1.Text = "";
            Form1_Load(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete that Product ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                db.deleteSP(label1.Text);
            }
        }
    }
}
