using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncScale : NetworkBehaviour {
    #region Attributes
    [SyncVar(hook = "OnScaleChange")]
    public Vector3 scale;   // The scale to keep track of
	#endregion
	
	#region Properties
	
	#endregion

	#region Event Functions
	// Awake is called before Start
	private void Awake ()
	{
        
	}

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
        if (isServer)
        {
            scale = transform.localScale;
        }
        else if (isClient)
        {
            transform.localScale = scale;
        }
    }
	#endregion
	
	#region Methods
	// The hook of changing scale
    private void OnScaleChange (Vector3 newScale)
    {
        scale = newScale;
        transform.localScale = newScale;
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
