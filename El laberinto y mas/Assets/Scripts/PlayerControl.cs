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
    public LevelManager levelManager;

    private Vector3 touchStartPos, touchEndPos;
    private Touch touch;

    bool inicializado = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameManager.State.RUN)
        {
            if (!inicializado && levelManager.playerCasilla != null)
            {
                inicializado = true;
                transform.position = levelManager.playerCasilla.transform.position;
            }

            if (!moving && touchMovement() != Dir.STOP) moving = true;
            else if (levelManager.playerCasilla != null)
            {
                if (transform.position == levelManager.playerCasilla.transform.position)
                {
                    dir = Dir.STOP;
                    moving = false;
                }
            }
            if (dir != Dir.STOP)
            {
                moving = true;
                levelManager.MovePlayer(dir);
                dir = Dir.STOP;
            }
        }
    }

    void FixedUpdate()
    {
        if(moving)
            transform.position = Vector3.MoveTowards(transform.position, levelManager.playerCasilla.transform.position, 0.15f);
    }
    //Input táctil
    Dir touchMovement()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                float x = touchEndPos.x - touchStartPos.x;
                float y = touchEndPos.y - touchStartPos.y;

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
            }
            else dir = Dir.STOP;
        }
        else dir = Dir.STOP;
        return dir;
    }

}
