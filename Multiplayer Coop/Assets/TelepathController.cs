using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class TelepathController : NetworkBehaviour {

    #region Attribute
    private Transform myCam;
    private Vector3 leviatePoint;
    private bool canTele;
    private Rigidbody teleObject = null;

    public float force;
    public float high;
    public float Range;
    public GameObject camOrigin;
    public Image crossHair;
    #endregion
    private void Awake()
    {
        crossHair.enabled = true;
        StartCoroutine("getCamera");
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!isLocalPlayer)
            return;

        leviatePoint = SetLevitatePoint();
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (teleObject == null)
                canTele = Telekenesis();
            else
            {
                teleObject = null;
                canTele = false;
            }
            Debug.Log(teleObject);
        }
	}
    private void LateUpdate()
    {
        if (canTele && teleObject != null)
        {
            Vector3 position = leviatePoint - teleObject.transform.position;
            teleObject.velocity = position * force * Time.deltaTime;
            Debug.Log(canTele);
        }
    }

    private bool Telekenesis()
    {
        RaycastHit hit;
        if (Physics.Raycast(myCam.position, myCam.forward, out hit, Range)  && hit.transform.tag == "Object")
        {
            teleObject = hit.transform.GetComponent<Rigidbody>();
            return true;
        }
        return false;
    }

    private Vector3 SetLevitatePoint()
    {
        return gameObject.transform.position + (gameObject.transform.forward + Vector3.up) * high;
    }

    IEnumerator getCamera()
    {
        yield return new WaitForEndOfFrame();
        for(int i = 0; i < camOrigin.transform.childCount; i++)
        {
            if (camOrigin.transform.GetChild(i).tag == "MainCamera")
            {
                myCam = camOrigin.transform.GetChild(i);
                Debug.Log(myCam.tag);
            }
        }
    }
}
