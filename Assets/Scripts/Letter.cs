using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    MeshRenderer m_MeshRenderer;
    GrabbableObject m_GrabbableObject;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Letter awake");
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_GrabbableObject = GetComponent<GrabbableObject>();
    }

    public void Init(Material letterMaterial, DepositBin.BinType binType)
    {
        Debug.Log($"InitLetter: {letterMaterial} - {binType}");
        m_MeshRenderer.material = letterMaterial;
        m_GrabbableObject.MyCorrectBin = binType;
    }
}
