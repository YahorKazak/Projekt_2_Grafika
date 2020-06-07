using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_2
{
    public partial class Form1 : Form
    {
        Pętla pet = new Pętla();
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBoxFirst_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            E3D e3D = new E3D(pictureBoxFirst);
            pet = new Pętla();
            pet.Load(e3D);
            pet.Start();
        }
    }
}
