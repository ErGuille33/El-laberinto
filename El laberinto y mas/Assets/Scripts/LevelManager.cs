using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public MatrixCasillas mat;
    public TextAsset level;

    protected LevelData lvlData;
    protected WallsData[] walls;
    protected bool[,,] wallsArray = new bool [250, 250, 4];
    protected bool[,] isIcedarray = new bool[250, 250];

    // Start is called before the first frame update
    void Start()
    {
        cargaJson();
    }


    //Para poner el nivel 
    public void setTextAsset(TextAsset lvl)
    {
        level = lvl;
    }

    protected void setWallsArray()
    {
        int auxInvertedCoord = lvlData.r ;

        for (int i = 0; i < wallsArray.GetLength(0); i++)
        {
            for (int j = 0; j < wallsArray.GetLength(1); j++)
            {
                for (int l = 0; l < wallsArray.GetLength(2); l++)
                {/*
                    if ((i == 0 && l == 3) || (i == wallsArray.GetLength(0) && l == 1) || (j == 0 && l == 0) || (j == wallsArray.GetLength(1) && l == 2))
                    {
                        wallsArray[i, j, l] = false;
                    }
                    else*/ wallsArray[i, j, l] = true;
                }
            }
        }

        for (int i = 0; i < walls.Length; i++)
        {
            print("antes: " + walls[i].o);
            print("despues " + walls[i].d);

            if (walls[i].o.y !=  walls[i].d.y )
            {
                if ((int)walls[i].o.x == 0)
                {
                        wallsArray[(int)walls[i].o.x, auxInvertedCoord - (int)walls[i].o.y, 1] = false;
             
                }
                else
                {
                        wallsArray[(int)walls[i].o.x-1, auxInvertedCoord - (int)walls[i].o.y, 3] = false;
                }
            }

            if (walls[i].o.x != walls[i].d.x)
            {
                if ((int)walls[i].o.y == auxInvertedCoord)
                {
                    wallsArray[(int)walls[i].o.x, auxInvertedCoord - (int)walls[i].o.y, 0] = false;

                }
                else
                {
                    wallsArray[(int)walls[i].o.x , auxInvertedCoord-1 - (int)walls[i].o.y, 2] = false;
                }
            }
        }
    }

    protected void setIcedArray()
    {
        for (int i = 0; i < wallsArray.GetLength(0); i++)
        {
            for (int j = 0; j < wallsArray.GetLength(1); j++)
            {
                isIcedarray[i, j] = false;
            }
        }

        for(int i = 0; i < lvlData.i.Length; i++)
        {
            isIcedarray[(int)lvlData.i[i].x, (int)lvlData.i[i].y] = true;
        }
    }

    void cargaJson()
    {
        lvlData = JsonUtility.FromJson<LevelData>(level.ToString());

        walls = lvlData.w;

        for(int i = 0; i < lvlData.h.Length; i++)
        {
            if (lvlData.h[i].x < 0.5)
            {
                lvlData.h[i].x = 0;
            }
            if (lvlData.h[i].y < 0.5)
            {
                lvlData.h[i].y = 0;
            }
        }
       /* print("WAllsrx " + lvlData.r);
        print("WAlls1x " + lvlData.s.x);
        print("WAlls1y " + lvlData.s.y);

        print("WAlls2x " + lvlData.mx);
        print("WAlls2y " + lvlData.my);

        print("owo " + walls[0].o.x);
        print("owo " + walls[0].o.y);*/

        setWallsArray();
        setIcedArray();

        mat.createNewMap(lvlData.r, lvlData.c, wallsArray, isIcedarray);
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
        public bool my;
        public bool mx;
        public Vector2 s;
        public Vector2 f;

        public WallsData[] w = new WallsData[100];
        public Vector2[] h = new Vector2[100];
        public Vector2[] i = new Vector2[2];
        public Vector2[] e = new Vector2[2];
        public Vector2[] t = new Vector2[2];
    }

    [Serializable]
    public class WallsData
    {
        
        public Vector2 o;
        public Vector2 d;

    }
}
