using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float m_MoveSpeed = 12f;
    public bool m_IsMoving = false;
    public float stepWait = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SoundOut());

        IEnumerator SoundOut()
        {
            while (m_IsMoving == true)
            {
                AkSoundEngine.PostEvent("footStepping", this.gameObject);
                Debug.Log("Foot Should Be Stepping");
                yield return new WaitForSeconds(stepWait);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float SideMovement = Input.GetAxis("Horizontal");
        float FrontBackMovement = Input.GetAxis("Vertical");

        if (SideMovement > 0 || FrontBackMovement > 0)
        {
            m_IsMoving = true;
        }
        else m_IsMoving = false;
        
        
        

        Vector3 move = transform.right * SideMovement + transform.forward * FrontBackMovement;
        controller.Move(move * m_MoveSpeed * Time.deltaTime);
    }
}
