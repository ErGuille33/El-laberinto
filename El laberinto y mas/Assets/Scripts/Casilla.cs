using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Este script consta de la informacion que contiene cada casilla
public class Casilla : MonoBehaviour
{
    //SI es helada
    public bool _isIced;
    //Si es la casilla de meta
    public bool _end;

    public bool[] _casillaAdyacente = { true, false, true, true }; //Array de las casillas adyacentes: Arriba, derecha, abajo, izquierda. True = libre, False = ocupada
    //Ancho y alto
    public float _width;
    public float _heigth;
    //Sprite de acabado
    public GameObject endSprite;


    public void setIced (bool iced) { _isIced = iced; }
    public bool getIced() { return _isIced; }

    public void setAdyacent(bool[] adyacent) { _casillaAdyacente = adyacent; }
    public bool[] getAdyacent() { return _casillaAdyacente; }
    // Start is called before the first frame update
    void Start()
    {
        //Escalamos la casilla y dibujamos con sus caracteristicas
        transform.localScale = new Vector2( _width,_heigth);
        if (!_isIced)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else GetComponent<SpriteRenderer>().enabled = true;

        if (_end)
        {
            Instantiate(endSprite, transform);
        }
    }

    public int getSalidas()
    {
        int aux = 0;
        if (_casillaAdyacente[0]) aux++;
        if (_casillaAdyacente[1]) aux++;
        if (_casillaAdyacente[2]) aux++;
        if (_casillaAdyacente[3]) aux++;
        return aux;
    }
}
