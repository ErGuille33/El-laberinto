using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Control de jugador
public class PlayerControl : MonoBehaviour
{
    //Direcciones
    public enum Dir { UP, DOWN, RIGHT, LEFT, STOP }

    private Dir dir = Dir.STOP;
    private bool moving = false;
    private bool clicked = false;
    public LevelManager levelManager;

    private Vector3 touchStartPos, touchEndPos;
    private Touch touch;

    private GameObject[] arrows = new GameObject [4];

    bool inicializado = false;

    private void Start()
    {
        //Flechas
        if (transform.childCount == 4)
        {
            
            for (int i = 0; i < 4; i++) {

                arrows[i] = transform.GetChild(i).gameObject;
                arrows[i].GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            }
        }
    }

    void Update()
    {
        if (GameManager._instance.getRunningGame())
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if (!inicializado && levelManager.playerCasilla != null)
            {
                inicializado = true;
                transform.position = levelManager.playerCasilla.transform.position;
            }

            // Deteccion de input
            if (!moving && touchMovement() != Dir.STOP)
            {
                moving = true;
                hideArrows();
            }
            else if (levelManager.playerCasilla != null)
            {
                if (transform.position == levelManager.playerCasilla.transform.position)
                {
                    dir = Dir.STOP;
                    moving = false;

                    setArrowsActive(levelManager.getPossibleMoves());
                }
            }
            if (dir != Dir.STOP)
            {
                moving = true;
                levelManager.MovePlayer(dir);
                dir = Dir.STOP;
            }
        } else if (GameManager._instance.getLevelFinished())
        {
            GetComponent<SpriteRenderer>().enabled = false;
            dir = Dir.STOP;
        }
    }

    void FixedUpdate()
    {
        // Ir a la casilla correspondiente
        if(moving)
            transform.position = Vector3.MoveTowards(transform.position, levelManager.playerCasilla.transform.position, 0.15f);
    }

    public void setArrowsActive(bool[] active)
    {
       
       for(int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(active[i]);
        }
       
    }

    public void hideArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(false);
        }
    }

    //Input táctil
    Dir touchMovement()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began) // Primer punto
            {
                touchStartPos = touch.position;
                clicked = true;
            }
            else if (touch.phase == TouchPhase.Ended) // Levanta el dedo
            {
                float x = touchEndPos.x - touchStartPos.x;
                float y = touchEndPos.y - touchStartPos.y;

                if (touchStartPos.y > levelManager.mat.transform.position.y)

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x > 0) dir = Dir.RIGHT;
                        else if (x < 0) dir = Dir.LEFT;
                    }
                    else
                    {
                        if (y > 0) dir = Dir.UP;
                        else if (y < 0) dir = Dir.DOWN;
                    }
                clicked = false;
            }
            else if (touch.phase == TouchPhase.Moved && clicked) // Segundo punto
            {
                touchEndPos = touch.position;
            }
            else dir = Dir.STOP;
        }
        else dir = Dir.STOP;
        return dir;
    }

    public void setArrowColor()
    {
        for (int i = 0; i < 4; i++)
        {
            arrows[i].GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        }
    }

}
