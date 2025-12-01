using UnityEngine;
using UnityEngine.UI;

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
    private Sprite[] _liveSprites;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
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
           
                _gameOverText.gameObject.SetActive(true);
             
            

        }
    }

}
