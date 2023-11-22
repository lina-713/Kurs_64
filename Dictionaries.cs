using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_64
{
    internal class Dictionaries
    {
        private string _ikey;
        public string IKey
        {
            get
            {
                return _ikey;
            }
            set
            {
                _ikey = value;
            }
        }
        private string _ivalue;
        public string IValue
        {
            get
            {
                return _ivalue;
            }
            set
            {
                _ivalue = value;
            }
        }
        public override string ToString()
        {
            return _ivalue;
        }
    }
}
