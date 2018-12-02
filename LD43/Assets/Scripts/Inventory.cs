using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public GameObject[] Objects;

    public int CurrentSlot = 0;

	void Start () {
		Objects = new GameObject[4];
	}

    void Update()
    {

    }

    public void GrabObject(GameObject obj)
    {
        // If there is a free slot, put the object in it and move to this slot
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i] == null)
            {
                Objects[i] = obj;
                CurrentSlot = i;
                return;
            }
        }
    }

    public void DropSelectedObject()
    {
        //Destroy(Objects[CurrentSlot]);
        if (Objects[CurrentSlot] == null) return;
        Objects[CurrentSlot].GetComponent<Rigidbody>().isKinematic = false;
        Objects[CurrentSlot].transform.parent = null;
        Objects[CurrentSlot] = null;
    }

    public GameObject GrabbedObject { get { return Objects[CurrentSlot]; } }
}
