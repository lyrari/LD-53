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
        bool successfulDeposit = objectToPutIn.MyCorrectBin == MyBinType;

        if (successfulDeposit)
        {
            
            if (MyBinType == BinType.Hell)
            {
                // Play hell sound instead of success sound
            } else
            {
                // Play standard success sound
            }
        } else
        {
            // Play failure sound
        }

        return successfulDeposit;
    }
}
