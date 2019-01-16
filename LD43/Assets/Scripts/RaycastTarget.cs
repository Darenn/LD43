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
            bool hasknife = inventory.GrabbedObject != null && inventory.GrabbedObject.CompareTag("Knife");
            if (current_target != null && current_target.GetComponent<Outline>() != null && current_target.GetComponent<Killable>() != null && !hasknife)
            {
                    current_target.GetComponent<Outline>().enabled = false;
            }
        }

        UpdateInputs();
    }

    void UpdateInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            if (current_target != null && (current_target.GetComponent<GrabbableObject>() != null))
            {
                if (inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(false);
                inventory.GrabObject(current_target);
                if (inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(true);
                if (current_target != null && current_target.GetComponent<Outline>() != null)
                current_target.GetComponent<Rigidbody>().isKinematic = true;
                current_target.GetComponent<Outline>().enabled = false;
                inventory.GrabbedObject.transform.parent = transform;
                inventory.GrabbedObject.transform.position = ObjectGrabbedPosition.position;
                current_target = null;
            }
        }
        if (Input.GetButtonDown("Drop"))
        {
            dropSelectedObj();
        }
        if (Input.GetButtonDown("Fire1") && current_target != null && inventory.GrabbedObject != null)
        {
            // Give food to cabane
            Cabane cabane = current_target.GetComponent<Cabane>();
            Food food = inventory.GrabbedObject.GetComponent<Food>();
            // If i have food and im looking to the cabane
            if (cabane != null && food != null)
            {
                if (food.foodType != cabane.currentFood().foodType)
                {

                }
                else
                {
                    cabane.Feed(food);
                    dropSelectedObjDestroy();
                }
            }

            // Or cut the guy
            Killable kill = current_target.GetComponent<Killable>();
            if (kill != null && inventory.GrabbedObject.CompareTag("Knife"))
            {
                GameObject go = Instantiate(kill.PrefabToSpawn, current_target.transform.position, Quaternion.identity);
                go.GetComponent<Rigidbody>().isKinematic = false;
                go.GetComponent<Food>().droppedOnFloor();
                ;
                Destroy(kill.gameObject);
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
            changeInventorySlot(Mathf.Clamp(inventory.CurrentSlot - 1, 0, inventory.Objects.Length - 1));
        }
        if (Input.GetButtonDown("ChangeObjectR"))
        {
            changeInventorySlot(Mathf.Clamp(inventory.CurrentSlot + 1, 0, inventory.Objects.Length - 1));
        }
    }

    void changeInventorySlot(int slot)
    {
        if(inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(false);
        inventory.CurrentSlot = slot;
        if(inventory.Objects[inventory.CurrentSlot] != null) inventory.Objects[inventory.CurrentSlot].SetActive(true);
    }

    void dropSelectedObj()
    {
        inventory.DropSelectedObject();
        for (int i = inventory.CurrentSlot - 1; i >= 0; i--)
        {
            if (inventory.Objects[i] != null)
            {
                changeInventorySlot(i);
                break;
            }
        }
        if (inventory.Objects[inventory.CurrentSlot] == null)
        {
            for (int i = inventory.Objects.Length - 1; i >= 0; i--)
            {
                if (inventory.Objects[i] != null)
                {
                    changeInventorySlot(i);
                    break;
                }
            }
        }
    }

    void dropSelectedObjDestroy()
    {
        Destroy(inventory.GrabbedObject);
        dropSelectedObj();
    }
}
