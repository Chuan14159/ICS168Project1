using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerTrigger : NetworkBehaviour {

    public bool isPicking = false;
    private GameObject picked = null;
    public bool isPickingPlayer = false;
    private List<GameObject> objectStore;
    public GameObject player;
    public static PlayerTrigger instance;
    // Use this for initialization
    void Start () {
        instance = this;
        objectStore = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!player.GetComponent<PlayerController>().isLocalPlayer)
            return;

        if (picked != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (picked.tag == "Player")
                {
                    isPickingPlayer = !isPickingPlayer;
                    if (!isPickingPlayer)
                        DeletePlayerFromList(picked);
                }
                else isPicking = !isPicking;
            }
            if (picked.tag == "Object" && isPicking)
            {
                picked.transform.GetComponent<Rigidbody>().MovePosition(transform.position);
                picked.transform.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
                ThrowerController.instance.throwable_Object = picked;
            }
            else if (picked.tag == "Player" && isPickingPlayer)
            {
                picked.transform.parent.GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward + transform.up);
                picked.transform.parent.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
                ThrowerController.instance.throwable_Object = picked;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Object" || other.tag == "Player") && !objectStore.Contains(other.gameObject))
        {
            {
                objectStore.Add(other.gameObject);
                picked = objectStore[0];
            }   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (objectStore.Count > 0 && objectStore.Contains(other.gameObject))
        {   
            if (isPickingPlayer)
                return;
            objectStore.Remove(other.gameObject);
            isPicking = false;
            picked = null;
        }
    }

    public void DeletePlayerFromList(GameObject o)
    {
        if (objectStore.Contains(o))
        {
            Debug.Log("Player Deleted");
            isPickingPlayer = false;
            isPicking = false;
            objectStore.Remove(o);
            picked = null;
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
