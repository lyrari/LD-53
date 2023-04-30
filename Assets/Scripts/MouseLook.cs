using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    public float m_ClampYDegrees = 75f;
    public float m_MouseSensitivity = 500f;
    float m_VerticalRotation = 0f;

    Quaternion StartRotation = Quaternion.Euler(20f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = StartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        float mouseMoveY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_VerticalRotation -= mouseMoveY;
        m_VerticalRotation = Mathf.Clamp(m_VerticalRotation, -m_ClampYDegrees, m_ClampYDegrees);

        transform.localRotation = Quaternion.Euler(m_VerticalRotation, 0, 0);
        
        playerBody.Rotate(Vector3.up * mouseMoveX);
    }
}
