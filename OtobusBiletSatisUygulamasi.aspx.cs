using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OtobusBiletSatisi
{
    public static class MyData
    {
        public static DataTable table;
        public static DataTable DataTable_Yolcular()
        {
            table = new DataTable("Yolcular");
            table.Columns.Add(new DataColumn("yol_RECno", typeof(int)));
            table.Columns.Add(new DataColumn("yol_Koltuk_No", typeof(int)));
            table.Columns.Add(new DataColumn("yol_Koltuk_Durum", typeof(int))); // 0 boş, 1 satış, 2 rezerve
            table.Columns.Add(new DataColumn("yol_Koltuk_Ad_Soyad", typeof(string)));
            table.Columns.Add(new DataColumn("yol_Koltuk_Cinsiyet", typeof(string)));
            table.Columns.Add(new DataColumn("yol_Koltuk_Islem_Tarih", typeof(DateTime)));
            table.Columns.Add(new DataColumn("yol_Koltuk_Islem_Yapan", typeof(string)));
            return table;
        }


        public static void DataTable_Yolcular_Insert(int koltuk_no, int durum, string ad_soyad, string cinsiyet, DateTime tarih, string gorevli)
        {
            int recno = 0;
            try
            {
                recno = table.Rows.Count + 1;
            }
            catch (NullReferenceException)
            {
                recno = 1;
            }
            table.Rows.Add(recno, koltuk_no, durum, ad_soyad, cinsiyet, tarih, gorevli);
        }
    }
}
private void Form1_Load(object sender, EventArgs e)
        {
            MyData.DataTable_Yolcular();
            DuzenKur();
        }
void DuzenKur()
        {
            int say = 0;
            panel1.Controls.Clear();
            int olcu = 38;
            for (int i = 0; i < txt_duzen.Lines.Count(); i++)// textbox satırları arasında
            {
                for (int j = 0; j < txt_duzen.Lines[i].Count(); j++) // bir satırdaki karakterler arasında
                {
                    string satir = txtduzen.Lines[i]; // bir satırı aldık
                    if (satir[j] == '') // satırdaki j index'ine denk gelen ifade ise
                    {
                        Button nesne = new Button();
                        nesne.Text = (++say).ToString();
                        nesne.Name = "buton" + nesne.Text;
                        nesne.BackColor = Color.Red;
                        nesne.Width = nesne.Height = 40;
                        nesne.Left = olcu * j;// butonun nerede duracağı
                        nesne.Top = olcu * i; // butonun nerede duracağı
                        panel1.Controls.Add(nesne);
                        nesne.Click += ButtonClick;
                    }
                }
            }
        }
private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Islem_Yap fr = new Islem_Yap();
            fr.lbl_koltukno.Text = btn.Name.Split('')[1];
            fr.ShowDialog();
            int durum = fr.cmb_islem.SelectedIndex + 1;
            string cinsiyet = fr.cmb_musteri_cinsiyet.Text;
            if (fr.tamam == 1)
            {
                MyData.DataTable_Yolcular_Insert(Convert.ToInt32(fr.lbl_koltuk_no.Text), durum, fr.txt_mustari.Text, cinsiyet, Convert.ToDateTime(fr.lbl_tarih.Text), fr.lbl_gorevli.Text);

                switch (durum)
                {
                    case 1:
                        btn.BackColor = Color.YellowGreen;
                        break;
                    case 2:
                        btn.BackColor = Color.Yellow;
                        break;
                }
                if (cinsiyet == "Erkek")
                    btn.Text += " E";
                else
                    btn.Text += " K";
                dgv_update();
            }
        }
private void dgv_update()
        {
            dataGridView1.DataSource = MyData.table;
        }
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OtobusBiletSatisi
{
    public partial class Islem_Yap : Form
    {
        public Islem_Yap()
        {
            InitializeComponent();
        }

        private void Islem_Yap_Load(object sender, EventArgs e)
        {
            lbl_tarih.Text = DateTime.Now.ToString();
        }

        private void btn_iptal_Click(object sender, EventArgs e)
        {
            Close();
        }
        public int tamam = 0;
        private void btn_kadyet_Click(object sender, EventArgs e)
        {
            if (txt_mustari.Text.Length > 0 && cmb_musteri_cinsiyet.SelectedIndex >= 0 && cmb_islem.SelectedIndex >= 0)
            {
                tamam = 1;
                Close();
            }
            else
            {
                MessageBox.Show("*'lı alanları boş bırakamazsınız.", "Zorunlu Alan", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}