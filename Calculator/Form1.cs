﻿using System;
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
			SaveValue();
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
				SaveValue();
				operation = '-';
			}
		}

		private void Btn_equal_Click(object sender, EventArgs e)
		{
			SaveValue();
			operation = ' ';
		}

		private void SaveValue()
		{
			//double tmp = 0;
			if (double.TryParse(txt_input.Text, out double tmp))
			{
				if (value == null || operation == ' ')
				{
					value = tmp;
					txt_display.Text = value.ToString();
				}
				else
				{
					PerformCalculation(tmp);
				}
			}
			Console.WriteLine(value);
			txt_input.Text = "";
		}

		private void PerformCalculation(double input)
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
				SaveValue();
				operation = 'x';
			}
		}

		private void Btn_divide_Click(object sender, EventArgs e)
		{
			SaveValue();
			operation = '/';
		}

		private void Btn_backspace_Click(object sender, EventArgs e)
		{
			txt_input.Text = "";
		}

		private void Btn_c_Click(object sender, EventArgs e)
		{
			txt_display.Text = "";
			txt_input.Text = "";
		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{
			DonutBox.Image = Images.Donut;
		}

		private void Txt_Premium_Click(object sender, EventArgs e)
		{
			ShowPremiumWindow();
		}

		private void ShowPremiumWindow()
		{
			var popup = new Form3();
			popup.ShowDialog();
			var amount = popup.Amount;
			popup.Dispose();

			if (amount == 500)
			{
				txt_Premium.Dispose();
			}
			else if (amount > 500)
			{
				txt_Premium.BackColor = Color.Red;
				txt_Premium.ForeColor = Color.Black;
				txt_Premium.Text = "That's too much!\nClick to try again.";
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			var popup = new Form2();
			popup.ShowDialog();
			var status = popup.Status;
			popup.Dispose();
			if (status == null) { this.Dispose(); }
			else if (status == false) { ShowPremiumWindow(); }
		}
	}
}
