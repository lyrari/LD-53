using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    bool grabbed;
    Rigidbody rb;

    public DepositBin.BinType MyCorrectBin;

    // Any other info about the object should go here - like mail stats, destination

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ToggleGrabbed()
    {
        grabbed = !grabbed;
        rb.isKinematic = !rb.isKinematic;

        CreepyTutorialLetter creepletter = this.GetComponent<CreepyTutorialLetter>();
        if (creepletter != null)
        {
            creepletter.OnCreepyLetterPickup();
        }
    }
}
