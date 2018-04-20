using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {

    private bool isPicking = false;
    private GameObject picked;
    private bool isPickingPlayer = false;
    private List<GameObject> objectStore = new List<GameObject>();

    public GameObject player;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isPicking = !isPicking;
            if (isPicking && objectStore.Count > 0)
            {
                picked = objectStore[0];
                if (picked.tag == "Object")
                {
                    picked.transform.GetComponent<Rigidbody>().MovePosition(transform.position);
                    picked.transform.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
                }
                else if (picked.tag == "Player")
                {
                    picked.transform.parent.GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward + transform.up);
                    picked.transform.parent.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
                }
            }
        }
        Debug.Log(objectStore.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Object" || other.tag == " Player") && !objectStore.Contains(other.gameObject))
        {
            objectStore.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Object" || other.tag == " Player") && objectStore.Contains(other.gameObject))
        {
            objectStore.Remove(other.gameObject);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        //Debug.Log(isPicking);
        if (other.tag != "Object" || other.GetComponent<Interactible>().TeamID == null)
        {
            return;
        }

        if(other.GetComponent<Interactible>().TeamID.ID == player.GetComponent<PlayerController>().team.ID)
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
    }*/
}
