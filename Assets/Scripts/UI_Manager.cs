using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public int scoreP1 = 0;
    public int scoreP2 = 0;
    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;
    public TextMeshProUGUI scoreTextWinner;
    public TextMeshProUGUI scoreTextLoser;
    public TextMeshProUGUI playerWinner;
    public TextMeshProUGUI playerLoser;

    public GameObject leaderboard;
    public GameObject HUD;

    public static UI_Manager instance;
    private Spawner spawner;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();

        scoreTextP1.text = scoreP1.ToString();
        scoreTextP2.text = scoreP2.ToString();

        leaderboard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CallLeaderBoard();
    }

    private void CallLeaderBoard()
    {
        if(spawner.countEnemy >= 10)
        {
            leaderboard.SetActive(true);
            HUD.SetActive(false);
            SetLeaderBoard();
        }
    }

    private void SetLeaderBoard()
    {
        scoreTextWinner.text = scoreP1 > scoreP2 ? scoreP1.ToString() : scoreP2.ToString();
        scoreTextLoser.text = scoreP1 < scoreP2 ? scoreP1.ToString() : scoreP2.ToString();

        playerWinner.text = scoreP1 > scoreP2 ? "Player 1" : "Player 2";
        playerLoser.text = scoreP1 < scoreP2 ? "Player 1" : "Player 2";    
    }
}
