using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MailButton : MonoBehaviour
{
    public MailPickup m_MailPickup;

    public void ClickMailButton()
    {
        m_MailPickup.PressMailButton();
    }
}
