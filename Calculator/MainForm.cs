﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Calculator
{
	public partial class MainForm : Form
	{
		private double? _value = null;
		private char _operation = ' ';
		private bool _premium = false;

		public MainForm()
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
			txt_input.Focus(); 
			if (sender.GetType() == typeof(Button))
			{
				txt_input.Text += ((Button)sender).Text;
			}
		}

		private void Btn_plus_Click(object sender, EventArgs e)
		{
			txt_input.Focus();
			//if (txt_input.Text == "-")
			//{
			//	txt_input.Text = "";
			//}
			//else
			//{
			SaveValue();
			_operation = '+';
			//}

		}

		private void Btn_minus_Click(object sender, EventArgs e)
		{
			txt_input.Focus();
			if (txt_input.Text == "" && (_operation != ' ' || txt_display.Text == ""))
			{
				txt_input.Text = "-";
			}
			else
			{
				SaveValue();
				_operation = '-';
			}
		}

		private void Btn_equal_Click(object sender, EventArgs e)
		{
			txt_input.Focus();
			SaveValue();
			_operation = ' ';
		}

		private void SaveValue()
		{
			if (double.TryParse(txt_input.Text, out double tmp))
			{
				txt_input.Text = "";
				if (_value == null || _operation == ' ')
				{
					_value = tmp;
					txt_display.Text = _value.ToString();
				}
				else
				{
					PerformCalculation(tmp);
				}

				if (_value == 58008)
				{
					if (_premium == false) { txt_display.Text = "Censored"; }
					var caption = (_premium) ? "Premium feature" : "You pervert!!!";
					var text = (_premium) ? "Ok, here you go.\n\n        ( . ) ( . )\n          )  .  (" : "Find your porn elsewhere...";
					MessageBox.Show(text, caption, MessageBoxButtons.OK);
				}
				else if (_value == 1134)
				{
					var popup = new TicTacDoom();
					popup.ShowDialog();
				}
				else if (_value == 2005)
				{
					if (_premium == false) { txt_display.Text = "You need premium"; }
					var caption = (_premium) ? "Premium feature" : "Your close";
					var text = (_premium) ? "My birthday?" : "Try premium";
					MessageBox.Show(text, caption, MessageBoxButtons.OK);
				}
				else if (_value == 2606)
				{
					if (_premium == false) { txt_display.Text = "You need premium"; }
					var caption = (_premium) ? "Premium feature" : "Your close";
					var text = (_premium) ? "Did you just beat the game?" : "Try premium";
					MessageBox.Show(text, caption, MessageBoxButtons.OK);
				}
			}
			Console.WriteLine(_value);
		}

		private void PerformCalculation(double input)
		{
			switch (_operation)
			{
				case '+':
					if (_value == 9 && input == 10)
					{
						_value = 21;
					}
					else
					{
						_value = (_value ?? 0) + input;
					}
					break;
				case '-':
					_value = (_value ?? 0) - input;
					break;
				case 'x':
					if (_value == 5 && input == 9)
					{
						_value = 42;
					}
					else
					{
						_value = (_value ?? 0) * input;
					}
					break;
				case '/':
					_value = (_value ?? 0) / input;
					break;
				default:
					_value = input;
					break;
			}

			txt_display.Text = _value.ToString();
		}

		private void Btn_x_Click(object sender, EventArgs e)
		{
			txt_input.Focus();
			//if (txt_input.Text != "")
			{
				SaveValue();
				_operation = 'x';
			}
		}

		private void Btn_divide_Click(object sender, EventArgs e)
		{
			txt_input.Focus();
			SaveValue();
			_operation = '/';
		}

		private void Btn_backspace_Click(object sender, EventArgs e)
		{
			txt_input.Focus();
			txt_input.Text = "";
		}

		private void Btn_c_Click(object sender, EventArgs e)
		{
			txt_input.Focus();
			_value = null;
			txt_display.Text = "";
			txt_input.Text = "";
		}

		private void Img_Donut_Click(object sender, EventArgs e)
		{
			img_Donut.Image = (img_Donut.Image == null) ? Images.Donut : null;
		}

		private void Txt_Premium_Click(object sender, EventArgs e)
		{
			ShowPremiumWindow();
		}

		private void ShowPremiumWindow()
		{
			var popup = new WalletForm();
			popup.ShowDialog();
			var amount = popup.Amount;
			popup.Dispose();

			if (amount == 500)
			{
				SavePremiumLicense();
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
			LoadPremiumLicense();
			if (_premium == false)
			{
				var popup = new StartupMessageForm();
				popup.ShowDialog();
				var status = popup.Status;
				popup.Dispose();
				if (status == null) { this.Dispose(); }
				else if (status == false) { ShowPremiumWindow(); }
			}
		}

		private void SetPremium()
		{
			_premium = true;
			txt_Premium.Dispose();
			this.Text += " - PREMIUM";
		}

		private void SavePremiumLicense()
		{
			SetPremium();

			var currenttime = DateTime.Now.AddMonths(1).ToString();
			var bytes = System.Text.Encoding.UTF8.GetBytes(currenttime);
			var license = System.Convert.ToBase64String(bytes);
			var docPath = Directory.GetCurrentDirectory();

			using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "License.txt")))
			{
				outputFile.WriteLine(license);
			}
		}

		private void LoadPremiumLicense()
		{
			var docPath = Directory.GetCurrentDirectory();
			var filecontent = System.IO.File.ReadAllText(Path.Combine(docPath, "License.txt"));
			var bytes = System.Convert.FromBase64String(filecontent);
			filecontent = System.Text.Encoding.UTF8.GetString(bytes);
			if (DateTime.TryParse(filecontent, out DateTime license_expiration))
			{
				if (license_expiration >= DateTime.Now) { SetPremium(); }
			}
		}

		private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			Console.WriteLine(e.KeyChar);
			switch (e.KeyChar)
			{
				case '1': btn_1.PerformClick(); break;
				case '2': btn_2.PerformClick(); break;
				case '3': btn_3.PerformClick(); break;
				case '4': btn_4.PerformClick(); break;
				case '5': btn_5.PerformClick(); break;
				case '6': btn_6.PerformClick(); break;
				case '7': btn_7.PerformClick(); break;
				case '8': btn_8.PerformClick(); break;
				case '9': btn_9.PerformClick(); break;
				case '0': btn_0.PerformClick(); break;
				case '+': btn_plus.PerformClick(); break;
				case '-': btn_minus.PerformClick(); break;
				case '/': btn_divide.PerformClick(); break;
				case 'x': btn_x.PerformClick(); break;
				case (char)Keys.Return: btn_equal.PerformClick(); break;
				case (char)Keys.Back: btn_ce.PerformClick(); break;
				case (char)Keys.Escape: btn_c.PerformClick(); break;
			}
		}
	}
}
