﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePlayerTrigger : MonoBehaviour {

    public bool isPicking = false;
    private GameObject picked = null;
    public bool isPickingPlayer = false;
    private List<GameObject> objectStore;
    public GameObject player;
    public static OfflinePlayerTrigger instance;
    private bool tryingPick = false;
    // Use this for initialization
    void Start () {
        instance = this;
        objectStore = new List<GameObject>();
	}

    private void Update()
    {
        tryingPick = Input.GetKeyDown(KeyCode.F);        
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (picked != null && picked.GetComponent<OfflineInteractible>().TeamID.ID == player.GetComponent<OfflinePlayerController>().team.ID)
        {
            if (tryingPick)
            {
                if (picked.tag == "Player")
                {
                    isPickingPlayer = !isPickingPlayer;
                    if (!isPickingPlayer)
                        DeletePlayerFromList(picked);
                }
                else isPicking = !isPicking;
                if (!isPicking)
                {
                    picked.transform.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            if (picked.tag == "Object" && isPicking)
            {
                picked.transform.GetComponent<Rigidbody>().isKinematic = true;
                picked.transform.GetComponent<Rigidbody>().MovePosition(transform.position);
            }
            else if (picked.tag == "Player" && isPickingPlayer)
            {
                picked.transform.parent.GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward + transform.up);
                OfflineThrowerController.instance.throwable_Object = picked;
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
            if (other.gameObject == picked && isPickingPlayer)
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
