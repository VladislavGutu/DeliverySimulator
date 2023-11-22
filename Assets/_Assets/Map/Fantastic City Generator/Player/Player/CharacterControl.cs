using System.Collections;
using System.Collections.Generic;
using SBPScripts;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public float speed = 10.0f;
    
    public float sensitivity = 100f;
    float xRotation = 0f;

    private Transform cam;

    private CharacterController charController;

    void Start()
    {
    
        Cursor.lockState = CursorLockMode.Locked;

        charController = GetComponent<CharacterController>();

        cam = transform.Find("Camera");

    }
    
    void Update()
    {

        CameraMovement();

         Vector3 move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));
        
         charController.SimpleMove((Vector3.ClampMagnitude(move, 1.0f) * (Input.GetKey(KeyCode.LeftShift) ? speed * 1.6f : speed)));


    }

    void CameraMovement()
    {
        if(UIManager.instance.IsPause)
            return;
        
#if UNITY_SWITCH
        Vector2 md = new Vector2();
        if (NintendoInput.isEditorInputActiv)
        {
            md = new Vector2(NintendoInput.InputNpadAxis(NintendoInput.NpadAxis.RightHorizontal),
                NintendoInput.InputNpadAxis(NintendoInput.NpadAxis.RightVertical));
        }
        // else
        //     md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
#else
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
#endif

        md = Vector2.Scale(md, new Vector2(sensitivity * Time.deltaTime, sensitivity * Time.deltaTime));

        xRotation -= md.y;

        cam.localRotation = Quaternion.Euler(Mathf.Clamp(xRotation, -70, 70), 0, 0);

        transform.transform.Rotate(Vector3.up * md.x);

    }

}