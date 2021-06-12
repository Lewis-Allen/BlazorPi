using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPi.Services
{
    public interface IGameStateService
    {
        bool GameOver { get; }
        bool Animating { get; }
        int Score { get; }
        int HighScore { get; }
        int Lives { get; }
        bool LosingLife { get; }

        Task Initialize();
        Task ChooseDigit(char digit);
        Task ResetGame();
    }
}
