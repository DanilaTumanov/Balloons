using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    [DisallowMultipleComponent]
    public class ScreenController : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Экран паузы")]
        private UIScreen _pauseScreen;

        [SerializeField]
        [Tooltip("Экран конца игры")]
        private GameOverScreen _gameOverScreen;




        public void ShowGameOverScreen(int scores)
        {
            _gameOverScreen.Show();
            _gameOverScreen.SetScores(scores);
        }

        public void HideGameOverScreen()
        {
            _gameOverScreen.Hide();
        }




        public void ShowPauseScreen()
        {
            _pauseScreen.Show();
        }

        public void HidePauseScreen()
        {
            _pauseScreen.Hide();
        }


    }

}


