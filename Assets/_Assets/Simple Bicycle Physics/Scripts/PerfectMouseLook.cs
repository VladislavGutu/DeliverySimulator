using UnityEngine;
using System.Collections;

namespace SBPScripts
{
    public class PerfectMouseLook : MonoBehaviour
    {
        Vector2 _mouseAbsolute;
        Vector2 _smoothMouse;

        public Vector2 clampInDegrees = new Vector2(360, 180);
        public Vector2 sensitivity = new Vector2(2, 2);
        public Vector2 smoothing = new Vector2(3, 3);
        public Vector2 targetDirection;
        public Vector2 targetCharacterDirection;
        [HideInInspector] public bool movement;
        public bool autoRotate;

        void Start()
        {
            // Set target direction to the camera's initial orientation.
            targetDirection = transform.localRotation.eulerAngles;
        }

        void LateUpdate()
        {
            // Allow the script to clamp based on a desired target value.
            var targetOrientation = Quaternion.Euler(targetDirection);
            var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

            // Get raw mouse input for a cleaner reading on more sensitive mice.
            Vector2 mouseDelta;
#if UNITY_SWITCH
            if (NintendoInput.isEditorInputActiv)
            {
                mouseDelta = new Vector2(NintendoInput.InputNpadAxis(NintendoInput.NpadAxis.RightHorizontal),
                    NintendoInput.InputNpadAxis(NintendoInput.NpadAxis.RightVertical));
            }
            else
                mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
#else
            mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
#endif

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta,
                new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            // _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
            // _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);
            _smoothMouse.x = mouseDelta.x;
            _smoothMouse.y = mouseDelta.y;

            // Find the absolute mouse movement value from point zero.
            _mouseAbsolute += _smoothMouse;
            if (_smoothMouse == new Vector2(0, 0) && autoRotate)
            {
                targetDirection = transform.localRotation.eulerAngles;
                _mouseAbsolute = new Vector2(0, 0);
                movement = false;
            }
            else
            {
                movement = true;
                // Clamp and apply the local x value first, so as not to be affected by world transforms.
                if (clampInDegrees.x < 360)
                    _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

                // Then clamp and apply the global y value.
                if (clampInDegrees.y < 360)
                    _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

                transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) *
                                          targetOrientation;


                var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
                transform.localRotation *= yRotation;
            }
        }
    }
}