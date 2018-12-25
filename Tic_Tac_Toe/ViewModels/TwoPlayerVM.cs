using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.ViewModels
{
    internal class TwoPlayerVM : BaseVM
    {
        public GameBoard GameBoard { get; } 
        public GameTimer GameTimer { get; } 

        public TwoPlayerVM()
        {
            GameBoard = new GameBoard(25);
            GameTimer = new GameTimer();
        }
    }
}