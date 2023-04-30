using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Camera MainCamera;
    public float InteractionRange = 1.5f;
    public LayerMask InteractableObjectMask;

    public Transform HoldingObjectLocation;
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
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, InteractionRange, InteractableObjectMask))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name + " at distance: " + hit.distance);

                // See what we hit
                ButtonClick button = hit.transform.GetComponent<ButtonClick>();
                if (button != null)
                {
                    button.ToggleButton();
                }

                GrabbableObject grabObject = hit.transform.GetComponent<GrabbableObject>();
                if (grabObject != null)
                {
                    Debug.Log("Grab object");
                    HeldObject = grabObject;
                    grabObject.transform.parent = HoldingObjectLocation;
                    grabObject.transform.localPosition = Vector3.zero;
                    grabObject.transform.localRotation = Quaternion.LookRotation(Vector3.back);
                }
            }
        }
        
    }
}
