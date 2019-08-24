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
	public partial class Form3 : Form
	{
		private int _amount = 0;

		public int Amount { get { return _amount; } }

		public Form3()
		{
			InitializeComponent();
			img_wallet.Image = Images.Wallet;
			img_half.Image = Images.Half_dollar;
			img_penny.Image = Images.US_One_Cent_Obv;
			img_nickel.Image = Images.US_Nickel_2013_Rev;
			img_dime.Image = Images.dime;
			img_dollar.Image = Images._1dollar;

			img_wallet.AllowDrop = true;
		}

		private void Coin_MouseDown(object sender, MouseEventArgs e)
		{
			img_dime.DoDragDrop(sender, DragDropEffects.Move);
		}

		private void Img_wallet_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.AllowedEffect;
		}

		private void Img_wallet_DragDrop(object sender, DragEventArgs e)
		{
			var img = (PictureBox)e.Data.GetData(typeof(PictureBox));
			_amount += Int32.Parse((string)img.Tag);
			Console.WriteLine("{0}", _amount);
			img.Dispose();

			if (_amount >= 500)
			{
				this.Close();
			}
		}
	}
}
