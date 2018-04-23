using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    #region Attributes
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
		
	}

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.RestartGame();
        }
    }
    #endregion

    #region Methods
    // Move to a platform
    public void MoveToPlatform (Transform platform)
    {
        transform.position = platform.position + Vector3.up * (platform.lossyScale.y / 2 + 1f);
    }
    #endregion

    #region Coroutines

    #endregion
}
