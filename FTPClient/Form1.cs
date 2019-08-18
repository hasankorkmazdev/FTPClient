using Lib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FTPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        bool FTPServerState = false;
        FtpClient ftp;
        string FTPWriteFilePath;
        string FTPWriteFileName;
       
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ftp = new FtpClient(textBox1.Text, textBox2.Text, textBox3.Text);
            bool state = ftp.ConnectionTest();
            if (state)
            {
                //Connection Oluştu
               
                lblFTPServerStatus.Text = "FtpServer Durumu : OK";
                btnFTPServerStatus.BackgroundImage = FTPClient.Properties.Resources.Ok_icon;
            }
            else
            {
                //Connection Oluşmadı
   
                lblFTPServerStatus.Text = "FtpServer Durumu : NULL";
                btnFTPServerStatus.BackgroundImage = FTPClient.Properties.Resources.Ad_Aware_icon;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            DialogResult msgResult;
            OpenFileDialog FTPServerWriteFile = new OpenFileDialog();
            FTPServerWriteFile.ShowDialog();
            FTPWriteFilePath = FTPServerWriteFile.FileName;
            FTPWriteFileName = FTPServerWriteFile.SafeFileName;
            //Serverda Böyle Bir Dosya Varmi ?
            if (ftp.FindFile(FTPWriteFileName))
            {
                msgResult=MessageBox.Show("Serverda Böyle Bir Dosya Zaten Var...\nÜzerine Yazılsın mı ?"
                    ,"Dosya Çakışmasi Oluştu"
                    ,MessageBoxButtons.YesNo
                    ,MessageBoxIcon.Error);
                if (msgResult.Equals(DialogResult.Yes))
                {
                    ftp.Write(FTPWriteFileName, FTPWriteFilePath);
                    MessageBox.Show("Dosya Yazma İşlemi Başarılı Bir Şekilde Yapıldı...","Dosya Yazma",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Dosya Yazma İşlemi Kullanıcı Tarafından İptal Edildi.", "Dosya Yazma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                ftp.Write(FTPWriteFileName, FTPWriteFilePath);
                MessageBox.Show("Dosya Yazma İşlemi Başarılı Bir Şekilde Yapıldı...", "Dosya Yazma", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            List<string> directories = new List<string>();
            List<string> trimitem = new List<string>();
            directories =ftp.directoryListSimple("aaa").ToList();
            foreach (var item in directories)
            {
               trimitem=item.Split('.').ToList();
                if (trimitem.Count==1)
                {
                    //Bu Bir Dosyadır.
                    /*
                    treeView1.Nodes.Add(trimitem[0].ToString(), trimitem[0].ToString(),)
                   */
                }
                else if (trimitem.Count==2 && trimitem.Last()=="txt")
                {
                    //Bu bir metin belgesidir.

                }
                else if (trimitem.Count == 2 && trimitem.Last() == "html")
                {
                    //Bu bir web sayfasıdır.

                }
                else
                {
                    
                }

            }









        }
    }
}
