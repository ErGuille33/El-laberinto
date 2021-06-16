using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;

    public void changeColor(Color color)
    {
        renderer.color = color;
    }
}
