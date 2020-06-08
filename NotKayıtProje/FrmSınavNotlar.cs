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

namespace NotKayıtProje
{
    public partial class FrmSınavNotlar : Form
    {
        public FrmSınavNotlar()
        {
            InitializeComponent();
        }
        DataSet1TableAdapters.TBLNOTLARTableAdapter ds = new DataSet1TableAdapters.TBLNOTLARTableAdapter();
        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.NotListesi(int.Parse(TxtID.Text));
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-I0KDOLK\SQLEXPRESS;Initial Catalog=OkulProje;Integrated Security=True");
        private void FrmSınavNotlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLDERSLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "DERSAD";
            comboBox1.ValueMember = "DERSID";
            comboBox1.DataSource = dt;
            baglanti.Close();
        }
        int notid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtSınav1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSınav2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtSınav3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            TxtID.Text = "";
            TxtSınav1.Text = "";
            TxtSınav2.Text = "";
            TxtSınav3.Text = "";
            TxtProje.Text = "";
            TxtOrtalama.Text = "";
            TxtDurum.Text = "";
        }
        double sinav1, sinav2, sinav3, proje, ortalama;

        string durum;
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            sinav1 = Convert.ToDouble(TxtSınav1.Text);
            sinav2 = Convert.ToDouble(TxtSınav2.Text);
            sinav3 = Convert.ToDouble(TxtSınav3.Text);
            proje = Convert.ToDouble(TxtProje.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            TxtOrtalama.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                TxtDurum.Text = "True";
            }
            else
            {
                TxtDurum.Text = "False";
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(comboBox1.SelectedValue.ToString()), int.Parse(TxtID.Text), byte.Parse(TxtSınav1.Text), byte.Parse(TxtSınav2.Text), byte.Parse(TxtSınav3.Text), byte.Parse(TxtProje.Text), decimal.Parse(TxtOrtalama.Text), bool.Parse(TxtDurum.Text), notid);
            MessageBox.Show("Sınav Notu Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
