using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Camera MainCamera;
    public Transform playerBody;

    public float m_ClampYDegrees = 75f;
    public float m_MouseSensitivity = 100f;

    public float DefaultFOV = 60f;
    public float ZoomedFOV = 30f;

    float m_VerticalRotation = 0f;

    Quaternion StartRotation = Quaternion.Euler(20f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = StartRotation;
        MainCamera.fieldOfView = DefaultFOV;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MainCamera.fieldOfView = ZoomedFOV;
        }

        if (Input.GetMouseButtonUp(1))
        {
            MainCamera.fieldOfView = DefaultFOV;
        }


        float mouseMoveX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        float mouseMoveY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_VerticalRotation -= mouseMoveY;
        m_VerticalRotation = Mathf.Clamp(m_VerticalRotation, -m_ClampYDegrees, m_ClampYDegrees);

        transform.localRotation = Quaternion.Euler(m_VerticalRotation, 0, 0);
        
        playerBody.Rotate(Vector3.up * mouseMoveX);
    }
}
