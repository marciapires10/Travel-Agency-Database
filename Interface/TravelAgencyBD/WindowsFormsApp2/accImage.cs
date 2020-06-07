using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp2
{
    class accImage : PictureBox
    {
        private int _ID;
        private Image _img;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Image Img
        {
            get { return _img; }
            set { _img = value; }
        }
    }
}
