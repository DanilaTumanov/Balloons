using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Balloons
{

    /// <summary>
    /// Контроллер HUD
    /// </summary>
    [DisallowMultipleComponent]
    public class HUDController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Поле вывода очков")]
        private Text _scoresField;

        [SerializeField]
        [Tooltip("Поле вывода времени")]
        private Text _timeField;


        /// <summary>
        /// Установить количество очков
        /// </summary>
        /// <param name="scores"></param>
        public void SetScores(int scores)
        {
            _scoresField.text = scores.ToString();
        }
     

        /// <summary>
        /// Установаить количество оставшегося времени
        /// </summary>
        /// <param name="time"></param>
        public void SetTime(int time)
        {
            _timeField.text = time.ToString();
        }

    }

}
