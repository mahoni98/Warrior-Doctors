using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject StartPanel, GamePanel, WinPanel;

    private void Start()
    {
        //Time.timeScale = 0;

        StartPanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    public void startButton()
    {
        //Time.timeScale = 1;

        StartPanel.SetActive(false);
        GamePanel.SetActive(true);
    }
    public void ShowWinPanel()
    {
        //WinPanel.SetActive(true);
        nextScene();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startButton();
        }
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void nextScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
