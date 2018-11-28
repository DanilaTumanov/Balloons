using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace InputSystem
{
    public class InputMobileController : InputController
    {
        /// <summary>
        /// Чувствительность отклонения пальцев при двойном свайпе. Содержит значение, 
        /// меньше которого должно быть скалярное произведение векторов движения пальцев, 
        /// чтобы свайп засчитался.
        /// </summary>
        const float DOUBLE_SWIPE_FINGER_DEVIATION = 0.5f;

        /// <summary>
        /// Допустимое минимальное отклонение пальцев от вертикальной или горизонтальной оси для засчитывания свайпа
        /// (для рассчета с помощью скалярного умножения вектора движения пальца на вектор перпендикулярной оси)
        /// 0 - 90 градусов
        /// 1 - 0 градусов
        /// </summary>
        const float SWIPE_DEVIATION = 0.3f;

        /// <summary>
        /// Мертвая зона свайпа - расстояние, смещение на которое на защитывается за свайп
        /// </summary>
        const float SWIPE_DEADZONE = 4f;


        private float _distanceBtwFinger;
        private float _delta;

        // Флаг для определения происходит ли какое-то движение связанное со свайпом - свайп, двойной свайп, зум и тд
        private bool _isAnySwipe = false;
        // Флаг показывает, происходит ли поворот
        private bool _isRotation = false;
        // Флаг показывает, происходит ли двойной свайп
        private bool _isDoubleSwipe = false;

        private Vector3 _rotationIdentity = Vector3.zero;

        private int[] _pointerIds = new int[5];



        protected override Vector3 GetDeltaTouchPosition()
        {
            if (Input.touchCount == 1)
                return Input.GetTouch(0).deltaPosition;
            else
                return Vector3.zero;
        }

               

        protected override bool GetIsTap()
        {
            return GetIsScreenTap() && !_blockInput;
        }

        protected override bool GetIsTapDown()
        {   
            return GetIsScreenTapDown() && !_blockInput;
        }

        protected override bool GetIsTapUp()
        {
            var screenTapUp = GetIsScreenTapUp();
            var isTapUp = screenTapUp && !_blockInputLate && !_isAnySwipe;

            if (screenTapUp)
            {
                _isAnySwipe = false;
            }

            return isTapUp;
        }


        protected override bool GetIsScreenTap()
        {
            return Input.touchCount == 1;
        }

        protected override bool GetIsScreenTapDown()
        {
            return Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began;
        }

        protected override bool GetIsScreenTapUp()
        {
            bool isAllTapUp = Input.touchCount > 0;
            bool isAnyTapUp = false;
            int i = 0;
            int touchCount = Input.touchCount;
            
            while ((isAllTapUp || !isAnyTapUp) && i < touchCount)
            {
                bool currentFingerUp = Input.GetTouch(i).phase == TouchPhase.Ended;

                if (!currentFingerUp)
                    isAllTapUp = false;
                if (currentFingerUp)
                    isAnyTapUp = true;

                i++;
            }
            
            if (isAnyTapUp)
            {
                _isDoubleSwipe = false;
                _isRotation = false;
            }

            return isAllTapUp;
        }


        protected override Vector3 GetTouchPosition()
        {
            if (Input.touchCount == 1)
                return Input.GetTouch(0).position;
            else
                return Vector3.zero;
        }




        protected override float GetHorizontalSwipe()
        {
            return GetSwipe(Vector2.right, Vector2.up, _screenWidth);
        }

        protected override float GetVerticalSwipe()
        {
            return GetSwipe(Vector2.up, Vector2.right, _screenHeight);
        }

        protected override float GetHorizontalDoubleSwipe()
        {
            return GetDoubleSwipe(Vector2.right, Vector2.up, _screenWidth);
        }

        protected override float GetVerticalDoubleSwipe()
        {
            return GetDoubleSwipe(Vector2.up, Vector2.right, _screenHeight);
        }

        protected override float GetZoom()
        {
            float dist = 0;

            if (Input.touchCount != 2)
                return dist;

            var touch1 = Input.GetTouch(0);
            var touch2 = Input.GetTouch(1);
            var delta1 = touch1.deltaPosition;
            var delta2 = touch2.deltaPosition;
            var delta1norm = delta1.normalized;
            var delta2norm = delta2.normalized;
            
            var deadZone = _isAnySwipe ? 0 : SWIPE_DEADZONE;
            var oldPosition1 = touch1.position - delta1;
            var oldPosition2 = touch2.position - delta2;

            var deltaFingers = (touch1.position - touch2.position).magnitude;
            var oldDeltaFingers = (oldPosition1 - oldPosition2).magnitude;

            var deltaMag = deltaFingers - oldDeltaFingers;
            

            if (Mathf.Abs(deltaMag) > deadZone
                && Vector2.Dot(delta1norm, delta2norm) < 0)
            {
                dist = deltaMag / _maxSideSize;
                _isAnySwipe = true;
            }

            return dist;
        }






        private float GetSwipe(Vector2 tangentAxis, Vector2 normalAxis, int axisMax)
        {
            float dist = 0;
            
            if (Input.touchCount != 1)
                return dist;

            var delta = Input.GetTouch(0).deltaPosition;
            var deltaMag = delta.magnitude;
            var deltaNorm = delta.normalized;
            var deadZone = _isAnySwipe ? 0 : SWIPE_DEADZONE;

            if (deltaMag > deadZone 
                && Mathf.Abs(Vector2.Dot(deltaNorm, normalAxis)) < SWIPE_DEVIATION)
            {
                dist = Mathf.Sign(Vector2.Dot(deltaNorm, tangentAxis)) * deltaMag / axisMax;
                _isAnySwipe = true;
            }

            return dist;
        }


        private float GetDoubleSwipe(Vector2 tangentAxis, Vector2 normalAxis, int axisMax)
        {
            float dist = 0;

            if (Input.touchCount != 2)
                return dist;

            var delta1 = Input.GetTouch(0).deltaPosition;
            var delta2 = Input.GetTouch(1).deltaPosition;
            var delta1norm = delta1.normalized;
            var delta2norm = delta2.normalized;
            var deltaMag = delta1.magnitude;
            var deadZone = _isAnySwipe ? 0 : SWIPE_DEADZONE;

            if (deltaMag > deadZone
                && Vector2.Dot(delta1norm, delta2norm) > DOUBLE_SWIPE_FINGER_DEVIATION
                && Mathf.Abs(Vector2.Dot(delta1norm, normalAxis)) < SWIPE_DEVIATION
                && !_isRotation)
            {
                dist = Mathf.Sign(Vector2.Dot(delta1norm, tangentAxis)) * deltaMag / axisMax;
                _isAnySwipe = true;
                _isDoubleSwipe = true;
            }

            return dist;
        }






        protected override float GetRotation()
        {
            float angle = 0;

            if (Input.touchCount != 2)
            {
                _rotationIdentity = Vector3.zero;
                return angle;
            }

            var touch1 = Input.GetTouch(0);
            var touch2 = Input.GetTouch(1);
            var fingersVector = touch2.position - touch1.position;
            var deadZone = _isRotation ? 0 : SWIPE_DEADZONE;

            if (_rotationIdentity != Vector3.zero
                && (touch1.deltaPosition.magnitude + touch2.deltaPosition.magnitude) > deadZone
                && Vector2.Dot(touch1.deltaPosition.normalized, touch2.deltaPosition.normalized) < 0
                && !_isDoubleSwipe)
            {
                angle = Mathf.Sign(Vector3.Cross(_rotationIdentity, fingersVector).z) * Vector3.Angle(_rotationIdentity, fingersVector);
                _isRotation = true;
                _isAnySwipe = true;
            }

            _rotationIdentity = fingersVector;

            return angle;
        }





        protected override int[] GetPointerIds()
        {
            var touchCount = Input.touchCount;

            // TODO: Магическая константа, убрать.
            for (var i = 0; i < 5; i++)
            {
                if(i < touchCount)
                {
                    _pointerIds[i] = Input.GetTouch(i).fingerId;
                }
                else
                {
                    _pointerIds[i] = -1;
                }
            }

            return _pointerIds;
        }


        

        protected override Vector2 GetSwipeVector()
        {
            if (Input.touchCount != 1)
                return Vector2.zero;

            var swipeVector = Input.GetTouch(0).deltaPosition;

            swipeVector.x /= _screenWidth;
            swipeVector.y /= _screenHeight;

            return swipeVector;
        }



        protected override bool GetEscape()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
