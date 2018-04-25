using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OfflineFloorDetection : MonoBehaviour {
    #region Attributes
    protected Collider floorDetector;
    protected List<Collider> floorCollisions;
    #endregion

    #region Properties
    // Returns whether the object is grounded
    public bool Grounded
    {
        get
        {
            return floorCollisions.Count > 0;
        }
    }
    #endregion

    #region Event Functions
    // Awake is called before Start
    private void Awake ()
	{
        floorCollisions = new List<Collider>();
        floorDetector = GetComponent<Collider>();
    }

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
	}

    protected void OnTriggerEnter(Collider other)
    {
        if (!floorCollisions.Contains(other))
        {
            floorCollisions.Add(other);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (floorCollisions.Contains(other))
        {
            floorCollisions.Remove(other);
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
