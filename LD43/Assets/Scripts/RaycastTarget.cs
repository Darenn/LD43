using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{

    public float RaycastRange;
    public Transform ObjectGrabbedPosition;

    private Camera fpsCam;
    private GameObject current_target;

    private GameObject objectGrabbed = null;

    void Start () {
	    fpsCam = GetComponentInParent<Camera>();
    }
	
	// Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        GameObject newTarget = null;
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, RaycastRange))
        {
            if (hit.transform.GetComponent<GrabbableObject>() != null)
            {
                newTarget = hit.transform.gameObject;
            }
        }

        if (newTarget != current_target)
        {
            if (current_target != null) 
                current_target.GetComponent<Outline>().enabled = false;
            current_target = newTarget;
            if (newTarget != null)
                current_target.GetComponent<Outline>().enabled = true;
        }

        if (Input.GetButton("Fire1"))
        {
            if (current_target != null)
            {
                objectGrabbed = current_target.GetComponent<GrabbableObject>().getObjectToGrab();
                objectGrabbed.transform.parent = transform;
                objectGrabbed.transform.position = ObjectGrabbedPosition.position;
                Destroy(current_target);
                current_target = null;
            }
        }
    }
}
