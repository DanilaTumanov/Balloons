using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Balloons
{

    /// <summary>
    /// Экран конца игры
    /// </summary>
    public class GameOverScreen : UIScreen
    {

        [SerializeField]
        [Tooltip("Текстовое поле для вывода количества очков")]
        private Text _scores;

        
        /// <summary>
        /// Установить количество очков
        /// </summary>
        /// <param name="scores"></param>
        public void SetScores(int scores)
        {
            _scores.text = scores.ToString();
        }

    }

}
