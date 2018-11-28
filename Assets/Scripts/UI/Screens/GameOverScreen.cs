using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Balloons
{

    public class GameOverScreen : UIScreen
    {

        [SerializeField]
        private Text _scores;


        public void SetScores(int scores)
        {
            _scores.text = scores.ToString();
        }

    }

}
