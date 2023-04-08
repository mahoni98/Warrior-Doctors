using System.Collections;
using System.Collections.Generic;
//using com.alictus.sdklite;
//using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;

    [SerializeField] private MSCGameSettings MSCSettings = null;

    private int lastLevel =0;
    private int _isFirstLevelFirstlyCompleted;
    int _tryNum;


    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                new GameObject("LevelManager").AddComponent<LevelManager>();
            }
            return instance;
        }
    }


    void Awake()
    {
        instance = this;
        MSCSettings = Resources.Load<MSCGameSettings>("MSCSettings");
        lastLevel = PlayerPrefs.GetInt("lastLevel", 1);
        _tryNum = PlayerPrefs.GetInt("tryNum", 1);
        _isFirstLevelFirstlyCompleted = PlayerPrefs.GetInt("firstLevelCompleted", 0);
        //GameAnalytics.Initialize();
        // EventManager.onFailed.AddListener(LevelFailed);
    }


    private void Start()
    {
        //AlictusSDK.SetConsentStatus(true);
        //AlictusSDK.LevelTag = "Level";
        //AlictusSDK.LevelIndex = lastLevel;
        LevelStart();
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level", lastLevel.ToString(), "Level_Progress");
        //FirebaseInit();

        //MaxSdk.InitializeSdk();
    }

    private void Update()
    {
     #if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.C))
        {
            LevelComplete();
        }

        else if (Input.GetKeyDown(KeyCode.F))
        {
            LevelFailed();
        }
     #endif
    }

    public void LevelComplete()
    {
        if (_isFirstLevelFirstlyCompleted == 0)
        {
            _isFirstLevelFirstlyCompleted = 1;
            PlayerPrefs.SetInt("firstLevelCompleted", _isFirstLevelFirstlyCompleted);
        }

        // UIManager.instance.victoryPanel.SetActive(true);

        //AlictusSDK.LevelComplete(lastLevel, true);
        //AlictusSDK.LevelIndex = lastLevel;
        //AlictusSDK.ShowingIntersititaldAd();


    }

    public void LevelFailed()
    {

        //  UIManager.instance.failPanel.SetActive(true);
        //AlictusSDK.LevelComplete(lastLevel, false);
        //AlictusSDK.ShowingIntersititaldAd();


    }

    public void LevelRestart()
    {
        PlayerPrefs.SetInt("tryNum", _tryNum++);

        StartCoroutine(AsyncSceneLoader(SceneManager.GetActiveScene().buildIndex));
    }


    void LevelPause()
    {

        
    }

    void FirebaseInit()
    {
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //    var dependencyStatus = task.Result;
        //    if (dependencyStatus == Firebase.DependencyStatus.Available)
        //    {
        //        // Create and hold a reference to your FirebaseApp,
        //        // where app is a Firebase.FirebaseApp property of your application class.
        //        // Crashlytics will use the DefaultInstance, as well;
        //        // this ensures that Crashlytics is initialized.
        //        Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

        //        // Set a flag here for indicating that your project is ready to use Firebase.
        //    }
        //    else
        //    {
        //        UnityEngine.Debug.LogError(System.String.Format(
        //          "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //        // Firebase Unity SDK is not safe to use here.
        //    }
        //});
    }

    void LevelStart()
    {
#if UNITY_EDITOR

        Debug.Log("Last Level:" + lastLevel);
        //Debug.Log("Level Mod:" + (lastLevel % MSCSettings.LevelArray.Count));
#endif
    }

    public void NextLevel()
    {
#if UNITY_EDITOR

        Debug.Log("Last Level:" + lastLevel);
        Debug.Log("Level Mod:" + (lastLevel % MSCSettings.LevelArray.Count));
        Debug.Log("New Level:" + (lastLevel + 1));

#endif

        StartCoroutine(AsyncSceneLoader(MSCSettings.LevelArray[((lastLevel) % MSCSettings.LevelArray.Count)]));
        lastLevel++;
        PlayerPrefs.SetInt("lastLevel", lastLevel);
    }

    IEnumerator AsyncSceneLoader(int BuildIndex)
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);

        while (!asyncLoadScene.isDone)
        {
            yield return null;
        }
    }
}
