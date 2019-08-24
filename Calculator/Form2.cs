using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
	public partial class Form2 : Form
	{
		private bool _status = false;

		public bool Status { get { return _status; } }

		public Form2()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			_status = true;
			this.Close();
		}

		private void Button3_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
