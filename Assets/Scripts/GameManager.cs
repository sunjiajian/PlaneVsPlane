using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum GameState
{
    Gaming,
    Pause,
}

public class GameManager : MonoBehaviour {

    public static GameManager _instance;

    public int score = 0;
    public Text scoreText;

    public GameState state = GameState.Gaming;

    void Awake() {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
        scoreText = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Text>();
        state = GameState.Gaming;
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score:" + score;
	}

    public void SwitchGameState() {
        switch (state) { 
            case GameState.Gaming:
                state = GameState.Pause;
                Time.timeScale = 0;
                break;

            case GameState.Pause:
                state = GameState.Gaming;
                Time.timeScale = 1;
                break;
        }
    }
}
