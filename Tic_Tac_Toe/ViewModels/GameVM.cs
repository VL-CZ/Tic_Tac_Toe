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
        public GameBoard GameBoard { get; }
        public GameTimer GameTimer { get; }

        public GameVM()
        {
            GameBoard = new GameBoard(25);
            GameTimer = new GameTimer();
        }
    }
}
