using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{

    public float RaycastRange;
    public Transform ObjectGrabbedPosition;
    public float DropForce;

    private Camera fpsCam;
    private GameObject current_target;

    [SerializeField] private Inventory inventory;

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
            newTarget = hit.transform.gameObject;
        }

        if (newTarget != current_target)
        {
            if (current_target != null && current_target.GetComponent<Outline>() != null) 
                current_target.GetComponent<Outline>().enabled = false;
            current_target = newTarget;
            if (newTarget != null && current_target.GetComponent<Outline>() != null)
                current_target.GetComponent<Outline>().enabled = true;
        }

        UpdateInputs();
    }

    void UpdateInputs()
    {
        if (Input.GetButton("Fire1"))
        {
            if (current_target != null && current_target.GetComponent<GrabbableObject>() != null)
            {
                if (inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(false);
                inventory.GrabObject(current_target.GetComponent<GrabbableObject>().getObjectToGrab());
                if (inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(true);
                inventory.GrabbedObject.transform.parent = transform;
                inventory.GrabbedObject.transform.position = ObjectGrabbedPosition.position;
                Destroy(current_target);
                current_target = null;
            }
        }
        if (Input.GetButton("Drop"))
        {
            inventory.DropSelectedObject();
        }
        if (Input.GetButton("Fire2") && current_target != null && inventory.GrabbedObject != null)
        {
            Cabane cabane = current_target.GetComponent<Cabane>();
            Food food = inventory.GrabbedObject.GetComponent<Food>();
            // If i have food and im looking to the cabane
            if (cabane != null && food != null)
            {
                cabane.Feed(food.FoodAmount);
                inventory.DropSelectedObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeInventorySlot(0);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeInventorySlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeInventorySlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeInventorySlot(3);
        }

        if (Input.GetButtonDown("ChangeObjectL"))
        {
            changeInventorySlot(Mathf.Clamp(inventory.CurrentSlot - 1, 0, inventory.Objects.Length));
        }
        if (Input.GetButtonDown("ChangeObjectR"))
        {
            changeInventorySlot(Mathf.Clamp(inventory.CurrentSlot + 1, 0, inventory.Objects.Length));
        }
    }

    void changeInventorySlot(int slot)
    {
        if(inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(false);
        inventory.CurrentSlot = slot;
        if(inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(true);
    }
}
