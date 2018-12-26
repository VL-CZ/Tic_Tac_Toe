using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.ViewModels
{
    internal class OnePlayerVM : GameVM
    {
        public GameBot GameBot { get; }

        public OnePlayerVM() : base()
        {
            GameBot = new GameBot(GameBoard);
        }
    }
}