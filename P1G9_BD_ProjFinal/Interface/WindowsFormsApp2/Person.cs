using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Person
    {
        private string _email;
        private string _fname;
        private string _lname;
        private string _phoneNo;

        public string Email
        {
            get { return _email;  }
            set { _email = value; }
        }

        public string Fname
        {
            get { return _fname; }
            set { _fname = value; }
        }

        public string Lname
        {
            get { return _lname; }
            set { _lname = value; }
        }

        public string PhoneNo
        {
            get { return _phoneNo; }
            set { _phoneNo = value; }
        }

        public Person (string _email, string _fname, string _lname, string _phoneNo) : base()
        {
            this._email = _email;
            this._fname = _fname;
            this._lname = _lname;
            this._phoneNo = _phoneNo;
        }

        public Person (string _fname, string _lname): base()
        {
            this._fname = _fname;
            this._lname = _lname;
        }

        public Person(): base()
        {

        }

        public override String ToString()
        {
            return _fname + " " + _lname;
        }

    }
}
