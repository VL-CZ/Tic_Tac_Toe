using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.ViewModels
{
    class TwoPlayerVM : BaseVM
    {
        public GameBoard GameBoard { get; set; } = new GameBoard(20);
    }
}
