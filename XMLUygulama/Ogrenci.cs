using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLUygulama
{
    internal class Ogrenci
    {
        public string Numara { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }

        public override string ToString()
        {
            return this.Numara + "-" + this.AdSoyad.ToUpper();
        }
    }
}
