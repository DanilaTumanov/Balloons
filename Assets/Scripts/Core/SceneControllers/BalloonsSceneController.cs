using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Контроллер сцены сбивания шариков
    /// </summary>
    public class BalloonsSceneController : SceneController
    {

        [SerializeField]
        [Tooltip("Экземпляр спаунера шариков")]
        private Spawner _balloonSpawner;



        private void Awake()
        {
            if (_balloonSpawner == null)
                _balloonSpawner = FindObjectOfType<BalloonSpawner>();
        }


        /// <summary>
        /// Перезапуск игры
        /// </summary>
        public override void RestartGame()
        {
            _balloonSpawner.SetEnabled(false);
            _balloonSpawner.ClearInstances();

            base.RestartGame();
        }


        /// <summary>
        /// Запуск игры
        /// </summary>
        protected override void StartGame()
        {
            base.StartGame();

            _balloonSpawner.SetEnabled(true);
        }


        /// <summary>
        /// Конец игры
        /// </summary>
        protected override void GameOver()
        {
            _balloonSpawner.SetEnabled(false);
            _balloonSpawner.ClearInstances();

            base.GameOver();
        }

    }

}


