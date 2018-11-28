using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Balloons
{

    [DisallowMultipleComponent]
    public class Main : MonoBehaviour
    {

        public static SceneController SceneController { get; private set; }
        public static UIController UIController { get; private set; }


        private void Awake()
        {
            SceneController = FindObjectOfType<SceneController>();
            UIController = FindObjectOfType<UIController>();
        }

    }

}
