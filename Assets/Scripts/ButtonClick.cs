using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public BoolSO ThingToToggle;

    public void ToggleButton()
    {
        ThingToToggle.Toggle();
    }
}
