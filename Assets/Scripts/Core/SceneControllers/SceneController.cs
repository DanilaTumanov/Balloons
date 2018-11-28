using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Абстрактный класс контроллера сцены
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ShootController))]
    public abstract class SceneController : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Количество времени, отведенное на игру в секундах")]
        private float _gameTime = 60;



        // Очки
        protected int _scores;

        // Оставшееся время игры
        protected float _remainingTime;

        // Признак паузы
        protected bool _paused = false;

        ///Признак конца игры
        protected bool _gameOver = false;


        /// <summary>
        /// Время, прошедшее с начала раунда
        /// </summary>
        public float GameTime { get; private set; }



        private void Start()
        {
            StartGame();
        }



        private void Update()
        {
            if (_gameOver)
                return;

            ProcessInput();
            ProcessTime();
            UpdateHUD();
        }




        /// <summary>
        /// Добавить очки
        /// </summary>
        /// <param name="scores">Количество очков</param>
        public void AddScores(int scores)
        {
            _scores += scores;
        }


        /// <summary>
        /// Перезапуск игры
        /// </summary>
        public virtual void RestartGame()
        {
            StartGame();
        }


        /// <summary>
        /// Выход из игры
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }





        /// <summary>
        /// Обработка ввода
        /// </summary>
        protected void ProcessInput()
        {
            if (InputManager.Controller.Escape)
            {
                Pause(!_paused);
            }
        }


        /// <summary>
        /// Обработка времени раунда
        /// </summary>
        private void ProcessTime()
        {
            _remainingTime -= Time.deltaTime;
            GameTime += Time.deltaTime;

            // Если время истекло, то завершаем игру
            if (_remainingTime < 0)
            {
                GameOver();
            }
        }


        /// <summary>
        /// Обновление пользовательского интерфейса
        /// </summary>
        private void UpdateHUD()
        {
            Main.UIController.HUD.SetScores(_scores);
            Main.UIController.HUD.SetTime((int)_remainingTime);
        }


        /// <summary>
        /// Начать игру
        /// </summary>
        protected virtual void StartGame()
        {
            ResetScene();
        }


        /// <summary>
        /// Поставить игру на паузу, или снять с нее
        /// </summary>
        /// <param name="paused">true - поставить на паузу, false - снять с паузы</param>
        protected virtual void Pause(bool paused)
        {
            _paused = paused;

            if (_paused)
            {
                Time.timeScale = 0;
                Main.UIController.Screens.ShowPauseScreen();
            }
            else
            {
                Time.timeScale = 1;
                Main.UIController.Screens.HidePauseScreen();
            }
        }


        /// <summary>
        /// Обработка завершения раунда
        /// </summary>
        protected virtual void GameOver()
        {
            _gameOver = true;
            Main.UIController.Screens.ShowGameOverScreen(_scores);
        }


        /// <summary>
        /// Сброс параметров сцены
        /// </summary>
        private void ResetScene()
        {
            _scores = 0;
            _remainingTime = _gameTime;
            GameTime = 0;
            _gameOver = false;

            Main.UIController.Screens.HideGameOverScreen();
            Main.UIController.Screens.HidePauseScreen();

            Pause(false);
        }

    }

}