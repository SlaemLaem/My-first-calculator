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
	public partial class TicTacDoom : Form
	{
		Label _lbl_text = new Label();
		Image _img_p1 = null;
		Image _img_p2 = null;
		Business.GameState.TicTacDoom _gamestate = null;

		public TicTacDoom()
		{
			InitializeComponent();
		}

		private void TicTacDoom_Load(object sender, EventArgs e)
		{
			img_TopLeft.Visible = false;
			img_TopCenter.Visible = false;
			img_TopRight.Visible = false;

			_lbl_text.Text = "Choose your character";
			_lbl_text.Dock = DockStyle.Fill;
			_lbl_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			_lbl_text.TextAlign = ContentAlignment.MiddleCenter;
			tbl_TicTac.Controls.Add(_lbl_text, 0, 0);
			tbl_TicTac.SetColumnSpan(_lbl_text, 3);

			img_MiddleLeft.Image = Images.X;
			img_center.Image = Images.Cross;
			img_MiddleRight.Image = Images.trident;

			img_MiddleLeft.Click += Player_Select_Click;
			img_center.Click += Player_Select_Click;
			img_MiddleRight.Click += Player_Select_Click;
		}

		private void Player_Select_Click(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(PictureBox))
			{
				var img = (PictureBox)sender;
				if (_img_p1 == null)
				{
					_img_p1 = img.Image;

					img_MiddleLeft.Image = Images.Circle;
					img_center.Image = Images.SummoningCircle;
					img_MiddleRight.Image = Images.Flat_Earth;
				}
				else if (_img_p2 == null)
				{
					_img_p2 = img.Image;

					tbl_TicTac.Controls.Remove(_lbl_text);
					tbl_TicTac.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

					img_TopLeft.Visible = true;
					img_TopCenter.Visible = true;
					img_TopRight.Visible = true;

					img_MiddleLeft.Image = null;
					img_center.Image = null;
					img_MiddleRight.Image = null;

					img_MiddleLeft.Click -= Player_Select_Click;
					img_center.Click -= Player_Select_Click;
					img_MiddleRight.Click -= Player_Select_Click;

					img_TopLeft.Click += Player_Move_Click;
					img_TopCenter.Click += Player_Move_Click;
					img_TopRight.Click += Player_Move_Click;
					img_MiddleLeft.Click += Player_Move_Click;
					img_center.Click += Player_Move_Click;
					img_MiddleRight.Click += Player_Move_Click;
					img_BottomLeft.Click += Player_Move_Click;
					img_BottomCenter.Click += Player_Move_Click;
					img_BottomRight.Click += Player_Move_Click;

					_gamestate = new Business.GameState.TicTacDoom();
				}
			}
		}

		private void Player_Move_Click(object sender, EventArgs e)
		{
			Console.WriteLine(_gamestate.ToString());

			if (sender.GetType() == typeof(PictureBox))
			{
				var img = (PictureBox)sender;
				switch (_gamestate.CurrentPlayer)
				{
					case Business.GameState.TicTacDoom.Player.Player1: img.Image = _img_p1; break;
					case Business.GameState.TicTacDoom.Player.Player2: img.Image = _img_p2; break;
					default: return;
				}

				if (img.Tag.GetType() == typeof(Point))
				{
					var point = (Point)img.Tag;

					_gamestate.SetPlayer(point.X, point.Y);
					var winner = _gamestate.GetWinner();

					if (winner != null)
					{
						img_MiddleLeft.Visible = false;
						img_center.Visible = false;
						img_MiddleRight.Visible = false;

						_lbl_text.Text = String.Format("{0} has won!\nGood job!", winner);
						_lbl_text.Click += Game_Over_Click;
						tbl_TicTac.Controls.Add(_lbl_text, 0, 1);
						tbl_TicTac.SetColumnSpan(_lbl_text, 3);

						img_TopLeft.Click -= Player_Move_Click;
						img_TopCenter.Click -= Player_Move_Click;
						img_TopRight.Click -= Player_Move_Click;
						img_MiddleLeft.Click -= Player_Move_Click;
						img_center.Click -= Player_Move_Click;
						img_MiddleRight.Click -= Player_Move_Click;
						img_BottomLeft.Click -= Player_Move_Click;
						img_BottomCenter.Click -= Player_Move_Click;
						img_BottomRight.Click -= Player_Move_Click;
					}
					else if (_gamestate.CellsRemaining == 0)
					{
						img_MiddleLeft.Visible = false;
						img_center.Visible = false;
						img_MiddleRight.Visible = false;

						_lbl_text.Text = "You're dead, you're mom's dead, you're dad's dead, you're friends are fricking dead, you're pets are being skinned alive!";
						_lbl_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
						_lbl_text.Click += Game_Over_Click;
						tbl_TicTac.Controls.Add(_lbl_text, 0, 1);
						tbl_TicTac.SetColumnSpan(_lbl_text, 3);

						img_TopLeft.Click -= Player_Move_Click;
						img_TopCenter.Click -= Player_Move_Click;
						img_TopRight.Click -= Player_Move_Click;
						img_MiddleLeft.Click -= Player_Move_Click;
						img_center.Click -= Player_Move_Click;
						img_MiddleRight.Click -= Player_Move_Click;
						img_BottomLeft.Click -= Player_Move_Click;
						img_BottomCenter.Click -= Player_Move_Click;
						img_BottomRight.Click -= Player_Move_Click;
					}
				}
			}

			Console.WriteLine(_gamestate.ToString());
		}

		private void Game_Over_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
