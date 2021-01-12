using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public MatrixCasillas mat;
    public TextAsset level;

    protected LevelData lvlData;
    protected WallsData[] walls;
    protected bool[,,] wallsArray = new bool [500, 500, 4];
    protected bool[,] isIcedarray = new bool[500, 500];
    protected bool[,] hintsArray = new bool[500, 500];
    //False es horizontal, true vertical
    protected int[,,] hintsDir = new int[500, 500, 2];

    public Casilla playerCasilla;

    Vector2 endCasilla;
    Vector2 startCasilla;

    private int auxInvertedCoord;
    private int auxTotalCols;

    void Awake()
    {
  
    }
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
           

            if (walls[i].o.y !=  walls[i].d.y )
            {
                if ((int)walls[i].o.x == 0)
                {
                        wallsArray[(int)walls[i].o.x, auxInvertedCoord - (int)walls[i].o.y, 3] = false;
                

                }
                else
                {
                    if ((int)walls[i].o.x == auxTotalCols+1) {
                        wallsArray[(int)walls[i].o.x, auxInvertedCoord - (int)walls[i].o.y, 1] = false; 
                    }
                    else
                    {
                        wallsArray[(int)walls[i].o.x - 1, auxInvertedCoord - (int)walls[i].o.y, 1] = false;
                        wallsArray[(int)walls[i].o.x, auxInvertedCoord - (int)walls[i].o.y, 3] = false;
                    }
                       
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
                    wallsArray[(int)walls[i].o.x, auxInvertedCoord - (int)walls[i].o.y, 0] = false;
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
            if ((int)lvlData.i[i].y == auxInvertedCoord)
            {
                isIcedarray[(int)lvlData.i[i].x, auxInvertedCoord - (int)lvlData.i[i].y] = true;

            }
            else
            {
                isIcedarray[(int)lvlData.i[i].x, auxInvertedCoord - 1 - (int)lvlData.i[i].y] = true;
            }
       
        }
    }

    protected void setHintsArray()
    {

        for (int i = 0; i < hintsArray.GetLength(0); i++)
        {
            for (int j = 0; j < hintsArray.GetLength(1); j++)
            {
        
                for (int l = 0; l < hintsDir.GetLength(2); l++)
                {
                    hintsArray[i, j] = false;
                    hintsDir[i, j,l] = 1;
                }
            }
        }

        for (int i = 0; i < lvlData.h.Length; i++)
        {
            if (lvlData.h[i].y == auxInvertedCoord)
            {
                hintsArray[(int)lvlData.h[i].x, auxInvertedCoord - (int)lvlData.h[i].y] = true;

            }
            else
            {
                hintsArray[(int)lvlData.h[i].x, auxInvertedCoord - 1  - (int)lvlData.h[i].y] = true;
            }
            if (i < lvlData.h.Length - 1)
            {
                if (lvlData.h[i].x > lvlData.h[i + 1].x)
                {

                }
            }
        }
    }

    protected void setEnd()
    {
        if(lvlData.f.y == auxInvertedCoord)
        {
            endCasilla = new Vector2(lvlData.f.x, auxInvertedCoord - lvlData.f.y);
        }
        else endCasilla = new Vector2(lvlData.f.x, auxInvertedCoord - 1 - lvlData.f.y);

    }

    protected void setStart()
    {
        if (lvlData.s.y == auxInvertedCoord)
        {
            startCasilla = new Vector2(lvlData.s.x, auxInvertedCoord - lvlData.s.y);
        }
        else startCasilla = new Vector2(lvlData.s.x, auxInvertedCoord - 1 - lvlData.s.y);

    }

    public Vector3 getStart()
    {
        return new Vector3(startCasilla.x, startCasilla.y, 0);
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

        auxInvertedCoord = lvlData.r;
        auxTotalCols = lvlData.c;

        setWallsArray();
        setIcedArray();
        setHintsArray();
        setEnd();
        setStart();

        mat.createNewMap(lvlData.r, lvlData.c, wallsArray, isIcedarray,endCasilla,startCasilla);
        mat.setHints(hintsArray,hintsDir);
    }

    public void MovePlayer(PlayerControl.Dir dir)
    {
        switch(dir)
        {
            case PlayerControl.Dir.UP:
                if (playerCasilla._casillaAdyacente[0])
                {
                    playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos - 1].GetComponent<Casilla>();
                    mat.playerYPos--;
                    playerMoveUp();
                }
                break;
            case PlayerControl.Dir.DOWN:
                if (playerCasilla._casillaAdyacente[2])
                {
                    playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos + 1].GetComponent<Casilla>();
                    mat.playerYPos++;
                    playerMoveDown();
                }
                break;
            case PlayerControl.Dir.RIGHT:
                if (playerCasilla._casillaAdyacente[1])
                {
                    playerCasilla = mat.casillas[mat.playerXPos + 1, mat.playerYPos].GetComponent<Casilla>();
                    mat.playerXPos++;
                    playerMoveRight();
                }
                break;
            case PlayerControl.Dir.LEFT:
                if (playerCasilla._casillaAdyacente[3])
                {
                    playerCasilla = mat.casillas[mat.playerXPos - 1, mat.playerYPos].GetComponent<Casilla>();
                    mat.playerXPos--;
                    playerMoveLeft();
                }
                break;
        }
    }

    private void playerMoveUp()
    {
        if (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[0])
        {
            print("sigue");
            playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos - 1].GetComponent<Casilla>();
            mat.playerYPos--;
            playerMoveUp();
        }
        else print("parate");
    }
    private void playerMoveDown()
    {
        if (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[2])
        {
            print("sigue");
            playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos + 1].GetComponent<Casilla>();
            mat.playerYPos++;
            playerMoveDown();
        }
        else print("parate");
    }
    private void playerMoveRight()
    {
        if (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[1])
        {
            print("sigue");
            playerCasilla = mat.casillas[mat.playerXPos + 1, mat.playerYPos].GetComponent<Casilla>();
            mat.playerXPos++;
            playerMoveRight();
        }
        else print("parate");
    }
    private void playerMoveLeft()
    {
        if (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[3])
        {
            print("sigue");
            playerCasilla = mat.casillas[mat.playerXPos - 1, mat.playerYPos].GetComponent<Casilla>();
            mat.playerXPos--;
            playerMoveLeft();
        }
        else print("parate");
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
