using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Customer:Person
    {
        private string _ID;
        private string _NIF;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string NIF
        {
            get { return _NIF; }
            set { _NIF = value; }
        }


        public Customer(string _ID, string _NIF, string _email, string _fname, string _lname, string _phoneNo)
        {
            this._ID = _ID;
            this._NIF = _NIF;
            this.Email = _email;
            this.Fname = _fname;
            this.Lname = _lname;
            this.PhoneNo = _phoneNo;
        }

        public Customer() { 
        
        }
    }
}
