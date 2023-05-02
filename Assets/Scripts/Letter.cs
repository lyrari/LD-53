using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DepositBin;

public class Letter : MonoBehaviour
{
    MeshRenderer m_MeshRenderer;
    GrabbableObject m_GrabbableObject;
    public Image Stamp;
    public List<Sprite> StampImagesNormal;
    
    //public List<Texture2D> StampImagesSpooky;
    //public BoolSO SpookyModeEnabled;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Letter awake");
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_GrabbableObject = GetComponent<GrabbableObject>();
    }

    public void Init(Material letterMaterial, DepositBin.BinType binType, bool hasStamp)
    {
        Debug.Log($"InitLetter: {letterMaterial} - {binType}");
        m_MeshRenderer.material = letterMaterial;

        SetupStamp(hasStamp, binType);
    }

    public void SetupStamp(bool hasStamp, BinType normalBintype) {
        if (!hasStamp)
        {
            Stamp.transform.parent.gameObject.SetActive(false);
            m_GrabbableObject.MyCorrectBin = DepositBin.BinType.Discard;
        }
        else
        {
            Stamp.transform.parent.gameObject.SetActive(true);
            m_GrabbableObject.MyCorrectBin = normalBintype;
            SetRandomStampImage();
        }
    }

    void SetRandomStampImage()
    {
        Sprite StampImage = StampImagesNormal[Random.Range(0, StampImagesNormal.Count)];
        Stamp.sprite = StampImage;
    }
}
