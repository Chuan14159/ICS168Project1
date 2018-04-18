using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private List<GameObject> prefabs;       // The list of game objects to spawn for each team
    #endregion

    #region Properties
    public static GameController Instance   // The instance to reference
    { get; private set; }

    // Get and private set for prefabs
    public List<GameObject> Prefabs
    {
        get
        {
            return new List<GameObject>(prefabs);
        }
        private set
        {
            prefabs = value;
        }
    }
	#endregion

	#region Event Functions
	// Awake is called before Start
	private void Awake ()
	{
        Instance = this;
	}

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
	}
	#endregion
	
	#region Methods
	// Spawn the objects for a certain team
    private void SpawnObjects (Team team)
    {
        foreach (GameObject g in prefabs)
        {
            if (g.GetComponent<Interactible>() != null)
            {
                Vector3 pos = new Vector3(Random.Range(0f, 4f), 10, Random.Range(0f, 4f));
                Instantiate(g, pos, Quaternion.identity).GetComponent<Interactible>().SetObject(team);
            }
        }
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
