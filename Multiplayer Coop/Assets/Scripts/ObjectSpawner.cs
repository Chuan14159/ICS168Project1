using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour {
    public List<GameObject> objects;

    private List<GameObject> spawnSet;



	// Use this for initialization
	void Start () {
        spawnSet = new List<GameObject>();
        for (int i = 0; i < 10; i++){
            int j = Random.Range(0, objects.Count);
            spawnSet.Add(objects[j]);
        }
	}
	
    public void spawnItems(){
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        yield return new WaitForSeconds(1);
        foreach (GameObject o in spawnSet)
        {
            GameObject g = Instantiate(o, transform.position, Quaternion.identity);
            NetworkServer.Spawn(g);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
