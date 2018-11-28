using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    /// <summary>
    /// Настройки шарика
    /// </summary>
    [CreateAssetMenu(fileName = "BalloonsSettings", menuName = "Settings/Balloons", order = 1)]
    public class BalloonsSettings : ScriptableObject
    {

        [Tooltip("Минимальный (x) и максимальный (y) масштаб шариков")]
        public Vector3 scaleBounds = new Vector2(0.5f, 1.5f);

        [Tooltip("Базовая скорость шарика с единичным масштабом")]
        public float baseSpeed = 10;

        [Tooltip("Зависимость скорости от масштаба")]
        public float speedOverScale = -1;

        [Tooltip("Увеличение скорости со временем")]
        public float speedOverTime = 1;

        [Tooltip("Базовое количество очков за шарик")]
        public int baseScores = 200;

        [Tooltip("Зависимость количества очков от масштаба")]
        public int scoresOverScale = -200;

        [Tooltip("Порядок округления числа очков")]
        public int scoresRoundOrder = 10;
        

        [Tooltip("Префаб шарика")]
        public Balloon balloonPrefab;

        public Sprite[] balloonSprites;

    }

}
