using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    //[SerializeField] private HexTile _tile;
    public GameObject hexPrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float hexSize = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateHexGrid();  
    }

    void GenerateHexGrid()
    {
        float hexWidth = hexSize;
        float hexHeight = hexSize;

        for (int x = -2; x <= 2; x++)
        {
            if (x == -2 || x == 2)
            {
                for (int y = -1; y <= 1; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight + (x % 2 == 0 ? 0 : hexHeight / 2);

                    GameObject hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    Debug.Log(x + " " + y);
                    Debug.Log(xPos + " " + yPos);
                }
            }

            else if (x == -1 || x == 1)
            {
                for (int y = -2; y <= 1; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight + (x % 2 == 0 ? 0 : hexHeight / 2);

                    GameObject hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    Debug.Log(x + " " + y);
                    Debug.Log(xPos + " " + yPos);
                }
            }

            else
            {
                for (int y = -2; y <= 2; y++)
                {
                    float xPos = x * hexWidth;
                    float yPos = y * hexHeight + (x % 2 == 0 ? 0 : hexHeight / 2);

                    GameObject hex = Instantiate(hexPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                    hex.transform.parent = this.transform;
                    hex.name = $"Hex_{x}_{y}";

                    Debug.Log(x + " " + y);
                    Debug.Log(xPos + " " + yPos);
                }
            }



        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
