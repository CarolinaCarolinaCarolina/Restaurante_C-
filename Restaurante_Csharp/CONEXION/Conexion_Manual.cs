using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Sistema_para_restaurante_en_CSHARP_codigo369.CONEXION
{
    public partial class CONEXION_MANUAL : Form
    {
        private Restaurante_Csharp.LIBRERIAS.Encriptacion aes = new Restaurante_Csharp.LIBRERIAS.Encriptacion();
        public CONEXION_MANUAL()
        {
            InitializeComponent();
        }
        public void SavetoXML(Object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }
        string dbcnString;
        public void ReadfromXML()
        {
        try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load("ConnectionString.xml");
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                txtCnString.Text = (aes.Decrypt(dbcnString, Restaurante_Csharp.LIBRERIAS.Desencriptar.appPwdUnique, int.Parse("256")));
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {

            }
        }
        private void CONEXION_MANUAL_Load(object sender, EventArgs e)
        {
            ReadfromXML();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SavetoXML(aes.Encrypt(txtCnString.Text, Restaurante_Csharp.LIBRERIAS.Desencriptar.appPwdUnique, int.Parse("256")));
        }
    }
}
