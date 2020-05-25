using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Accommodation
    {
        private int _ID;
        private string _name;
        private string _image;
        private string _description;
        private decimal _price;
        private string _location;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public Accommodation(): base()
        {

        }

        public override String ToString()
        {
            return _ID + " " + _name + " " + _price + " " + _location;
        }
    }
}
