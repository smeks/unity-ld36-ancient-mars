using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleRoomController : MonoBehaviour {
    public float ytranslate = 0.0f;
    public int translateSteps = 0;

    public GameObject platformPrefab;

    private float tileWidth = 20.0f;
    private float tileHeight = 10.0f;

    private int[,] roomMap = new int[,] {
        { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 1,1,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 1,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 1,0,0,1,0,0,0,0,0,0,0,0,0,0,0 },
        { 1,0,0,0,1,0,0,0,0,0,0,0,0,0,0 },
        { 1,0,0,0,0,1,0,0,0,0,0,0,0,0,0 },
        { 1,0,0,0,0,0,1,0,0,0,0,0,0,0,0 },
        { 1,0,0,0,0,0,0,1,0,0,0,0,0,0,0 },
        { 1,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
        { 1,0,0,0,0,0,0,0,0,1,0,0,0,0,0 },
        { 1,0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
        { 1,0,0,0,0,0,0,0,0,0,0,1,0,0,0 },
        { 1,0,0,0,0,0,0,0,0,0,0,0,1,0,0 },
        { 1,0,0,0,0,0,0,0,0,0,0,0,0,1,0 },
        { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
    };

	// Use this for initialization
	void Start () {


        for(int x=0; x<15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if(roomMap[x,y] == 1)
                {
                    var xoffset = -14.5f * tileWidth;
                    xoffset += 7.5f * tileWidth;
                    xoffset -= y * (tileWidth/2);

                    var xPos = x * tileWidth + xoffset;
                    var yPos = y * tileHeight;

                    var newPos = new Vector3(xPos, yPos, 0);

                    Instantiate(platformPrefab, newPos, Quaternion.identity);

                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
