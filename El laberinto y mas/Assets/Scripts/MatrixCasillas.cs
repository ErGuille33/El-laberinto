using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCasillas : MonoBehaviour
{
    public int numCasillasX;
    public int numCasillasY;

    public GameObject prefabCasilla;
    public GameObject[,] casillas;

    public float widthCasilla;
    public float heigthCasilla;

    // Start is called before the first frame update
    void Start()
    {
        casillas = new GameObject[numCasillasX, numCasillasY];
        for(int i = 0; i < numCasillasX; i++)
        {
            for(int j = 0; j < numCasillasY; j++)
            {
                casillas[i, j] = Instantiate(prefabCasilla);
                casillas[i, j].transform.SetParent(transform);
               
                widthCasilla = prefabCasilla.GetComponent<Casilla>()._width;
                heigthCasilla = prefabCasilla.GetComponent<Casilla>()._heigth;
                casillas[i, j].transform.position = new Vector2(transform.position.x + (i * widthCasilla/1), transform.position.y - (j * heigthCasilla/1));

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
