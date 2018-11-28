using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InputSystem
{
    public static class InputManager
    {

        public static InputController Controller { get; private set; }

        private static GameObject _inputController;
        


        static InputManager()
        {
            ResetInputController();
        }

        


        private static void ResetInputController()
        {
            _inputController = new GameObject("InputController");
            Controller = SetController(_inputController);
        }

        
        /// <summary>Возвращает контроллер ввода в зависимости от платформы, которая используется пользователем.</summary>
        private static InputController SetController(GameObject GO)
        {
            
#if UNITY_STANDALONE_WIN || UNITY_EDITOR

            return GO.AddComponent<InputWinController>();

#elif UNITY_ANDROID || UNITY_IOS

            return GO.AddComponent<InputMobileController>();

#endif
        }

    }
    
}