using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{

    public bool _isIced = false;

    public bool[] _casillaAdyacente = { true, true, true, true }; //Array de las casillas adyacentes: Arriba, derecha, abajo, izquierda. True = libre, False = ocupada

    public float _width;
    public float _heigth;


        
    public void setIced (bool iced) { _isIced = iced; }
    public bool getIced(bool iced) { return _isIced; }

    public void setAdyacent(bool[] adyacent) { _casillaAdyacente = adyacent; }
    public bool[] getAdyacent() { return _casillaAdyacente; }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2( _width,_heigth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
