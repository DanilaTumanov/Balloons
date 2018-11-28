using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Контроллер экранов
    /// </summary>
    [DisallowMultipleComponent]
    public class ScreenController : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Экран паузы")]
        private UIScreen _pauseScreen;

        [SerializeField]
        [Tooltip("Экран конца игры")]
        private GameOverScreen _gameOverScreen;


        
        /// <summary>
        /// Показать экран конца игры
        /// </summary>
        /// <param name="scores"></param>
        public void ShowGameOverScreen(int scores)
        {
            _gameOverScreen.Show();
            _gameOverScreen.SetScores(scores);
        }

        /// <summary>
        /// Скрыть экран конца игры
        /// </summary>
        public void HideGameOverScreen()
        {
            _gameOverScreen.Hide();
        }



        /// <summary>
        /// Показать экран паузы
        /// </summary>
        public void ShowPauseScreen()
        {
            _pauseScreen.Show();
        }

        /// <summary>
        /// Скрыть экран паузы
        /// </summary>
        public void HidePauseScreen()
        {
            _pauseScreen.Hide();
        }


    }

}


