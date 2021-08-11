using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sftptest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //https://csharp.hotexamples.com/examples/Renci.SshNet/SftpClient/CreateDirectory/php-sftpclient-createdirectory-method-examples.html
        private void btnDownload_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Guid.NewGuid());

            var ci = new ConnectionInfo("192.168.0.133", "loccitance", new PasswordAuthenticationMethod("loccitance", "0152"));

            using (var sftp = new SftpClient(ci))
            {
                // SFTP 서버 연결
                sftp.Connect();

                // 현재 디렉토리 내용 표시
                foreach (SftpFile f in sftp.ListDirectory("."))
                {
                    Console.WriteLine(f.Name);
                }

                // SFTP 다운로드
                //using (var outfile = File.Create("HelloWorld.txt"))
                //{
                //    sftp.DownloadFile("./HelloWorld.txt", outfile);
                //}

                // SFTP 업로드
                using (var infile = File.Open("HelloWorld.txt", FileMode.Open))
                {
                    if(!sftp.Exists("/RECV/20210810"))
                    {
                        sftp.CreateDirectory("/RECV/20210810");
                    }
                    //sftp.ChangeDirectory(ConstFields.TEMP_PRINT_DIRECTORY);
                    sftp.UploadFile(infile, "/RECV/20210810/HelloWorld.txt");
                }

                sftp.Disconnect();
            }
        }
    }
}
