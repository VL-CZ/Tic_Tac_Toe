using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.ViewModels
{
    internal class TwoPlayerVM : BaseVM
    {
        public GameBoard GameBoard { get; set; } = new GameBoard(25);
    }
}