using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName= "Harmonia Games/Harmonia Games Settings", fileName = "HarmoniaSettings")]

public class HarmoniaGamesSettings : ScriptableObject
{

    public enum BuildType { Debug, Release }
    public enum LoadingType { FakeLoading, RealLoading }
    public enum TutorialType { TutorialInLevel1, TutorialScene }
    public enum TutorialMechanic { TutorialInLevel1, TutorialScene }
    public enum LevelManagerType { InterSceneLevelManager , SingleSceneLevelManager }

    public BuildType buildType;

    [Header("Level Settings")]
    public LevelManagerType levelManagerType;

   // public int levelCount = 0;
    public List<int> LevelArray; 

    [Header("Loading Scene Settings")]

    public LoadingType loadingType;
    public float loadingSecond = 1f;

    [Header("Tutorial Settings")]

    public TutorialType tutorialType;
    public int tutorialLevelIndex=1;
   


}