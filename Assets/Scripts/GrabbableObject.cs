using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    bool grabbed;
    Rigidbody rb;

    // Any other info about the object should go here - like mail stats, destination

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ToggleGrabbed()
    {
        grabbed = !grabbed;
        rb.isKinematic = !rb.isKinematic;
    }
}
