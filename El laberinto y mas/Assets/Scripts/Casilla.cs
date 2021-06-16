using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public GameObject[] hintLine = new GameObject[4];

    public GameObject[] paths = new GameObject[4];

    public GameObject[] walls = new GameObject[4];

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private SpriteRenderer[] pathsRenderer = new SpriteRenderer[4];




    public void setIced (bool iced) { _isIced = iced; }
    public bool getIced() { return _isIced; }

    public void setAdyacent(bool[] adyacent) { _casillaAdyacente = adyacent; }
    public bool[] getAdyacent() { return _casillaAdyacente; }
    // Start is called before the first frame update
    void Start()
    {
        //Escalamos la casilla y dibujamos con sus caracteristicas

        _width = 1;
        _heigth = 1;
        transform.localScale = new Vector2( _width,_heigth);

        if (renderer != null)
        {
            if (!_isIced)
            {
                renderer.enabled = false;
            }
            else renderer.enabled = true;
        }

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

    public void changePathsColor( Color color)
    {
        for(int i = 0; i < 4; i++)
        {
            pathsRenderer[i].color = color;
        }
    }
}
