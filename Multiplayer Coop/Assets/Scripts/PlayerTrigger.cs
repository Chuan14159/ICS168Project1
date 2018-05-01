using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerTrigger : NetworkBehaviour {

    //public bool isPicking = false;
    public GameObject picked = null;
    public GameObject trigger = null;
    private float rotateZ = 0;
    private float lift = 1;
    public GameObject triggerPoint;
    //public bool isPickingPlayer = false;
    //private List<GameObject> objectStore;
    //public GameObject player;
    public static PlayerTrigger instance;
    // Use this for initialization
    void Start () {
        instance = this;
        //objectStore = new List<GameObject>();
	}

    // Update is called once per frame
    /*void Update () {
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
                else
                {
                    if (picked.GetComponent<Interactible>().canBePickedUp && !isPicking)
                    {
                        isPicking = true;
                    }
                    else if (isPicking)
                    {
                        isPicking = false;
                    }
                    if (!isPicking)
                        DeletePlayerFromList(picked);
                }
            }
            if (picked.tag == "Object")
            {
                if (isPicking)
                {
                    picked.layer = 8;
                    picked.GetComponent<Interactible>().ChangeColor();
                    picked.GetComponent<Rigidbody>().useGravity = false;
                    picked.transform.GetComponent<Rigidbody>().MovePosition(transform.position);
                    picked.transform.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
                    ThrowerController.instance.throwable_Object = picked;
                }
                else
                {
                    picked.layer = 0;
                    picked.GetComponent<Rigidbody>().useGravity = true;
                    picked.GetComponent<Interactible>().ChangeColor();
                }
            }
            else if (picked.tag == "Player" && isPickingPlayer)
            {
                picked.transform.parent.GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward + transform.up);
                picked.transform.parent.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
                ThrowerController.instance.throwable_Object = picked;
            }
        }
        
    }*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(picked == null && trigger == null)
            {
                return;
            }
            if (picked == null && trigger != null)
            {
                if (trigger.tag == "Player" || trigger.tag == "Object")
                {
                    picked = trigger;
                }
            }
            else
            {
                //if(picked.tag == "Player")
                //    picked.transform.parent.GetComponent<PlayerController>().TargetPlayer = null;
                picked.transform.GetComponent<Rigidbody>().useGravity = true;
                picked = null;
                rotateZ = 0;
                lift = 1;
                return;
            }
        }

        if (picked != null)
        {
            if (picked.tag == "Player")
            {
                picked.transform.parent.GetComponent<PlayerController>().TargetPlayer = gameObject;
                picked.transform.parent.GetComponent<Rigidbody>().MovePosition(triggerPoint.transform.position - triggerPoint.transform.forward + triggerPoint.transform.up);
                picked.transform.parent.GetComponent<Rigidbody>().MoveRotation(triggerPoint.transform.rotation);
                ThrowerController.instance.throwable_Object = picked;
            }
            else if (picked.tag == "Object")
            {
                Quaternion rotation = Quaternion.Euler(0, 0, rotateZ);
                picked.transform.GetComponent<Rigidbody>().useGravity = false;
                picked.transform.GetComponent<Rigidbody>().MoveRotation(triggerPoint.transform.rotation * rotation);
                picked.transform.GetComponent<Rigidbody>().MovePosition(triggerPoint.transform.position + triggerPoint.transform.up * lift);
                ThrowerController.instance.throwable_Object = picked;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                rotateZ += 2;
                if(rotateZ > 90)
                {
                    rotateZ = 90;
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotateZ -= 2;
                if (rotateZ < -90)
                {
                    rotateZ = -90;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                lift += 0.1f;
                if(lift > 4)
                {
                    lift = 4;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Object")
        {
            trigger = other.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        trigger = null;
    }

    /*private void OnTriggerEnter(Collider other)
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
            if (isPickingPlayer || isPicking)
                return;
            objectStore.Remove(other.gameObject);
            isPicking = false;
            picked.layer = 0;
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
    }*/

}
