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
using System.Xml;

namespace XMLUygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        XmlDocument xDoc;
        string dosya = "../../Kurs.xml";
        private void Form1_Load(object sender, EventArgs e)
        {
            xDoc = new XmlDocument();

            if (File.Exists(dosya)) DosyaOlustur();           //Eğer elimizde xml dosyası yoksa

            OgrenciYukle();
        }

        private void DosyaOlustur()
        {
            XmlDeclaration xDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xDec);             //Xddoc içinde alt ağaç tanımlıyor

            XmlElement ogrenciler = xDoc.CreateElement("ogrenciler");
            xDoc.AppendChild(ogrenciler);

            //<Ogrenci>

            #region Ogrenci-1
            XmlElement ogrenci1 = xDoc.CreateElement("ogrenci");

            XmlAttribute no = xDoc.CreateAttribute("no");
            no.Value = "1";
            ogrenci1.Attributes.Append(no);

            XmlElement adSoyad1 = xDoc.CreateElement("adSoyad");
            adSoyad1.InnerText = "Ali Veli";
            ogrenci1.AppendChild(adSoyad1);

            XmlElement telefon1 = xDoc.CreateElement("telefon");
            telefon1.InnerText = "555 555 55";
            ogrenci1.AppendChild(telefon1);

            ogrenciler.AppendChild(ogrenci1);
            #endregion
            #region Ogrenci-2
            XmlElement ogrenci2 = xDoc.CreateElement("ogrenci");

            XmlAttribute no2 = xDoc.CreateAttribute("no");
            no2.Value = "2";
            ogrenci2.Attributes.Append(no2);

            XmlElement adSoyad2 = xDoc.CreateElement("adSoyad");
            adSoyad2.InnerText = "Eda Koç";
            ogrenci2.AppendChild(adSoyad2);

            XmlElement telefon2 = xDoc.CreateElement("telefon");
            telefon2.InnerText = "333 33 33";
            ogrenci2.AppendChild(telefon2);

            ogrenciler.AppendChild(ogrenci2);
            #endregion
            #region Ogrenci-3
            XmlElement ogrenci3 = xDoc.CreateElement("ogrenci");

            XmlAttribute no3 = xDoc.CreateAttribute("no");
            no3.Value = "3";
            ogrenci3.Attributes.Append(no3);

            XmlElement adSoyad3 = xDoc.CreateElement("adSoyad");
            adSoyad3.InnerText = "Cem Ayaz";
            ogrenci3.AppendChild(adSoyad3);

            XmlElement telefon3 = xDoc.CreateElement("telefon");
            telefon3.InnerText = "555 77 77";
            ogrenci3.AppendChild(telefon3);

            ogrenciler.AppendChild(ogrenci3);
            #endregion

            xDoc.Save(dosya);
        }

        private void OgrenciYukle()
        {
            lbxOgrenci.Items.Clear();
            xDoc.Load(dosya);

            XmlNode ogrenciler = xDoc.SelectSingleNode("ogrenciler");

            XmlNodeList liste = ogrenciler.SelectNodes("ogrenci");

            Ogrenci ogrenci;

            foreach (XmlNode item in liste)
            {
                ogrenci = new Ogrenci()
                {
                    AdSoyad=item.SelectSingleNode("adSoyad").InnerText,      //InnerText = xml içindeki yazı Ali Veli gibi
                    Telefon=item.SelectSingleNode("telefon").InnerText,
                    Numara = item.Attributes["no"].Value
                };

                lbxOgrenci.Items.Add(ogrenci);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Ogrenci yeni = new Ogrenci()
            {
                AdSoyad = txtAdSoyad.Text,
                Telefon = txtTelefon.Text,
                Numara = txtNumara.Text
            };
            //lbxOgrenci.Items.Add(yeni);


            XmlNode ogrenciler = xDoc.SelectSingleNode("ogrenciler");

            XmlElement ogr = xDoc.CreateElement("ogrenci");

            XmlAttribute no = xDoc.CreateAttribute("no");
            no.Value = yeni.Numara;
            ogr.Attributes.Append(no);

            XmlElement adSoyad = xDoc.CreateElement("adSoyad");
            adSoyad.InnerText = yeni.AdSoyad;
            ogr.AppendChild(adSoyad);

            XmlElement telefon = xDoc.CreateElement("telefon");
            telefon.InnerText = yeni.Telefon;
            ogr.AppendChild(telefon);

            ogrenciler.AppendChild(ogr);

            xDoc.Save(dosya);
            MessageBox.Show("Ogrenci Eklendi!");
            Temizle();
            OgrenciYukle();
        }

        private void Temizle()
        {
            txtAdSoyad.Text = txtNumara.Text = txtTelefon.Text = string.Empty;
            btnSil.Visible = false;
            lbxOgrenci.SelectedItem = null;
        }

        private void lbxOgrenci_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ogrenci selected =(Ogrenci)lbxOgrenci.SelectedItem;
            if (selected == null)
            {
                return;
            }
            txtAdSoyad.Text = selected.AdSoyad;
            txtNumara.Text= selected.Numara;
            txtTelefon.Text= selected.Telefon; 
            btnSil.Visible = true;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Ogrenci silinecek = lbxOgrenci.SelectedItem as Ogrenci; 
            if (silinecek == null)
            {
                return;
            }
            lbxOgrenci.Items.Clear();
            xDoc.Load(dosya);

            XmlNode ogrenciler = xDoc.SelectSingleNode("ogrenciler");

            XmlNodeList liste = ogrenciler.SelectNodes("ogrenci");

            Ogrenci ogrenci;

            foreach (XmlNode item in liste)
            {
                ogrenci = new Ogrenci()
                {
                    AdSoyad = item.SelectSingleNode("adSoyad").InnerText,      //InnerText = xml içindeki yazı Ali Veli gibi
                    Telefon = item.SelectSingleNode("telefon").InnerText,
                    Numara = item.Attributes["no"].Value
                };
                if (silinecek.Numara==ogrenci.Numara)
                {
                    item.ParentNode.RemoveChild(item);
                }
                
            }
            xDoc.Save(dosya);
            MessageBox.Show("Kayıt silindi!");
            Temizle();
            OgrenciYukle();
        }
    }
}
