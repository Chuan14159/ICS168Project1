using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    #region Attributes
    private bool instructionOn = false;
    private int [] pType;
    [SerializeField]
    private List<GameObject> prefabs;       // The list of game objects to spawn for each team

    public Image instruction;
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
        pType = new int[] { 0, 0 };
        Instance = this;
	}

	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            instructionOn = !instructionOn;
            instruction.gameObject.SetActive(instructionOn);
        }
	}
	#endregion
	
	#region Methods
	// Spawn the objects for a certain team
    public void SpawnObjects (Team team)
    {
        foreach (GameObject g in prefabs)
        {
            if (g.GetComponent<Interactible>() != null)
            {
                Vector3 pos = new Vector3(Random.Range(0f, 40f), 10, Random.Range(0f, 40f));
                Instantiate(g, pos, Quaternion.identity).GetComponent<Interactible>().SetObject(team);
            }
        }
    }

    public int AssignRole()
    {
        int role = 0;
        int temp = 0;
        for(int i = 0; i < pType.Length; i++)
        {
            temp = pType[i];
            if (temp < pType[i])
            {
                role = i;
                ++pType[i];
            }
        }
        Debug.Log("role" + role);
        return role;
    }
    // Restart the game if needed
    public void RestartGame ()
    {
        
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
