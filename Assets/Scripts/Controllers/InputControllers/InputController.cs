using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputSystem
{
    public abstract class InputController: MonoBehaviour
    {
        
        protected int _screenWidth;
        protected int _screenHeight;
        protected int _maxSideSize;

        
        /// <summary>
        /// Возвращает true, если пользователь удерживает палец на экране 
        /// (Учитываются элементы, блокирующие нажатия, такие, как UI, в таком случае нажатие не засчитывается)
        /// </summary>
        public bool IsTap { get; protected set; }

        /// <summary>
        /// Возвращает true если пользователь поставил палец на экран.
        /// (Учитываются элементы, блокирующие нажатия, такие, как UI, в таком случае нажатие не засчитывается)
        /// </summary>
        public bool IsTapDown { get; protected set; }

        /// <summary>
        /// Возвращает true если пользователь убрал палец с экрана.
        /// (Учитываются элементы, блокирующие нажатия, такие, как UI, в таком случае нажатие не засчитывается)
        /// </summary>
        public bool IsTapUp { get; protected set; }

        /// <summary>
        /// Возвращает true, если пользователь удерживает палец на экране
        /// </summary>
        /// <returns></returns>
        public bool IsScreenTap { get; protected set; }

        /// <summary>
        /// Возвращает true если пользователь поставил палец на экран.
        /// </summary>
        /// <returns></returns>
        public bool IsScreenTapDown { get; protected set; }

        /// <summary>
        /// Возвращает true, если пользователь убрал палец с экрана.
        /// </summary>
        /// <returns></returns>
        public bool IsScreenTapUp { get; protected set; }

        /// <summary>
        /// Возвращает вектор движения пальца влево-вправо, вверх-вниз.
        /// </summary>
        public Vector3 DeltaTouchPosition { get; protected set; }

        /// <summary>
        /// Возвращает точку нажатия по экрану.
        /// </summary>
        public Vector3 TouchPosition { get; protected set; }

        /// <summary>
        /// Возвращает точку нажатия по экрану на прошлом кадре.
        /// Нужно для определения позиции при отпускании пальца
        /// </summary>
        public Vector3 OldTouchPosition { get; protected set; }

        /// <summary>
        /// Возвращает расстояние горизонтального свайпа
        /// Положительное - вправо
        /// Отрицательное - влево
        /// </summary>
        public float HorizontalSwipe { get; protected set; }

        /// <summary>
        /// Возвращает расстояние вертикального свайпа
        /// Положительное - вверх
        /// Отрицательное - вниз
        /// </summary>
        public float VerticalSwipe { get; protected set; }

        /// <summary>
        /// Возвращает расстояние горизонтального свайпа двумя пальцами
        /// Положительное - вправо
        /// Отрицательное - влево
        /// </summary>
        public float HorizontalDoubleSwipe { get; protected set; }

        /// <summary>
        /// Возвращает расстояние вертикального свайпа двумя пальцами
        /// Положительное - вверх
        /// Отрицательное - вниз
        /// </summary>
        /// <returns></returns>
        public float VerticalDoubleSwipe { get; protected set; }

        /// <summary>
        /// Возвращает уровень зума (положительный - разведение пальцев, отрицательный - сведение)
        /// </summary>
        /// <returns></returns>
        public float Zoom { get; protected set; }

        /// <summary>
        /// Возвращает угол поворота, заданный жестом поворота в текущем кадре
        /// </summary>
        public float Rotation { get; protected set; }

        /// <summary>
        /// Вектор, показывающий направление свайпа
        /// </summary>
        public Vector2 SwipeVector { get; protected set; }


        public bool Escape { get; protected set; }




        protected List<CachedMethod> _cachedMethodInfo;

        protected bool _blockInput = false;

        // Флаг блокирования ввода, идущий с отставанием в кадр. 
        // Снятие пальца не может проверить на текущем кадре находился ли палец в том или ином месте, поэтому ориентируемся на предыдущий кадр
        protected bool _blockInputLate = false;

        // Признак принудительной блокировки ввода
        protected bool _forceBlockInput = false;






        private void Awake()
        {
            SetScreenBounds();
            _cachedMethodInfo = GetCachedMethodsInfo();
        }


        private void Update()
        {
            _blockInput = IsInputBlocked();            

            CacheInput();
        }


        private void LateUpdate()
        {
            _blockInputLate = IsInputBlocked();
            OldTouchPosition = TouchPosition;
        }





        private void SetScreenBounds()
        {
            _screenHeight = Screen.height;
            _screenWidth = Screen.width;
            _maxSideSize = Math.Max(_screenHeight, _screenWidth);
        }




        /// <summary>
        /// Заблокировать ввод
        /// </summary>
        public void BlockInput()
        {
            _forceBlockInput = true;
        }

        /// <summary>
        /// Разблокировать ввод
        /// </summary>
        public void UnblockInput()
        {
            _forceBlockInput = false;
        }



        private List<CachedMethod> GetCachedMethodsInfo()
        {
            Type thisType = GetType();
            MethodInfo[] methods = thisType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            List<CachedMethod> CachedMethodsInfo = new List<CachedMethod>();

            foreach(var method in methods)
            {
                if(method.Name.IndexOf("Get") == 0)
                {
                    var prop = thisType.GetProperty(method.Name.Substring(3));
                    if(prop != null)
                    {
                        CachedMethodsInfo.Add(new CachedMethod()
                        {
                            property = prop,
                            method = method
                        });
                    }
                }
            }

            return CachedMethodsInfo;
        }


        private void CacheInput()
        {
            foreach(var cachedMethodInfo in _cachedMethodInfo)
            {
                cachedMethodInfo.property.SetValue(this, cachedMethodInfo.method.Invoke(this, null), null);
            }
        }






        protected abstract bool GetIsTap();

        protected abstract bool GetIsTapDown();

        protected abstract bool GetIsTapUp();

        protected abstract bool GetIsScreenTap();

        protected abstract bool GetIsScreenTapDown();

        protected abstract bool GetIsScreenTapUp();

        protected abstract Vector3 GetDeltaTouchPosition();
        
        protected abstract Vector3 GetTouchPosition();

        protected abstract float GetHorizontalSwipe();

        protected abstract float GetVerticalSwipe();

        protected abstract float GetHorizontalDoubleSwipe();

        protected abstract float GetVerticalDoubleSwipe();

        protected abstract float GetZoom();

        protected abstract float GetRotation();

        protected abstract Vector2 GetSwipeVector();

        protected abstract bool GetEscape();



        protected abstract int[] GetPointerIds();



        

        

        protected bool IsInputBlocked()
        {
            return _forceBlockInput || HandleUIInteraction();
        }

        private bool HandleUIInteraction()
        {
            int[] pointerIds = GetPointerIds();
            int i = 0;
            bool isUIInteraction = false;

            if(pointerIds.Length > 0)
            {
                if(pointerIds[0] >= 0)
                {
                    do
                    {
                        isUIInteraction = IsUIInteraction(pointerIds[i++]);
                    }
                    while (!isUIInteraction && pointerIds[i] >= 0 && i < pointerIds.Length);
                }
            }
            else
            {
                isUIInteraction = IsUIInteraction();
            }

            return isUIInteraction;
        }



        protected virtual bool IsUIInteraction(int pointerId)
        {
            if (EventSystem.current == null)
                return false;

            return pointerId >= 0 ? EventSystem.current.IsPointerOverGameObject(pointerId) : EventSystem.current.IsPointerOverGameObject();
        }

        protected virtual bool IsUIInteraction()
        {
            return IsUIInteraction(-1);
        }

    }


    public struct CachedMethod
    {
        public PropertyInfo property;
        public MethodInfo method;
    }

}
