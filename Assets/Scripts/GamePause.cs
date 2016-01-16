using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour
{

    public Sprite[] pauseSprites;
    public Sprite[] startSprites;

    private bool isDownState = false;

    private SpriteRenderer render;

    // Use this for initialization
    void Start()
    {
        isDownState = false;
        render = GetComponent<SpriteRenderer>();
        render.sprite = pauseSprites[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUpAsButton()
    {
        GameManager._instance.SwitchGameState();
        GetComponent<AudioSource>().Play();
        if (GameManager._instance.state == GameState.Gaming)
        {
            render.sprite = pauseSprites[0];
        }
        else
        {
            render.sprite = startSprites[0];
        }
    }

    void OnMouseDown()
    {
        isDownState = true;
        if (GameManager._instance.state == GameState.Gaming)
        {
            render.sprite = pauseSprites[1];
        }
        else
        {
            render.sprite = startSprites[1];
        }
    }

    void OnMouseExit()
    {
        if (isDownState)
        {
            isDownState = false;
            if (GameManager._instance.state == GameState.Gaming)
            {
                render.sprite = pauseSprites[0];
            }
            else
            {
                render.sprite = startSprites[0];
            }
        }
    }
}
