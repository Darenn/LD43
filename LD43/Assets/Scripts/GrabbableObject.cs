using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class GrabbableObject : MonoBehaviour
{
    public GameObject ObjectPrefabToGiveOnGrab;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public GameObject getObjectToGrab()
    {
        return Instantiate(ObjectPrefabToGiveOnGrab);
    }
}
