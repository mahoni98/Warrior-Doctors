using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public int lastLevel;
    [SerializeField] private HarmoniaGamesSettings HarmoniaSettings = null;
    [SerializeField] private GameObject updateReqPanel;

    public Slider loadingSlider;
    private bool isUpdateRequired;




    void Awake()
    {

        HarmoniaSettings = Resources.Load<HarmoniaGamesSettings>("HarmoniaSettings");

    }

    public void OnUpdateRequired()
    {
        isUpdateRequired = true;


    }

    void Start()
    {
        //if (!isUpdateRequired)
        //{
        //    lastLevel = PlayerPrefs.GetInt("lastLevel", 1);

        //    if (SceneControl.instance.firstLogin)
        //    {

        //        if(PlayerPrefs.GetInt("firstLevelCompleted") == 0 && HarmoniaSettings.tutorialType == HarmoniaGamesSettings.TutorialType.TutorialScene)
        //        {
        //            StartCoroutine(AsyncSceneLoader(HarmoniaSettings.tutorialLevelIndex, HarmoniaSettings.loadingSecond));
        //             #if UNITY_EDITOR
        //             Debug.Log("Level: Tutorial" + HarmoniaSettings.tutorialLevelIndex);
        //            #endif

        //        }
        //        else if (PlayerPrefs.GetInt("firstLevelCompleted") == 0 && HarmoniaSettings.tutorialType == HarmoniaGamesSettings.TutorialType.TutorialInLevel1)
        //        {
        //            StartCoroutine(AsyncSceneLoader(HarmoniaSettings.LevelArray[((lastLevel - 1) % HarmoniaSettings.LevelArray.Count)], HarmoniaSettings.loadingSecond));
        //        }
        //        else
        //        {
        //            StartCoroutine(AsyncSceneLoader(HarmoniaSettings.LevelArray[((lastLevel-1) % HarmoniaSettings.LevelArray.Count)], HarmoniaSettings.loadingSecond));
        //        }

        //        SceneControl.instance.firstLogin = false;
        //    }
        //    else
        //        StartCoroutine(AsyncSceneLoader(SceneControl.instance.levelIndex, HarmoniaSettings.loadingSecond));
        //}

        if (PlayerPrefs.GetInt("firstLevelCompleted") == 0)
        {
            StartCoroutine(AsyncSceneLoader(HarmoniaSettings.tutorialLevelIndex, HarmoniaSettings.loadingSecond));
        }
        else
        {
            StartCoroutine(AsyncSceneLoader(3, HarmoniaSettings.loadingSecond));
        }
    }




    IEnumerator AsyncSceneLoader(int BuildIndex, float seconds)
    {
        if (HarmoniaSettings.loadingType == HarmoniaGamesSettings.LoadingType.FakeLoading)  //
        {
            float currentTime = 0;

            while (currentTime < seconds)
            {
                currentTime += Time.deltaTime;
                loadingSlider.value = currentTime;
                yield return null;
            }

            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);
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
