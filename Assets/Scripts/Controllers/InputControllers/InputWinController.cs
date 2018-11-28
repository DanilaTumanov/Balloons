using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace InputSystem
{
    public class InputWinController : InputController
    {
        private float _scaleMultiply = 5f;
        private int[] _pointerIds = new int[0];

        protected override Vector3 GetDeltaTouchPosition()
        {
            if (Input.GetMouseButton((int)MouseButton.LeftMouse))
                return Input.mousePosition;
            else
                return Vector3.zero;
        }

        protected override bool GetIsTap()
        {
            return Input.GetMouseButton((int)MouseButton.LeftMouse) && !_blockInput;
        }

        protected override bool GetIsTapDown()
        {
            return Input.GetMouseButtonDown((int)MouseButton.LeftMouse) && !_blockInput;
        }

        protected override bool GetIsTapUp()
        {
            return Input.GetMouseButtonUp((int)MouseButton.LeftMouse) && !_blockInputLate;
        }

        protected override bool GetIsScreenTap()
        {
            return Input.GetMouseButton((int)MouseButton.LeftMouse);
        }

        protected override bool GetIsScreenTapDown()
        {
            return Input.GetMouseButtonDown((int)MouseButton.LeftMouse);
        }

        protected override bool GetIsScreenTapUp()
        {
            return Input.GetMouseButtonUp((int)MouseButton.LeftMouse);
        }

        protected override Vector3 GetTouchPosition()
        {
            return GetDeltaTouchPosition();
        }

        protected override float GetHorizontalSwipe()
        {
            return Input.GetAxis("Horizontal") * 0.1f;
        }

        protected override float GetVerticalSwipe()
        {
            return Input.GetAxis("Vertical") * 0.1f;
        }

        protected override float GetHorizontalDoubleSwipe()
        {
            return 0; //Input.GetAxis("Horizontal");
        }

        protected override float GetVerticalDoubleSwipe()
        {
            return 0; //Input.GetAxis("Vertical");
        }

        protected override float GetZoom()
        {
            return -Input.GetAxis("Mouse ScrollWheel") * 50;
        }

        protected override int[] GetPointerIds()
        {
            return _pointerIds;
        }

        protected override float GetRotation()
        {
            var angle = 0;

            if (Input.GetKey(KeyCode.Minus))
                angle -= 1;
            if (Input.GetKey(KeyCode.Equals))
                angle += 1;

            return angle;
        }

        protected override Vector2 GetSwipeVector()
        {
            return new Vector2(GetHorizontalSwipe(), GetVerticalSwipe());
        }

        protected override bool GetEscape()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
