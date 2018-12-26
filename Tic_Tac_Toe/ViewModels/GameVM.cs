using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.ViewModels
{
    class GameVM : BaseVM
    {
        protected readonly int gameBoardSize = 25;
        public GameBoard GameBoard { get; }
        public GameTimer GameTimer { get; }

        public GameVM()
        {
            GameBoard = new GameBoard(gameBoardSize);
            GameTimer = new GameTimer();
        }
    }
}
