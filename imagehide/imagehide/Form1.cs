using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace imagehide
{
    public partial class Form1 : Form
    {
        string key = "JVziuGwpgAucsv10dLHxzGPeAE4FFaAr";
        
        public Form1()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != null)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "Bitmap|*.bmp";
                if (s.ShowDialog() == DialogResult.OK)
                {
                    byte[] key2 = Encoding.UTF8.GetBytes(key);
                    string basetext = crypt.Base64Encode(textBox1.Text);
                    byte[] basetext2 = Encoding.UTF8.GetBytes(basetext);
                    byte[] etext = crypt.AESEncrypt(basetext2, key2);

                    Bitmap bit = imagehide.BitmapDataStorage.CreateBitmapFromData(etext);
                    bit.Save(s.FileName);
                    MessageBox.Show("Done!");
                }
            }
            else
            {
                MessageBox.Show("Please write some text");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Bitmap|*.bmp";
            if (o.ShowDialog() == DialogResult.OK)
            {
                byte[] key2 = Encoding.UTF8.GetBytes(key);

                Bitmap bt = new Bitmap(o.FileName);       
                byte[] text = BitmapDataStorage.ReadDataFromBitmap(bt);

                byte[] textde = crypt.AESDecrypt(text, key2);
                string text2 = Encoding.UTF8.GetString(textde);
                string textbase = crypt.Base64Decode(text2);
                textBox2.Text = textbase;
                
            }
        }
    }
}
