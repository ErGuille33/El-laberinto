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
    public GameObject[,] casillas;
    //Casilla que corresponde al final
    public Casilla endCasilla;
    public Casilla startCasilla;
    //Posicion del jugador
    public int playerXPos, playerYPos;

    public float widthCasilla = 0;
    public float heigthCasilla = 0;

    public Camera _cam;

    float sizeCasilla;




    //Resetear mapa y borrar todas las casillas
    public void resetMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
      
        endCasilla = null;
    }
    //En estos metodos hacemos Find () ,pero hay que tener en cuenta en cuenta que estamos buscando a un hijo de casilla que va a existir siempre ya que forma parte del prefab
    //por lo tanto no se va a dar el caso en el que de problemas. Somos conscientes de que en otros contextos puede causar problemas, pero como en este no decidimos activamente usarlo ya que facilita ligreramente el código
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
                casillas[casillaX, casillaY].transform.Find("HintLine").gameObject.SetActive(set); 
                break;
            case 1:
                casillas[casillaX, casillaY].transform.Find("HintLine 1").gameObject.SetActive(set);
                break;
            case 2:
                casillas[casillaX, casillaY].transform.Find("HintLine 2").gameObject.SetActive(set);
                break;
            case 3:
                casillas[casillaX, casillaY].transform.Find("HintLine 3").gameObject.SetActive(set);
                break;
        }
        switch (to)
        {
            case 0:
                casillas[casillaX, casillaY].transform.Find("HintLine").gameObject.SetActive(set);
                break;
            case 1:
                casillas[casillaX, casillaY].transform.Find("HintLine 1").gameObject.SetActive(set);
                break;
            case 2:
                casillas[casillaX, casillaY].transform.Find("HintLine 2").gameObject.SetActive(set);
                break;
            case 3:
                casillas[casillaX, casillaY].transform.Find("HintLine 3").gameObject.SetActive(set);
                break;
        }

    }
    //Método que activa o desactiva el rastro del jugador en una casilla
    public void setPlayerPath(int casillaX, int casillaY, int from)
    {

        switch (from)
        {
            case 0:
                if (casillas[casillaX, casillaY].transform.Find("PlayerPath").gameObject.activeSelf)
                    casillas[casillaX, casillaY].transform.Find("PlayerPath").gameObject.SetActive(false);
                else casillas[casillaX, casillaY].transform.Find("PlayerPath").gameObject.SetActive(true);
                break; 
            case 1:
                if (casillas[casillaX, casillaY].transform.Find("PlayerPath 1").gameObject.activeSelf)
                    casillas[casillaX, casillaY].transform.Find("PlayerPath 1").gameObject.SetActive(false);
                else casillas[casillaX, casillaY].transform.Find("PlayerPath 1").gameObject.SetActive(true);
                break;
            case 2:
                if (casillas[casillaX, casillaY].transform.Find("PlayerPath 2").gameObject.activeSelf)
                    casillas[casillaX, casillaY].transform.Find("PlayerPath 2").gameObject.SetActive(false);
                else casillas[casillaX, casillaY].transform.Find("PlayerPath 2").gameObject.SetActive(true);
                break;
            case 3:
                if (casillas[casillaX, casillaY].transform.Find("PlayerPath 3").gameObject.activeSelf)
                    casillas[casillaX, casillaY].transform.Find("PlayerPath 3").gameObject.SetActive(false);
                else casillas[casillaX, casillaY].transform.Find("PlayerPath 3").gameObject.SetActive(true);
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

            orthSize = unitsPerPixel * Screen.height * 0.5f * 1.25f;

        }

        _cam.orthographicSize = orthSize;
    }
    //Método que recibe los datos necesarios para crear el tablero, y lo crea.
    public void createNewMap(int rows, int cols, bool[,,] wallsArray, bool[,] isIced, Vector2 isEnd, Vector2 isStart, Color color)
    {
        numCasillasX = cols;
        numCasillasY = rows;
        casillas = new GameObject[numCasillasX, numCasillasY];

        prefabCasilla.GetComponent<Casilla>()._width = 1;
        prefabCasilla.GetComponent<Casilla>()._heigth = 1;

        sizeCasilla = 1;

        widthCasilla = prefabCasilla.GetComponent<Casilla>()._width;
        heigthCasilla = prefabCasilla.GetComponent<Casilla>()._heigth;


        transform.position = new Vector2((widthCasilla / 2) - ((numCasillasX * widthCasilla) / 2), -(heigthCasilla / 2) + ((numCasillasY * heigthCasilla) / 2));

        for (int i = 0; i < numCasillasX; i++)
        {
            for (int j = 0; j < numCasillasY; j++)
            {
                casillas[i, j] = Instantiate(prefabCasilla, transform);

                casillas[i, j].transform.Find("PlayerPath").GetComponent<SpriteRenderer>().color = color;

                casillas[i, j].transform.Find("PlayerPath 1").GetComponent<SpriteRenderer>().color = color;


                casillas[i, j].transform.Find("PlayerPath 2").GetComponent<SpriteRenderer>().color = color;


                casillas[i, j].transform.Find("PlayerPath 3").GetComponent<SpriteRenderer>().color = color;


                casillas[i, j].transform.position = new Vector2(transform.position.x + (i * widthCasilla), transform.position.y - (j * heigthCasilla));
                casillas[i, j].GetComponent<Casilla>()._isIced = isIced[i, j];
                if (i == isEnd.x && j == isEnd.y)
                {

                    casillas[i, j].GetComponent<Casilla>()._end = true;
                    endCasilla = casillas[i, j].GetComponent<Casilla>();


                }
                else casillas[i, j].GetComponent<Casilla>()._end = false;

                if (i == isStart.x && j == isStart.y)
                {
                    
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

                                casillas[i, j].transform.Find("Wall").gameObject.SetActive(true);

                            }

                            else if (l == 2)
                            {
                                casillas[i, j].transform.Find("Wall 2").gameObject.SetActive(true);
                            }

                        }
                        else
                        {

                            if (l == 1)
                            {
                                casillas[i, j].transform.Find("Wall 1").gameObject.SetActive(true);
                            }

                            else if (l == 3)
                            {
                                casillas[i, j].transform.Find("Wall 3").gameObject.SetActive(true);
                            }

                        }
                        //casilla superior derecha
                        if(i == numCasillasX - 1 && j == 0)
                        {
                            casillas[i, j].transform.Find("Wall").gameObject.SetActive(true);
                            casillas[i, j].transform.Find("Wall 1").gameObject.SetActive(true);
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[1] = false;
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[0] = false;
                        }
                        //Casilla superior izquierda
                        else if (i == 0 && j == 0)
                        {
                            casillas[i, j].transform.Find("Wall").gameObject.SetActive(true);
                            casillas[i, j].transform.Find("Wall 3").gameObject.SetActive(true);
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[0] = false;
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[3] = false;
                        }
                        //Casilla inferior izquierda
                        else if (i == 0 && j == numCasillasY - 1)
                        {
                            casillas[i, j].transform.Find("Wall 2").gameObject.SetActive(true);
                            casillas[i, j].transform.Find("Wall 3").gameObject.SetActive(true);
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[2] = false;
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[3] = false;
                        }

                        //Casilla inferior derecha
                        else if (i == numCasillasX - 1 && j == numCasillasY - 1)
                        {
                            casillas[i, j].transform.Find("Wall 2").gameObject.SetActive(true);
                            casillas[i, j].transform.Find("Wall 1").gameObject.SetActive(true);
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[2] = false;
                            casillas[i, j].GetComponent<Casilla>()._casillaAdyacente[3] = false;
                        }

                    }
                }
            }
        }

        adaptToResolution(cols, rows);

    }
}
