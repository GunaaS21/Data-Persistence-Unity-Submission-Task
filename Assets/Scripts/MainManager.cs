using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    [SerializeField] TextMeshProUGUI bestScoreAndNameText;

    private bool m_Started = false;
    public int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneSessionManager.Instance != null )//|| SceneSessionManager.Instance == null)
        {
            bestScoreAndNameText.text = "Best Score: " + SceneSessionManager.Instance.BestPlayer + " : " + SceneSessionManager.Instance.BestScore;
        }
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }



    void AddPoint(int point)
    {
        m_Points += point;
        if (SceneSessionManager.Instance != null) //|| SceneSessionManager.Instance == null)
        {
            ScoreText.text = $"Score : {SceneSessionManager.Instance.PlayerName} : {m_Points}";
        }
    }
     public void BackToMenu()
    {
        SceneSessionManager.Instance.SaveNameAndScore();
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        m_GameOver = true;
        SceneSessionManager.Instance.UpdateBestScore(m_Points);
        SceneSessionManager.Instance.SaveNameAndScore();
        //SceneSessionManager.Instance.SaveNameAndScore();
        GameOverText.SetActive(true);
    }
}
