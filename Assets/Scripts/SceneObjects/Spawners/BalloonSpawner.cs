using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;


namespace Balloons
{

    /// <summary>
    /// Спаунер шариков
    /// </summary>
    public class BalloonSpawner : SpawnerType<Balloon>
    {

        /// <summary>
        /// Примерное максимальное количество шариков на сцене. Нужно для контроля порядка шариков в слоях.
        /// </summary>
        const int ESTIMATED_MAX_BALLOONS = 100;
        

        [SerializeField]
        [Tooltip("Настройки шариков")]
        private BalloonsSettings _balloonsSettings;

        

        // Счетчик порядка шариков в слое отрисовки, чтобы избежать мигания при одинаковых слоях
        private int _orderInLayer;

        // Горизонтальные границы спауна
        private Vector2 _horizontalBounds;

        // Вертикальные границы
        private Vector2 _verticalBounds;





        private void Awake()
        {
            SetBounds();
        }



        /// <summary>
        /// Получить префаб объекта
        /// </summary>
        /// <returns></returns>
        protected override Balloon GetPrefab()
        {
            return _balloonsSettings.balloonPrefab;
        }

               
        /// <summary>
        /// Получить координаты спауна конкретного шарика (зависит от его размера)
        /// </summary>
        /// <param name="balloon">Шарик</param>
        /// <returns></returns>
        private Vector3 GetSpawnCoords(Balloon balloon)
        {
            var bounds = GetHorizontalBounds(balloon);

            return new Vector3(Random.Range(bounds.x, bounds.y), transform.position.y, 0);
        }



        /// <summary>
        /// Рассчитать и установить границы спауна
        /// </summary>
        private void SetBounds()
        {
            var cam = Camera.main;
            var halfHeight = cam.orthographicSize;
            var halfWidth = cam.orthographicSize * cam.aspect;

            _horizontalBounds = new Vector2(cam.transform.position.x - halfWidth, cam.transform.position.x + halfWidth);
            _verticalBounds = new Vector2(cam.transform.position.y - halfHeight, cam.transform.position.y + halfHeight);
        }



        /// <summary>
        /// Получить горизонтальные границы спауна шарика
        /// </summary>
        /// <param name="balloon">Шарик</param>
        /// <returns></returns>
        private Vector2 GetHorizontalBounds(Balloon balloon)
        {
            var extent = balloon.GetHorizontalExtent();
            return new Vector2(_horizontalBounds.x + extent, _horizontalBounds.y - extent);
        }


        /// <summary>
        /// Получить вертикальные границы полета шарика
        /// </summary>
        /// <param name="balloon">Шарик</param>
        /// <returns></returns>
        private float GetHorizontalBound(Balloon balloon)
        {
            return _verticalBounds.y + balloon.GetVertiacalExtent();
        }



        /// <summary>
        /// Рассчитать очки для шарика
        /// </summary>
        /// <param name="balloon">Шарик</param>
        /// <returns></returns>
        private int GetScores(Balloon balloon)
        {
            var pureScores = _balloonsSettings.baseScores + balloon.transform.localScale.y * _balloonsSettings.scoresOverScale;
            return pureScores.RoundToOrder(_balloonsSettings.scoresRoundOrder);
        }


        /// <summary>
        /// Инициализация нового объекта
        /// </summary>
        /// <param name="balloon"></param>
        protected override void InitInstance(Balloon balloon)
        {
            // Генерируем случаный масштаб
            var scale = Random.Range(_balloonsSettings.scaleBounds.x, _balloonsSettings.scaleBounds.y);

            // Устанавливаем масштаб
            balloon.transform.localScale = Vector3.one * scale;

            // Устанавливаем случаный спрайт (цвет)
            balloon.SetSprite(_balloonsSettings.balloonSprites[Random.Range(0, _balloonsSettings.balloonSprites.Length)]);

            // Рассчитываем скорость, исходя из базовой скорости, зависимости ее от скейла и от времени раунда
            balloon.speed = 
                _balloonsSettings.baseSpeed
                + Main.SceneController.GameTime * _balloonsSettings.speedOverTime
                + scale * _balloonsSettings.speedOverScale;
            
            // Устанавливаем верткальный предел, после которого шарик уничтожится
            balloon.verticalLimit = GetHorizontalBound(balloon);

            // Устанавливаем случаную позицию
            balloon.transform.position = GetSpawnCoords(balloon);

            // Рассчитывае количество очков, которые получит игров сбив текущий шарик
            balloon.Scores = GetScores(balloon);

            // Устанавливаем порядок сортировки, чтобы шарики не мигали
            balloon.GetComponent<SpriteRenderer>().sortingOrder = _orderInLayer;
            _orderInLayer = ++_orderInLayer % ESTIMATED_MAX_BALLOONS;
        }

    }

}
