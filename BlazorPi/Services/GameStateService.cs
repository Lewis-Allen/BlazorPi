using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPi.Services
{
    public class GameStateService : IGameStateService
    {
        public int Score { get; private set; }
        public int HighScore { get; private set; }
        public bool GameOver { get; private set; }
        public bool Animating { get; private set; }
        public int Lives { get; private set; } = 3;
        public bool LosingLife { get; private set; }

        private readonly ILocalStorageService _localStorage;

        public GameStateService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task Initialize()
        {
            if(await _localStorage.ContainKeyAsync(Constants.LS_HIGHSCORE))
            {
                HighScore = await _localStorage.GetItemAsync<int>(Constants.LS_HIGHSCORE);
            }
        }

        public async Task ChooseDigit(char number)
        {
            if (GameOver)
                return; 

            char current = Constants.PI[Score];

            if(number == current)
            {
                Animating = true;

                await Task.Delay(75);

                Score++;
                Animating = false;

                if (Score > HighScore)
                {
                    HighScore = Score;
                    await _localStorage.SetItemAsync(Constants.LS_HIGHSCORE, HighScore);
                }


            }
            else
            {
                LosingLife = true;

                await Task.Delay(375);

                Lives--;
                LosingLife = false;

                if(Lives == 0)
                    GameOver = true;
            }
        }

        public async Task ResetGame()
        {
            Score = 0;
            Lives = 3;
            GameOver = false;
        }
    }
}
