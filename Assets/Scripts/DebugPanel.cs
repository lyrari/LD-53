using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    public BoolSO LightsOut;
    public BoolSO Debug;

    public LetterSpawner TestSpawner;
    private void Update()
    {
        if (!Debug.value) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LightsOut.Toggle();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //TestSpawner.OpenBag();
        }
    }
}
