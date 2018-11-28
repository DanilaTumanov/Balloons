using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

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



        public override void RestartGame()
        {
            _balloonSpawner.SetEnabled(false);
            _balloonSpawner.ClearInstances();

            base.RestartGame();
        }



        protected override void StartGame()
        {
            base.StartGame();

            _balloonSpawner.SetEnabled(true);
        }


        protected override void GameOver()
        {
            _balloonSpawner.SetEnabled(false);
            _balloonSpawner.ClearInstances();

            base.GameOver();
        }

    }

}


