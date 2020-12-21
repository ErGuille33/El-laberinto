using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public MatrixCasillas mat;
    public TextAsset level;

    protected LevelData lvlData;

    // Start is called before the first frame update
    void Start()
    {
        cargaJson();
    }

    void cargaJson()
    {
        lvlData = JsonUtility.FromJson<LevelData>(level.ToString());

        for(int i = 0; i < lvlData.h.Length; i++)
        {
            if (lvlData.h[i].x < 0.8)
            {
                lvlData.h[i].x = 0;
            }
            if (lvlData.h[i].y < 0.8)
            {
                lvlData.h[i].y = 0;
            }
        }


 
        print("WAlls1 " + lvlData.h[0].x);
        print("WAlls2 " + lvlData.h[0].y);

    }

    // Update is called once per frame
    void Update()
    {

    }
    //r es la altura
    //C es la ancura 
    //s es la salida (0,0) abajo izquieda)
    //f es el  final
    //h es el array de pistas, o: origen, d:destino
    //EL resto no tengo ni reputa idea
    [Serializable]
    protected class LevelData{
        public int r;
        public int c;
        public Vector2 s;
        public Vector2 f;
    
        public Vector2[] w = new Vector2[50];
        public Vector2[] h = new Vector2[2];

        public Vector2[] i = new Vector2[2];
        public Vector2[] e = new Vector2[2];
        public Vector2[] t = new Vector2[2];
    }
}
