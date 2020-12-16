using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCasillas : MonoBehaviour
{
    public int numCasillasX;
    public int numCasillasY;

    public GameObject prefabCasilla;
    public GameObject prefabWall;
    public GameObject[,] casillas;

    List<GameObject> walls = new List<GameObject>();
    private int countWalls = 0;

    public float widthCasilla;
    public float heigthCasilla;


    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        casillas = new GameObject[numCasillasX, numCasillasY];

        prefabCasilla.GetComponent<Casilla>()._width = (5f / numCasillasX);
        prefabCasilla.GetComponent<Casilla>()._heigth = (5f / numCasillasX);

        widthCasilla = prefabCasilla.GetComponent<Casilla>()._width;
        heigthCasilla = prefabCasilla.GetComponent<Casilla>()._heigth;

        transform.position = new Vector2((widthCasilla / 2) - ((numCasillasX * widthCasilla) / 2), -(heigthCasilla / 2) + ((numCasillasY * heigthCasilla) / 2));

        for (int i = 0; i < numCasillasX; i++)
        {
            for(int j = 0; j < numCasillasY; j++)
            {
                casillas[i, j] = Instantiate(prefabCasilla,transform);
                casillas[i, j].transform.position = new Vector2(transform.position.x + (i * widthCasilla), transform.position.y - (j * heigthCasilla));

                for(int l = 0; l < 4; l++)
                {
                    if (casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[l] == true)
                    {
                        walls.Add(Instantiate(prefabWall, casillas[i, j].transform));
                        if(l == 0 || l == 2)
                        {
                            walls[countWalls].transform.localScale = new Vector2(widthCasilla, (float)(numCasillasX * 0.005));
                            if(l == 0)
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x, casillas[i, j].transform.position.y + heigthCasilla/2);
                            else if(l == 2)
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x, casillas[i, j].transform.position.y - heigthCasilla/2);
                        }
                        else
                        {
                            walls[countWalls].transform.localScale = new Vector2((float)(numCasillasX * 0.005), heigthCasilla);
                            if (l == 1)
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x - heigthCasilla/2, casillas[i, j].transform.position.y );
                            else if (l == 3)
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x + heigthCasilla/2, casillas[i, j].transform.position.y );
                        }
                       
                        countWalls++;
                    }   
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
