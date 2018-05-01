using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ThrowerController : NetworkBehaviour {

    // Use this for initialization
    #region Attributes
    private const float Max_Angle = 90.0f;
    private const float Min_Angle = 0.0f;
    public float Angle;
    public GameObject throwable_Object;
    public GameObject Player;
    public GameObject Arrow;
    public int ThrowForce;
    private int AngleSensitivity = 50;
    #endregion

    void Start () {
        Angle = 45;
        ThrowForce = 45;
        Arrow.transform.position = gameObject.transform.position + Vector3.up * 2;
        Arrow.transform.rotation = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        SpawnThrowAimer();
        if (Input.GetMouseButtonDown(0) && canThrow() && throwable_Object != null)
        {
            Throw();
        }  
	}

    private bool canThrow()
    {
        return /*PlayerTrigger.instance.isPickingPlayer ||*/ PlayerTrigger.instance.picked != null;
    }

    private void SpawnThrowAimer()
    {
        Angle += -Input.GetAxis("Mouse ScrollWheel") * AngleSensitivity;
        Angle = Mathf.Clamp(Angle, Min_Angle, Max_Angle);
        Arrow.transform.position = gameObject.transform.position + Vector3.up * 2;
        Arrow.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(-Angle, 0, 0);
    }
    private void Throw()
    {
        GameObject tempObject = throwable_Object;
        if (throwable_Object.tag == "Player")
        {
            Debug.Log("Throw Player");
            PlayerController p = throwable_Object.GetComponent<PlayerController>();
            if (p.isServer)
            {
                p.CmdThrow(Angle, ThrowForce);
            }
            else
            {
                p.RpcThrow(Angle, ThrowForce);
            }
        }
        else //throw Object.
        {
            tempObject.transform.GetComponent<Rigidbody>().AddForce
            (
                (tempObject.transform.forward * Mathf.Cos(Mathf.Deg2Rad * Angle) +
                tempObject.transform.up * Mathf.Sin(Mathf.Deg2Rad * Angle)) * ThrowForce,
                ForceMode.VelocityChange
            );
            tempObject.GetComponent<Rigidbody>().useGravity = true;
        }
        //PlayerTrigger.instance.DeletePlayerFromList(tempObject);
        PlayerTrigger.instance.picked = null;
    }
}
