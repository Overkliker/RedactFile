using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedactFile
{
    public class JsonSruct
    {
        public string firstName;
        public string lastName;
        public string gender;
        public int age;
        public Adress address = new Adress();
        public Numbers phoneNumbers = new Numbers();
    }
}
