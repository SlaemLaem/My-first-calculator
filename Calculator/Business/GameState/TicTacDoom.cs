using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.GameState
{

	public class TicTacDoom
	{
		public enum Player { Player1, Player2 }

		private Player?[,] _cells = null;
		private Player? _current_player = null;
		private int _cells_remaining;

		public int CellsRemaining { get { return _cells_remaining; } }
		public Player? CurrentPlayer { get { return _current_player; } }

		public TicTacDoom()
		{
			var x = 3;
			var y = 3;
			_cells = new Player?[x, y];
			_cells_remaining = x * y;
			_current_player = Player.Player1;
		}

		public bool SetPlayer(int x,int y)
		{
			bool result = false;
			if (_cells[x, y] == null && _current_player != null)
			{
				_cells[x, y] = _current_player;
				_cells_remaining -= 1;

				switch (_current_player)
				{
					case Player.Player1: _current_player = Player.Player2; break;
					case Player.Player2: _current_player = Player.Player1; break;
				}

				result = true;
			}
			return result;
		}

		public Player? GetWinner() {
			Player? winner = null;

			// don't check first 5 moves
			if (_cells_remaining < 5)
			{
				// check horizontal lines
				if (_cells[0, 0] != null && _cells[0, 0] == _cells[0, 1] && _cells[0, 1] == _cells[0, 2]) { winner = _cells[0, 0]; }
				else if (_cells[1, 0] != null && _cells[1, 0] == _cells[1, 1] && _cells[1, 1] == _cells[1, 2]) { winner = _cells[1, 0]; }
				else if (_cells[2, 0] != null && _cells[2, 0] == _cells[2, 1] && _cells[2, 1] == _cells[2, 2]) { winner = _cells[2, 0]; }

				// check vertical lines
				else if (_cells[0, 0] != null && _cells[0, 0] == _cells[1, 0] && _cells[1, 0] == _cells[2, 0]) { winner = _cells[0, 0]; }
				else if (_cells[0, 1] != null && _cells[0, 1] == _cells[1, 1] && _cells[1, 1] == _cells[2, 1]) { winner = _cells[0, 1]; }
				else if (_cells[0, 2] != null && _cells[0, 2] == _cells[1, 2] && _cells[1, 2] == _cells[2, 2]) { winner = _cells[0, 2]; }

				// check diagonal lines
				else if (_cells[0, 0] != null && _cells[0, 0] == _cells[1, 1] && _cells[1, 1] == _cells[2, 2]) { winner = _cells[0, 0]; }
				else if (_cells[2, 0] != null && _cells[2, 0] == _cells[1, 1] && _cells[1, 1] == _cells[0, 2]) { winner = _cells[2, 0]; }
			}

			return winner;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					switch (_cells[x, y])
					{
						case Player.Player1: sb.Append("X"); break;
						case Player.Player2: sb.Append("O"); break;
						default: sb.Append("#"); break;
					}
				}
				sb.Append("\n");
			}

			return sb.ToString();
		}
	}
}
