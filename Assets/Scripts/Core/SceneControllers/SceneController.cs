using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(ShootController))]
    public abstract class SceneController : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Количество времени, отведенное на игру в секундах")]
        private float _gameTime = 60;


        protected int _scores;
        protected float _remainingTime;
        protected bool _paused = false;
        protected bool _gameOver = false;



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





        public void AddScores(int scores)
        {
            _scores += scores;
        }



        public virtual void RestartGame()
        {
            StartGame();
        }


        public void ExitGame()
        {
            Application.Quit();
        }





        protected void ProcessInput()
        {
            if (InputManager.Controller.Escape)
            {
                Pause(!_paused);
            }
        }


        private void ProcessTime()
        {
            _remainingTime -= Time.deltaTime;
            GameTime += Time.deltaTime;

            if (_remainingTime < 0)
            {
                GameOver();
            }
        }


        private void UpdateHUD()
        {
            Main.UIController.HUD.SetScores(_scores);
            Main.UIController.HUD.SetTime((int)_remainingTime);
        }


        protected virtual void StartGame()
        {
            ResetScene();
        }


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


        protected virtual void GameOver()
        {
            _gameOver = true;
            Main.UIController.Screens.ShowGameOverScreen(_scores);
        }



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