using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Balloons
{

    [DisallowMultipleComponent]
    public class HUDController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Поле вывода очков")]
        private Text _scoresField;

        [SerializeField]
        [Tooltip("Поле вывода времени")]
        private Text _timeField;


        
        public void SetScores(int scores)
        {
            _scoresField.text = scores.ToString();
        }
     
        
        public void SetTime(int time)
        {
            _timeField.text = time.ToString();
        }

    }

}
