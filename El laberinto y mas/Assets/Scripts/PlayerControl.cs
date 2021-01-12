﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum Dir { UP, DOWN, RIGHT, LEFT, STOP }

    private Dir dir = Dir.STOP;
    private bool moving = false;
    public LevelManager levelManager;

    private Vector3 touchStartPos, touchEndPos;
    private Touch touch;

    bool inicializado = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inicializado)
        {
            inicializado = true;
            transform.position = levelManager.playerCasilla.transform.position;
        }

        if (!moving && touchMovement() != Dir.STOP) moving = true;
        else
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

    void FixedUpdate()
    {
        if(moving)
            transform.position = Vector3.MoveTowards(transform.position, levelManager.playerCasilla.transform.position, 0.1f);
    }

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
