using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;


namespace Balloons
{

    public class BalloonSpawner : SpawnerType<Balloon>
    {

        /// <summary>
        /// Примерное максимальное количество шариков на сцене. Нужно для контроля порядка шариков в слоях.
        /// </summary>
        const int ESTIMATED_MAX_BALLOONS = 100;


        [SerializeField]
        [Tooltip("Настройки шариков")]
        private BalloonsSettings _balloonsSettings;

        

        
        private int _orderInLayer;

        private Vector2 _horizontalBounds;
        private Vector2 _verticalBounds;





        private void Awake()
        {
            SetBounds();
        }




        protected override Balloon GetPrefab()
        {
            return _balloonsSettings.balloonPrefab;
        }

               

        private Vector3 GetSpawnCoords(Balloon balloon)
        {
            var bounds = GetHorizontalBounds(balloon);

            return new Vector3(Random.Range(bounds.x, bounds.y), transform.position.y, 0);
        }



        private void SetBounds()
        {
            var cam = Camera.main;
            var halfHeight = cam.orthographicSize;
            var halfWidth = cam.orthographicSize * cam.aspect;

            _horizontalBounds = new Vector2(cam.transform.position.x - halfWidth, cam.transform.position.x + halfWidth);
            _verticalBounds = new Vector2(cam.transform.position.y - halfHeight, cam.transform.position.y + halfHeight);
        }




        private Vector2 GetHorizontalBounds(Balloon balloon)
        {
            var extent = balloon.GetHorizontalExtent();
            return new Vector2(_horizontalBounds.x + extent, _horizontalBounds.y - extent);
        }


        private float GetHorizontalBound(Balloon balloon)
        {
            return _verticalBounds.y + balloon.GetVertiacalExtent();
        }



        private int GetScores(Balloon balloon)
        {
            var pureScores = _balloonsSettings.baseScores + balloon.transform.localScale.y * _balloonsSettings.scoresOverScale;
            return pureScores.RoundToOrder(_balloonsSettings.scoresRoundOrder);
        }



        protected override void InitInstance(Balloon balloon)
        {
            var scale = Random.Range(_balloonsSettings.scaleBounds.x, _balloonsSettings.scaleBounds.y);

            balloon.SetSprite(_balloonsSettings.balloonSprites[Random.Range(0, _balloonsSettings.balloonSprites.Length)]);

            balloon.transform.localScale = Vector3.one * scale;

            balloon.speed = 
                _balloonsSettings.baseSpeed
                + Main.SceneController.GameTime * _balloonsSettings.speedOverTime
                + scale * _balloonsSettings.speedOverScale;
            
            balloon.verticalLimit = GetHorizontalBound(balloon);

            balloon.transform.position = GetSpawnCoords(balloon);

            balloon.Scores = GetScores(balloon);


            balloon.GetComponent<SpriteRenderer>().sortingOrder = _orderInLayer;
            _orderInLayer = ++_orderInLayer % ESTIMATED_MAX_BALLOONS;
        }

    }

}
