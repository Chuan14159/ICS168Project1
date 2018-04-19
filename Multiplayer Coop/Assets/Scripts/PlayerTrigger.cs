using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {

    private bool isPicking = false;
    private GameObject picked;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(isPicking);
        if(other.tag == "Object")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isPicking = !isPicking;
                if (isPicking)
                {
                    picked = other.gameObject;
                }
            }
            if (isPicking)
            {
                //Debug.Log("picking");
                picked.transform.position = transform.position;
                picked.transform.rotation = transform.rotation;
            }
        }
    }
}
