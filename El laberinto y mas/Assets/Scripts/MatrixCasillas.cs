using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatrixCasillas : MonoBehaviour
{
    public int numCasillasX;
    public int numCasillasY;

    public GameObject prefabCasilla;
    public GameObject prefabWall;
    public GameObject[,] casillas;
    public GameObject hintLine;

    public int playerXPos, playerYPos;

    List<GameObject> walls = new List<GameObject>();
    List<GameObject> hints = new List<GameObject>();
    private int countWalls = 0;

    public float widthCasilla;
    public float heigthCasilla;

    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        //Camera cam = Camera.main;
        // transform.localScale = new Vector2(cam.pixelWidth/5, cam.pixelWidth/5);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHints(bool [,] _hints, bool[,] _hintsDir)
    {
        int countHints = 0;
        for (int i = 0; i < numCasillasX; i++)
        {
            for (int j = 0; j < numCasillasY; j++)
            {
                if (_hints[i, j] == true)
                {
                    hints.Add(Instantiate(hintLine, casillas[i, j].transform));
                    hints[countHints].transform.position = new Vector2(casillas[i, j].transform.position.x, casillas[i, j].transform.position.y);
                    hints[countHints].transform.localScale = new Vector2((float)(numCasillasX * 0.0176), (float)(numCasillasX * 0.02));
                    if(_hintsDir[i, j] == true)
                    {
                        hints[countHints].transform.Rotate(0,0,90);
                    }
                    countHints++;
                   
                }
            }
        }
    }

    public void createNewMap(int rows, int cols, bool[,,] wallsArray, bool [,] isIced, Vector2 isEnd, Vector2 isStart)
    {
        numCasillasX = cols;
        numCasillasY = rows;
        casillas = new GameObject[numCasillasX, numCasillasY];

        prefabCasilla.GetComponent<Casilla>()._width = (5.3f / numCasillasX);
        prefabCasilla.GetComponent<Casilla>()._heigth = (5.3f / numCasillasX);


        widthCasilla = prefabCasilla.GetComponent<Casilla>()._width;
        heigthCasilla = prefabCasilla.GetComponent<Casilla>()._heigth;

        transform.position = new Vector2((widthCasilla / 2) - ((numCasillasX * widthCasilla) / 2), -(heigthCasilla / 2) + ((numCasillasY * heigthCasilla) / 2));

        for (int i = 0; i < numCasillasX; i++)
        {
            for (int j = 0; j < numCasillasY; j++)
            {
                casillas[i, j] = Instantiate(prefabCasilla, transform);

                casillas[i, j].transform.position = new Vector2(transform.position.x + (i * widthCasilla), transform.position.y - (j * heigthCasilla));
                casillas[i, j].GetComponent<Casilla>()._isIced = isIced[i, j];
                if (i == isEnd.x && j == isEnd.y)
                {
                    casillas[i, j].GetComponent<Casilla>()._end = true;
                }
                else casillas[i, j].GetComponent<Casilla>()._end = false;

                if (i == isStart.x && j == isStart.y)
                {
                    levelManager.playerCasilla = casillas[i, j].GetComponent<Casilla>();
                    playerXPos = i;
                    playerYPos = j;
                }

                for (int l = 0; l < 4; l++)
                {
                    casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[l] = wallsArray[i, j, l];
                    if (casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[l] == false)
                    {
                        if (l == 0 || l == 2)
                        {
                            if (l == 0)
                            {
                                walls.Add(Instantiate(prefabWall, casillas[i, j].transform));
                                walls[countWalls].transform.localScale = new Vector2(1, (float)(numCasillasX * 0.01));
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x, casillas[i, j].transform.position.y + 0.5f);
                                
                                countWalls++;
                            }

                            else if (l == 2)
                            {
                                walls.Add(Instantiate(prefabWall, casillas[i, j].transform));
                                walls[countWalls].transform.localScale = new Vector2(1, (float)(numCasillasX * 0.01));
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x, casillas[i, j].transform.position.y - 0.5f);
                                countWalls++;
                            }
                         
                        }
                        else
                        {

                            if (l == 1)
                            {
                                walls.Add(Instantiate(prefabWall, casillas[i, j].transform));
                                walls[countWalls].transform.localScale = new Vector2((float)(numCasillasX * 0.01), 1);
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x + 0.5f, casillas[i, j].transform.position.y);
                                countWalls++;
                            }

                            else if (l == 3)
                            {
                                walls.Add(Instantiate(prefabWall, casillas[i, j].transform));
                                walls[countWalls].transform.localScale = new Vector2((float)(numCasillasX * 0.01), 1);
                                walls[countWalls].transform.position = new Vector2(casillas[i, j].transform.position.x - 0.5f, casillas[i, j].transform.position.y);
                                countWalls++;
                            }
                            
                        }


                    }
                }
            }
        }
    }
}
