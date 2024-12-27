using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Scene scene;
    public Slider loadingBar;
    public GameObject UIGame;

    private string _lastPlayedScene; // Biến để lưu scene đã chơi

    void Start()
    {

    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void startGame()
    {
        StartCoroutine(LoadSceneAsync("Level_1_Scene"));
        hideUIMain();
    }

    public void LoadContinuteScene()
    {
        _lastPlayedScene = scene.name; // Lưu tên scene hiện tại
        StartCoroutine(LoadSceneAsync("Continute_Scene"));
    }
    public void ResumeGame() // Hàm để resume
    {
        StartCoroutine(LoadSceneAsync(_lastPlayedScene));
    }

    public void RestartCurrentScene()
    {
        StartCoroutine(LoadSceneAsync(scene.name));
    }

    private void hideUIMain()
    {
        UIGame.SetActive(false);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingBar.gameObject.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBar.value = progress;
            Debug.Log($"Loading progress: {progress * 100}%");

            yield return null;
        }

        loadingBar.gameObject.SetActive(false);
        Debug.Log("Scene loaded successfully.");
    }
}