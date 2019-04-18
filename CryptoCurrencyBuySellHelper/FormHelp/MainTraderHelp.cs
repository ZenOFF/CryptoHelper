using System;
using System.Drawing;
using System.Windows.Forms;

namespace NoviceCryptoTraderAdvisor.FormHelp
{
    public partial class MainTraderHelp : Form
    {
        public MainTraderHelp()
        {
            InitializeComponent();
        }
        //закрытие
        private void HelpImagePictureBox_ClickClose(object sender, EventArgs e)
        {
            Close();
        }
        //загрузка подходящего изображения исходя из языка
        public void Image_Load(string PathImage)
        {
            HelpImagePictureBox1.Image = Image.FromFile(PathImage);
            HelpImagePictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            Size = HelpImagePictureBox1.Size;
        }
    }
}