using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//Método consistente en preparar el tablero de juego
public class MatrixCasillas : MonoBehaviour
{
    //Número de casillas en x e Y
    public int numCasillasX;
    public int numCasillasY;
    //Prefabs a usar
    public GameObject prefabCasilla;
    public GameObject prefabWall;
    //Array de casillas
    public Casilla[,] casillas;
    //Casilla que corresponde al final
    public Casilla endCasilla;
    public Casilla startCasilla;
    //Posicion del jugador
    public int playerXPos, playerYPos;

    public float widthCasilla = 0;
    public float heigthCasilla = 0;

    public Camera _cam;



    //Resetear mapa y borrar todas las casillas
    public void resetMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
      
        endCasilla = null;
    }
    
    //Método que activa o desactiva las pistas en una casilla
    public void setHints(int casillaX, int casillaY, int from, int to, bool set)
    {
        if (casillaX < 0)
            casillaX = 0;
        if (casillaY < 0)
            casillaY = 0;


        switch (from)
        {
            case 0:
                casillas[casillaX, casillaY].hintLine[0].SetActive(set); 
                break;
            case 1:
                casillas[casillaX, casillaY].hintLine[1].SetActive(set);
                break;
            case 2:
                casillas[casillaX, casillaY].hintLine[2].SetActive(set);
                break;
            case 3:
                casillas[casillaX, casillaY].hintLine[3].SetActive(set);
                break;
        }
        switch (to)
        {
            case 0:
                casillas[casillaX, casillaY].hintLine[0].SetActive(set);
                break;
            case 1:
                casillas[casillaX, casillaY].hintLine[1].SetActive(set);
                break;
            case 2:
                casillas[casillaX, casillaY].hintLine[2].SetActive(set);
                break;
            case 3:
                casillas[casillaX, casillaY].hintLine[3].SetActive(set);
                break;
        }

    }
    //Método que activa o desactiva el rastro del jugador en una casilla
    public void setPlayerPath(int casillaX, int casillaY, int from)
    {

        switch (from)
        {
            case 0:
                if (casillas[casillaX, casillaY].paths[0].gameObject.activeSelf)
                    casillas[casillaX, casillaY].paths[0].gameObject.SetActive(false);
                else casillas[casillaX, casillaY].paths[0].gameObject.SetActive(true);
                break; 
            case 1:
                if (casillas[casillaX, casillaY].paths[1].gameObject.activeSelf)
                    casillas[casillaX, casillaY].paths[1].gameObject.SetActive(false);
                else casillas[casillaX, casillaY].paths[1].gameObject.SetActive(true);
                break;
            case 2:
                if (casillas[casillaX, casillaY].paths[2].gameObject.activeSelf)
                    casillas[casillaX, casillaY].paths[2].gameObject.SetActive(false);
                else casillas[casillaX, casillaY].paths[2].gameObject.SetActive(true);
                break;
            case 3:
                if (casillas[casillaX, casillaY].paths[3].gameObject.activeSelf)
                    casillas[casillaX, casillaY].paths[3].gameObject.SetActive(false);
                else casillas[casillaX, casillaY].paths[3].gameObject.SetActive(true);
                break;
        }

    }
    //Sólo se adaptara a la resolución del dispositivo sólo al crear cada mapa. Lo haremos cambiando el tamaño de la camara ortográfica
    void adaptToResolution(int _cols, int _rows)
    {
        _cam.orthographic = true;

        float unitsPerPixel;
        float orthSize;

        if (Math.Abs(Screen.width - Screen.height) > 400){
            if (Screen.width > Screen.height)
            {
                unitsPerPixel = (float)_rows / (float)Screen.height;

                orthSize = unitsPerPixel * Screen.width * 0.5f;

            }
            else 
            {
                unitsPerPixel = (float)_rows / (float)Screen.width;

                orthSize = unitsPerPixel * Screen.height * 0.5f;

            }
        }
        else
        {
            unitsPerPixel = (float)_rows / (float)Screen.width;

            orthSize = unitsPerPixel * Screen.height * 0.5f * 1.5f;

        }

        _cam.orthographicSize = orthSize;
    }
    //Método que recibe los datos necesarios para crear el tablero, y lo crea.
    public void createNewMap(int rows, int cols, bool[,,] wallsArray, bool[,] isIced, Vector2 isEnd, Vector2 isStart, Color color)
    {
        numCasillasX = cols;
        numCasillasY = rows;
        casillas = new Casilla[numCasillasX, numCasillasY];

        widthCasilla = 1;
        heigthCasilla = 1;


        transform.position = new Vector2((widthCasilla / 2) - ((numCasillasX * widthCasilla) / 2), -(heigthCasilla / 2) + ((numCasillasY * heigthCasilla) / 2));

        for (int i = 0; i < numCasillasX; i++)
        {
            for (int j = 0; j < numCasillasY; j++)
            {
                casillas[i, j] = Instantiate(prefabCasilla, transform).GetComponent<Casilla>();

                casillas[i, j].changePathsColor(color);


                casillas[i, j].transform.position = new Vector2(transform.position.x + (i * widthCasilla), transform.position.y - (j * heigthCasilla));
                casillas[i, j]._isIced = isIced[i, j];
                if (i == isEnd.x && j == isEnd.y)
                {

                    casillas[i, j]._end = true;
                    endCasilla = casillas[i, j];


                }
                else casillas[i, j]._end = false;

                if (i == isStart.x && j == isStart.y)
                {
                    
                    playerXPos = i;
                    playerYPos = j;
                }

                for (int l = 0; l < 4; l++)
                {
                    casillas[i, j]._casillaAdyacente[l] = wallsArray[i, j, l];
                    if (casillas[i, j]._casillaAdyacente[l] == false)
                    {
                        if (l == 0 || l == 2)
                        {
                            if (l == 0)
                            {

                                casillas[i, j].walls[0].gameObject.SetActive(true);

                            }

                            else if (l == 2)
                            {
                                casillas[i, j].walls[2].gameObject.SetActive(true);
                            }

                        }
                        else
                        {

                            if (l == 1)
                            {
                                casillas[i, j].walls[1].gameObject.SetActive(true);
                            }

                            else if (l == 3)
                            {
                                casillas[i, j].walls[3].gameObject.SetActive(true);
                            }

                        }
                        //casilla superior derecha
                        if(i == numCasillasX - 1 && j == 0)
                        {
                            casillas[i, j].walls[0].gameObject.SetActive(true);
                            casillas[i, j].walls[1].gameObject.SetActive(true);
                            casillas[i, j]._casillaAdyacente[1] = false;
                            casillas[i, j]._casillaAdyacente[0] = false;
                        }
                        //Casilla superior izquierda
                        else if (i == 0 && j == 0)
                        {
                            casillas[i, j].walls[0].gameObject.SetActive(true);
                            casillas[i, j].walls[3].gameObject.SetActive(true);
                            casillas[i, j]._casillaAdyacente[0] = false;
                            casillas[i, j]._casillaAdyacente[3] = false;
                        }
                        //Casilla inferior izquierda
                        else if (i == 0 && j == numCasillasY - 1)
                        {
                            casillas[i, j].walls[2].gameObject.SetActive(true);
                            casillas[i, j].walls[3].gameObject.SetActive(true);
                            casillas[i, j]._casillaAdyacente[2] = false;
                            casillas[i, j]._casillaAdyacente[3] = false;
                        }

                        //Casilla inferior derecha
                        else if (i == numCasillasX - 1 && j == numCasillasY - 1)
                        {
                            casillas[i, j].walls[2].gameObject.SetActive(true);
                            casillas[i, j].walls[1].gameObject.SetActive(true);
                            casillas[i, j]._casillaAdyacente[2] = false;
                            casillas[i, j]._casillaAdyacente[3] = false;
                        }

                    }
                }
            }
        }

        adaptToResolution(cols, rows);

    }
}
