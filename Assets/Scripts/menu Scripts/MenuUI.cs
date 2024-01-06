using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreAndNameText;
    [SerializeField] TMP_InputField enterPlayerName;
    [SerializeField] TextMeshProUGUI previousGame;
    // Start is called before the first frame update
    void Start()
    {
        SceneSessionManager.Instance.LoadNameAndScore();
        scoreAndNameText.text = "Best Score: " + SceneSessionManager.Instance.BestPlayer + " : " + SceneSessionManager.Instance.BestScore;
        previousGame.text = "Previous Game: " + SceneSessionManager.Instance.PlayerName + " : " + SceneSessionManager.Instance.PreviousScore;
        enterPlayerName.text = SceneSessionManager.Instance.PlayerName;
    }

    public void StartGame()
    {
        SceneSessionManager.Instance.PlayerName = enterPlayerName.text;
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Exit();
#endif
        SceneSessionManager.Instance.SaveNameAndScore();
    }
}
