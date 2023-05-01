using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositBin : MonoBehaviour
{
    public enum BinType
    {
        Red,
        Blue,
        Yellow,
        Discard,
        Hell,
        None,
    }

    public BinType MyBinType;

    public bool DepositInBin(GrabbableObject objectToPutIn)
    {
        return objectToPutIn.MyCorrectBin == MyBinType;
    }
}
