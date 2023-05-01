using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public ScoreTracker m_ScoreTracker;

    Camera MainCamera;
    public float InteractionRange = 1.5f;
    public LayerMask InteractableObjectMask;

    public Transform HoldingObjectLocation;
    public Transform DropObjectLocation;
    public GrabbableObject HeldObject;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) {

            // Raycast code

            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            bool raycastHitSuccess = Physics.Raycast(ray, out hit, InteractionRange, InteractableObjectMask);

            if (raycastHitSuccess)
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name + " at distance: " + hit.distance);

                // Hit button if you raycasted with one
                ButtonClick button = hit.transform.GetComponent<ButtonClick>();
                if (button != null)
                {
                    button.ToggleButton();
                    return;
                }

                // Grab an object if not already grabbing one
                GrabbableObject grabObject = hit.transform.GetComponent<GrabbableObject>();
                if (grabObject != null && HeldObject == null)
                {
                    Debug.Log("Grab object");
                    HeldObject = grabObject;
                    grabObject.ToggleGrabbed();

                    grabObject.transform.SetParent(HoldingObjectLocation);
                    grabObject.transform.localPosition = Vector3.zero;
                    grabObject.transform.localRotation = Quaternion.LookRotation(Vector3.up);
                    return;
                }
            }

            // Do stuff with the object you are holding
            if (HeldObject != null)
            {
                // See if the object can be placed into a bin. If it can, do that then return
                if (raycastHitSuccess)
                {
                    Debug.Log("Try to place it in something");
                    DepositBin bin = hit.transform.GetComponent<DepositBin>();
                    if (bin != null)
                    {
                        // Place in bin: Destroy it and mark success/failure
                        bool success = bin.DepositInBin(HeldObject);
                        if (success)
                        {
                            m_ScoreTracker.successes++;
                        } else
                        {
                            m_ScoreTracker.failures++;
                        }
                        Debug.Log("Place in bin success: " + success);
                        Destroy(HeldObject.gameObject);
                        HeldObject = null;
                        return;
                    }
                }

                // Else, drop the item
                Debug.Log("Drop it");
                HeldObject.transform.SetParent(null);
                HeldObject.transform.position = DropObjectLocation.transform.position;
                HeldObject.ToggleGrabbed();
                HeldObject = null;
            }
            
        }
        
    }
}
