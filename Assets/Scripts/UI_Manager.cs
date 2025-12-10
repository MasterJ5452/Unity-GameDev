using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Manager : MonoBehaviour
{
    //handle to Text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private float _textFlashTime = .5f;

    private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false); 
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if( _gameManager == null)
        {
            Debug.LogError("Game Manager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void updateLives(int currentLives)
    {
        //display img sprite
        //give it a new one based on the current Lives index

        _LivesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
            

        }
    }

    void GameOverSequence()
    {
        _restartText.gameObject.SetActive(true);
        StartCoroutine(BlinkText());
        _gameManager.GameOver();

    }


    IEnumerator BlinkText()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_textFlashTime);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(_textFlashTime);
        }

    }
}
