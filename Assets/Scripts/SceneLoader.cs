using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    public int lastLevel;
    [SerializeField] private MSCGameSettings MSCSettings = null;
    [SerializeField] private GameObject updateReqPanel;

    public Slider loadingSlider;
    private bool isUpdateRequired;

    void Awake()
    {
        MSCSettings = Resources.Load<MSCGameSettings>("MSCSettings");
    }

    public void OnUpdateRequired()
    {
        isUpdateRequired = true;
    }

    void Start()
    {

        if (PlayerPrefs.GetInt("firstLevelCompleted") == 0)
        {
            StartCoroutine(AsyncSceneLoader(MSCSettings.tutorialLevelIndex, MSCSettings.loadingSecond));
        }
        else
        {
            StartCoroutine(AsyncSceneLoader(3, MSCSettings.loadingSecond));
        }
    }

    IEnumerator AsyncSceneLoader(int BuildIndex, float seconds)
    {
        if (MSCSettings.loadingType == MSCGameSettings.LoadingType.FakeLoading)  //
        {
            DOTween.To(() => 0f, x =>
            {
                loadingSlider.value = x;
            }, 1f, seconds).OnComplete(() => SceneManager.LoadScene(BuildIndex));

            //float currentTime = 0;

            //while (currentTime < seconds)
            //{
            //    currentTime += Time.deltaTime;
            //    loadingSlider.value = currentTime;
            //    yield return null;
            //}
            //AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);
        }
        else
        {
            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);

            while (!asyncLoadScene.isDone)
            {
                loadingSlider.value = asyncLoadScene.progress;
                yield return null;
            }
        }
    }
}
