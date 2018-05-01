using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Goal : NetworkBehaviour {
    #region Attributes
    private int playersIn;  // The amount of players in the trigger
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
        if (!isServer)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            ++playersIn;
            if (playersIn == FindObjectsOfType<PlayerController>().Length)
            {
                PlayerController[] p = FindObjectsOfType<PlayerController>();
                for (int i = 0; i < p.Length; ++i)
                {
                    if (p[i].isClient)
                    {
                        p[i].RpcReset(i);
                    }
                    else
                    {
                        p[i].ReSpawn(i);
                    }
                }
                Interactible[] n = FindObjectsOfType<Interactible>();
                for (int i = 0; i < n.Length; ++i)
                {
                    n[i].transform.position = Vector3.up * (40 + i);
                }
            }
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (!isServer)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            --playersIn;
        }
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
