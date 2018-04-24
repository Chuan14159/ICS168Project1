using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject floorCube;
    public GameObject ceilingCube;
    public GameObject levelGoal;

    private GameObject spawner;

    private List< List< GameObject > > levelArray;

    private float levelWidth = 30f;
    private float levelLength = 30f;
    private float maxHeight = 30f;
    private float wallHeight = 52.5f;
    private float beginX;
    private float beginZ;
    private float perlinScale = 10f;
    private float offsetX;
    private float offsetZ;

    private int startAreaOffset = 4;

	// Use this for initialization
	void Start () {
        offsetX = Random.Range(0f, 99999f);
        offsetZ = Random.Range(0f, 99999f);
        levelArray = new List< List< GameObject > >();
        spawner = transform.GetChild(0).gameObject;
        instantiateLevel();
	}


    void instantiateLevel(){
        generateLevel();
        generateWalls();
        spawner.GetComponent<ObjectSpawner>().spawnItems();
    }


    void generateLevel(){
        beginX = -levelWidth * 5f / 2;
        beginZ = -levelLength * 5f / 2;

        GameObject highestPointObject = null;
        float highestPointHeight = 0f;

        //Create the main floor for the level
        for (int i = 0; i < levelWidth; i++)
        {
            List<GameObject> tempList = new List<GameObject>();
            for (int j = 0; j < levelLength; j++)
            {
                Vector3 position = new Vector3(beginX, 0f, beginZ);
                GameObject o = Instantiate(floorCube, position, Quaternion.identity);
                tempList.Add(o);
                //This allows us to have a flat starting point in the middle of the map for players to start at
                if (i < levelWidth / 2 - startAreaOffset || i > levelWidth / 2 + startAreaOffset || j < levelLength / 2 - startAreaOffset || j > levelLength / 2 + startAreaOffset)
                {
                    o.transform.localScale += perlinGenerator(i, j);

                    if (o.transform.localScale.y > highestPointHeight) {
                        highestPointHeight = o.transform.localScale.y;
                        highestPointObject = o;
                    }
                }
                o.transform.SetParent(transform);
                beginZ += 5f;
            }
            beginX += 5f;
            beginZ = -levelLength * 5 / 2;
            levelArray.Add(tempList);
        }

        //Instantiate the goal
        if (highestPointObject != null) {
            Vector3 goalPos = new Vector3(highestPointObject.transform.position.x, highestPointHeight / 2 + 2f, highestPointObject.transform.position.z);
            levelGoal = Instantiate(levelGoal, goalPos, Quaternion.identity);
        }
    }

    Vector3 perlinGenerator(int x, int z){
        float xCoord = x / levelWidth * perlinScale + offsetX;
        float zCoord = z / levelLength * perlinScale + offsetZ;

        float pn = Mathf.PerlinNoise(xCoord, zCoord);
        return new Vector3(0f, Mathf.Floor(pn*60), 0f);
    }

    Vector3 randomGenerator(){
        return new Vector3(0f, Mathf.Floor(Random.Range(0, maxHeight)), 0f);
    }

    void generateWalls(){
        //Create walls around level
        float wallX = (-levelWidth * 5f / 2) - 5f;
        float wallZ = (-levelLength * 5f / 2) - 5f;
        for (int i = 0; i < levelWidth + 2f; i++)
        {
            Vector3 position = new Vector3(wallX, 0f, wallZ);
            GameObject o = Instantiate(floorCube, position, Quaternion.identity);
            o.transform.localScale += new Vector3(0f, wallHeight, 0f);
            o.transform.SetParent(transform);
            wallX += 5f;
        }

        wallX -= 5f;
        wallZ += 5f;
        for (int i = 0; i < levelLength + 1f; i++)
        {
            Vector3 position = new Vector3(wallX, 0f, wallZ);
            GameObject o = Instantiate(floorCube, position, Quaternion.identity);
            o.transform.localScale += new Vector3(0f, wallHeight, 0f);
            o.transform.SetParent(transform);
            wallZ += 5f;
        }

        wallZ -= 5f;
        wallX -= 5f;
        for (int i = 0; i < levelWidth + 1f; i++)
        {
            Vector3 position = new Vector3(wallX, 0f, wallZ);
            GameObject o = Instantiate(floorCube, position, Quaternion.identity);
            o.transform.localScale += new Vector3(0f, wallHeight, 0f);
            o.transform.SetParent(transform);
            wallX -= 5f;
        }

        wallX += 5f;
        wallZ -= 5f;
        for (int i = 0; i < levelLength + 1f; i++)
        {
            Vector3 position = new Vector3(wallX, 0f, wallZ);
            GameObject o = Instantiate(floorCube, position, Quaternion.identity);
            o.transform.localScale += new Vector3(0f, wallHeight, 0f);
            o.transform.SetParent(transform);
            wallZ -= 5f;
        }

        /*Lastly, create the ceiling
        Vector3 ceilingPos = new Vector3(0f, 2.5f + wallHeight/2, 0f);
        GameObject ceiling = Instantiate(ceilingCube, ceilingPos, Quaternion.identity);
        ceiling.transform.localScale += new Vector3(levelWidth*5.5f, 0.0f, levelLength*5.5f);*/
    }
}
