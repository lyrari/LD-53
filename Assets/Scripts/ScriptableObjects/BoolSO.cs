using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Bool")]
public class BoolSO : ScriptableObject
{
    public bool value;

    public void Toggle()
    { 
        value = !value;
    }
}
