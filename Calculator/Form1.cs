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
	public partial class Form1 : Form
	{
		private double? value = null;
		private char operation = ' ';

		public Form1()
		{
			InitializeComponent();
			txt_input.Text = "";
			txt_display.Text = "";
		}

		//private void Btn_1_Click(object sender, EventArgs e)
		//{
		//	txt_display.Text = "1";
		//}

		private void Btn_Number_Click(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(Button))
			{
				txt_input.Text += ((Button)sender).Text;
			}
		}

		private void Btn_plus_Click(object sender, EventArgs e)
		{
			//if (txt_input.Text == "-")
			//{
			//	txt_input.Text = "";
			//}
			//else
			//{
				save_value();
				operation = '+';
			//}
		}

		private void Btn_minus_Click(object sender, EventArgs e)
		{
			if (txt_input.Text == "" && (operation != ' ' || txt_display.Text == ""))
			{
				txt_input.Text = "-";
			}
			else
			{
				save_value();
				operation = '-';
			}
		}

		private void Btn_equal_Click(object sender, EventArgs e)
		{
			save_value();
			operation = ' ';
		}

		private void save_value()
		{
			double tmp = 0;
			if (double.TryParse(txt_input.Text, out tmp))
			{
				if (value == null || operation == ' ')
				{
					value = tmp;
					txt_display.Text = value.ToString();
				}
				else
				{
					perform_calculation(tmp);
				}
			}
			Console.WriteLine(value);
			txt_input.Text = "";
		}

		private void perform_calculation(double input)
		{
			switch (operation)
			{
				case '+':
					value = (value ?? 0) + input;
					break;
				case '-':
					value = (value ?? 0) - input;
					break;
				case 'x':
					value = (value ?? 0) * input;
					break;
				case '/':
					value = (value ?? 0) / input;
					break;
				default:
					value = input;
					break;
			}
			txt_display.Text = value.ToString();
		}

		private void Btn_x_Click(object sender, EventArgs e)
		{
			//if (txt_input.Text != "")
			{
				save_value();
				operation = 'x';
			}
		}

		private void Btn_divide_Click(object sender, EventArgs e)
		{
			save_value();
			operation = '/';
		}
	}
}
