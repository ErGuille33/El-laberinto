using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
//EL level manager leerá los niveles y pasará los datos pertinentes al tablero, y tambien se encargará de la lógica de cada nivel
public class LevelManager : MonoBehaviour
{
    //Archivo y matriz de casillas lógica
    public MatrixCasillas mat;
    //Posición lógica del jugador en la casilla de la matriz
    public Casilla playerCasilla;

    public TextAsset level;

    //Para leer datos del nivel
    protected LevelData lvlData;
    protected WallsData[] walls;
    protected bool[,,] wallsArray = new bool [500, 500, 4];
    protected bool[,] isIcedarray = new bool[500, 500];
    public GameObject player;
    public GameObject panelFin;
    public Color col;

    Vector2 endCasillaVector;
    Vector2 startCasilla;

    //Cola con las partes del path pendientes de pintar. De esta manera se va creando poco a poco
    Queue<Vector3> colaPath = new Queue<Vector3>();

    //Variables para la partida
    public bool finishedLevel = false;
    public bool iceLevel;
    int totalHints;
    public int actualHints;

    //Variables auxiliares
    private int auxInvertedCoord;
    private int auxTotalCols;

    void Start()
    {
        GameManager._instance.StartNewLevel();
    }

    //Para poner el nivel 
    public void setTextAsset(TextAsset lvl)
    {
        level = lvl;
    }

    //Adaptamos los datos del json y preparamos el array de paredes
    protected void setWallsArray()
    {


        for (int i = 0; i < wallsArray.GetLength(0); i++)
        {
            for (int j = 0; j < wallsArray.GetLength(1); j++)
            {
                for (int l = 0; l < wallsArray.GetLength(2); l++)
                {
                     wallsArray[i, j, l] = true;
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
                    if ((int)walls[i].o.x == auxTotalCols) {
                        wallsArray[(int)walls[i].o.x-1, auxInvertedCoord - (int)walls[i].o.y, 1] = false; 
                    }
                    else
                    {
                        if (walls[i].o.y < walls[i].d.y)
                        {
                            wallsArray[(int)walls[i].o.x, auxInvertedCoord - 1-(int)walls[i].o.y, 3] = false;
                        }
                        if (walls[i].o.y > walls[i].d.y)
                        {
                            wallsArray[(int)walls[i].o.x, auxInvertedCoord - (int)walls[i].o.y, 3] = false;
                        }
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
                    if (walls[i].o.x < walls[i].d.x)
                    {
                        wallsArray[(int)walls[i].o.x, auxInvertedCoord - 1 - (int)walls[i].o.y, 2] = false;
                    }
                    if (walls[i].o.x > walls[i].d.x)
                    {
                        wallsArray[(int)walls[i].o.x-1, auxInvertedCoord - 1 - (int)walls[i].o.y, 2] = false;
                    }
                }
            }
        }

        for (int i = 0; i < wallsArray.GetLength(0); i++)
        {
            for (int j = 0; j < wallsArray.GetLength(1); j++)
            {
               
                if(i < auxTotalCols)
                {
                    if(!wallsArray[i+1, j, 3])
                        wallsArray[i, j, 1] = false;
                }

                if (j >0)
                {
                    if (!wallsArray[i, j-1, 2])
                        wallsArray[i, j, 0] = false;
                }

            }
        }

        //Esquina superior izquierdas
        wallsArray[0, 0, 3] = false;
        wallsArray[0, 0, 0] = false;

        //Esquina superior derecha
        wallsArray[auxTotalCols +2, 0, 1] = false;
        wallsArray[auxTotalCols +2, 0, 0] = false;

        wallsArray[auxTotalCols + 2, auxInvertedCoord-1, 1] = false;
        wallsArray[auxTotalCols + 2, auxInvertedCoord-1, 2] = false;

        wallsArray[0, auxInvertedCoord - 1, 2] = false;
        wallsArray[0 , auxInvertedCoord - 1, 3] = false;

    }
    //Adaptamos los datos del json y preparamos el array de casillas heladas
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
            if (lvlData.i[i].x > 0 && lvlData.i[i].y > 0)
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
    }

   //MArca la casilla final
    protected void setEnd()
    {
        if(lvlData.f.y == auxInvertedCoord)
        {
            endCasillaVector = new Vector2(lvlData.f.x, auxInvertedCoord - lvlData.f.y);
        }
        else endCasillaVector = new Vector2(lvlData.f.x, auxInvertedCoord - 1 - lvlData.f.y);
        

    }

    //MArca la casilla inicial
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
    //Carga los datos del json y llama a los metodos pertinentes para crear el mapa del nivel
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
      
        auxInvertedCoord = lvlData.r;
        auxTotalCols = lvlData.c;

        setWallsArray();
        setIcedArray();
   
        setEnd();
        setStart();
       
        mat.createNewMap(lvlData.r, lvlData.c, wallsArray, isIcedarray, endCasillaVector, startCasilla,col);
        playerCasilla = mat.casillas[(int)startCasilla.x, (int)startCasilla.y];
        player.transform.localScale = new Vector2 (2.5f, 2.5f);
        player.transform.position = playerCasilla.transform.position;

        totalHints = lvlData.h.Length;


    }
    //Este método sirve para pintar el path a medida que pasa el jugador, en vez de que se pinte todo automaticamente
    public void paintPath()
    {
        if (colaPath.Count > 0)
        {
            if (Vector2.Distance(player.transform.position, mat.casillas[(int)colaPath.First<Vector3>().x, (int)colaPath.First<Vector3>().y].transform.position) < 0.5)
            {
                
                Vector3 vec = colaPath.Dequeue();
                mat.setPlayerPath((int)vec.x, (int)vec.y, (int)vec.z);
            }
        }
    }

    //Método que mueve la posición del jugador
    public void MovePlayer(PlayerControl.Dir dir)
    {
        switch(dir)
        {
            case PlayerControl.Dir.UP:
                if (playerCasilla._casillaAdyacente[0])
                {

                        colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 0));
                        //Posición lógica
                        playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos - 1];
                        mat.playerYPos--;
                        colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 2));
                        playerMoveUp();
                  
                }
                break;
            case PlayerControl.Dir.DOWN:
                if (playerCasilla._casillaAdyacente[2])
                {
                    colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 2));
                    //Posición lógica
                    playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos + 1];
                    mat.playerYPos++;
                    colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 0));
                    playerMoveDown();
                }
                break;
            case PlayerControl.Dir.RIGHT:
                if (playerCasilla._casillaAdyacente[1])
                {

                        colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 1));
                        //Posición lógica     
                        playerCasilla = mat.casillas[mat.playerXPos + 1, mat.playerYPos];
                        mat.playerXPos++;
                        colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 3));
                        playerMoveRight();
                    
                }
                break;
            case PlayerControl.Dir.LEFT:
                if (playerCasilla._casillaAdyacente[3])
                {
                    colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 3));
                    //Posición lógica
                    playerCasilla = mat.casillas[mat.playerXPos - 1, mat.playerYPos];
                    mat.playerXPos--;
                    colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 1));
                    playerMoveLeft();
                }
             
                break;
        }

    }

    //Método que mueve el jugador reiterativamente mientras sea necesario
    private void playerMoveUp()
    {
        if ((playerCasilla.getIced() && playerCasilla._casillaAdyacente[0]) || (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[0]))
        {           
                playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos - 1];
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 0));
                mat.playerYPos--;
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 2));
                checkWin();
                playerMoveUp();          
        }
    }
    //Método que mueve el jugador reiterativamente mientras sea necesario
    private void playerMoveDown()
    {
        if ((playerCasilla.getIced() && playerCasilla._casillaAdyacente[2]) || (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[2]))
        {

                playerCasilla = mat.casillas[mat.playerXPos, mat.playerYPos + 1];
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 2));
                mat.playerYPos++;
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 0));
                checkWin();
                playerMoveDown();

        }
    }
    //Método que mueve el jugador reiterativamente mientras sea necesario
    private void playerMoveRight()
    {
        if ((playerCasilla.getIced() && playerCasilla._casillaAdyacente[1]) || (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[1]))
        {           
                playerCasilla = mat.casillas[mat.playerXPos + 1, mat.playerYPos];
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 1));
                mat.playerXPos++;
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 3));
                checkWin();
                playerMoveRight();
            
        }
    }
    //Método que mueve el jugador reiterativamente mientras sea necesario
    private void playerMoveLeft()
    {
        if ((playerCasilla.getIced() && playerCasilla._casillaAdyacente[3]) || (playerCasilla.getSalidas() < 3 && playerCasilla._casillaAdyacente[3]))
        {

                playerCasilla = mat.casillas[mat.playerXPos - 1, mat.playerYPos];
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 3));
                mat.playerXPos--;
                colaPath.Enqueue(new Vector3(mat.playerXPos, mat.playerYPos, 1));
                checkWin();
                playerMoveLeft();
            
        }
    }

    //Método que se llama desde player para las flechas que indican las posibles direcciones
    public bool[] getPossibleMoves()
    {
        bool[] possibleMoves = { false, false, false, false };

        for(int i = 0; i < 4; i++)
        {
            possibleMoves[i] = playerCasilla._casillaAdyacente[i];
        }

        return possibleMoves;
    }

    //Comprueba victoria
    public void checkWin()
    {
        if (playerCasilla != null)
        {
            //Lo hacemos por distancia y no por lógica para que no se realice el cambio de nivel hasta que el jugador llegue al centro de la casilla de destino
            if (Vector2.Distance(player.transform.position, mat.endCasilla.transform.position) < 0.2)
            {
                finishedLevel = true;

                //spawn panel
                panelFin.SetActive(true);
            }
        }
    }
    //Ultimo nivel
    public void setFinishedLevel(bool finish)
    {
        finishedLevel = finish;
    }
    //Nuevo nivel
    public void setNewLevel(TextAsset newLevel)
    {
        level = newLevel;
    }
    //Empezar un nuevo nivel
    public void startNewLevel(bool isIceLevel)
    {
        panelFin.SetActive(false);
        iceLevel = isIceLevel;
        if (!iceLevel) 
            col = new Color(0.082f, 0.745f, 0.196f);
        else 
            col = new Color(0, 0.6f, 0.84f);
     
      
        player.GetComponent<SpriteRenderer>().color = col;

        player.GetComponent<PlayerControl>().setArrowColor();

        colaPath.Clear();
        mat.resetMap();
        cargaJson();

    }

    public void restartLevel()
    {
        startNewLevel(iceLevel);
    }

    public void setActualHints(int num)
    {
        actualHints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        paintPath();

        checkWin();
   
    }
    //Adaptamos los datos del json y preparamos el array de hints dependiendo del número que tengamos
    protected void setHintsArray()
    {
        int from = -1;
        int to = -1;
        int x = -1;
        int y = -1;

        int countHints = (int)((float)(totalHints / 3) )* actualHints;

        if(countHints > totalHints || actualHints >= 3)
        {
            countHints = totalHints;
        }

        for (int i = 0; i < countHints; i++)
        {
              
            if (i != lvlData.h.Length - 1)
            {
                //Vertical
                if (lvlData.h[i].x == lvlData.h[i + 1].x)
                {
                    if (lvlData.h[i].y > lvlData.h[i + 1].y)
                    {
                        to = 2;
                    }
                    else
                    {
                        to = 0;
                    }

                }
                //Horizontal
                else if (lvlData.h[i].y == lvlData.h[i + 1].y)
                {
                    if (lvlData.h[i].x > lvlData.h[i + 1].x)
                    {
                        to = 3;
                    }
                    else
                    {
                        to = 1;
                    }

                }
                //Para la primera casilla
                if (i == 0)
                {
                    if(lvlData.s.y== lvlData.h[i].y)
                    {
                        if(lvlData.h[i].x > lvlData.s.x)
                        {
                            from = 3;
                        }
                        else
                        {
                            from = 1;

                        }
                    }
                    else
                    {
                        if (lvlData.h[i].y > lvlData.s.y)
                        {
                            from = 2;
                        }
                        else {
                            from = 0;
                        }
                    }
                }
            }
            //Ultimo caso
            if (i != 0)
            {
                if (lvlData.h[i].x == lvlData.h[i - 1].x)
                {
                    if (lvlData.h[i].y > lvlData.h[i - 1].y)
                    {
                        from = 2;
                
                    }
                    else
                    {
                        from = 0;
              
                    }

                }
                //Horizontal
                else if (lvlData.h[i].y == lvlData.h[i - 1].y)
                {
                    if (lvlData.h[i].x > lvlData.h[i - 1].x)
                    {
                        from = 3;
                    
                    }
                    else
                    {
                        from = 1;
                    }

                }
                //Ultimo caso
                if (i == lvlData.h.Length - 1)
                {
                    if (lvlData.f.y == lvlData.h[i].y)
                    {
                        if (lvlData.h[i].x > lvlData.f.x)
                        {
                            to = 3;
                        }
                        else
                        {
                            to = 1;

                        }
                    }
                    else
                    {
                        if (lvlData.h[i].y > lvlData.f.y)
                        {
                            to = 2;
                        }
                        else
                        {
                            to = 0;
                        }
                    }
                }
            }
                
            if ((int)lvlData.h[i].x == auxTotalCols)
            {
               x = (int)lvlData.h[i].x - 1;
            }
            else if((int)lvlData.h[i].x > auxTotalCols)
            {
                x = 0;
            }
             
            else if((int)lvlData.h[i].x <= 0)
            {

               x = 0;
            }
            else
            {
               x = (int)lvlData.h[i].x ;
            }

            if((int)lvlData.h[i].y >= auxInvertedCoord)
            {
               y = 0;
            }
           
            else if((int)lvlData.h[i].y <= 0)
            {
               y = 0;
               y = auxInvertedCoord - 1 -(int)lvlData.h[i].y;
            }
            else
            {
                y = auxInvertedCoord - 1 - (int)lvlData.h[i].y;
            }
            mat.setHints(x, y, from, to,true);

        }
       
    }
    public bool addHints()
    {
        if (actualHints < 3)
        {
            actualHints++;
            //Colocamos este script en el update para poder modificar las pistas desde el editor. Si no fuera asi, lo lanzaríamos sólo desde el game manager al gastar una pista
            setHintsArray();
            return true;
        }
        else return false;
    }

    //Datos de recogidos del array

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

        public WallsData[] w ;
        public Vector2[] h  ;
        public Vector2[] i ;
        public Vector2[] e ;
        public Vector2[] t;
    }

    [Serializable]
    public class WallsData
    {
        
        public Vector2 o;
        public Vector2 d;

    }
}
