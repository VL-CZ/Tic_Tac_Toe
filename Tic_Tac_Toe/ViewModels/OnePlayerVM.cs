using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.ViewModels
{
    internal class OnePlayerVM : GameVM
    {
        private readonly char botCharacter = 'X';
        public GameBot GameBot { get; }

        public OnePlayerVM() : base()
        {
            GameBot = new GameBot(GameBoard, botCharacter);
        }
    }
}