using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDirectionCheck : MonoBehaviour
{
    [SerializeField] private Vector3[] tutorialMoveDirections;
    [SerializeField] private GameObject[] tutorialFingers;
    [SerializeField] private RayController rayController;
    [SerializeField] private SceneController sceneController;
    private int tutorialCounter;
    private void OnEnable()
    {
        tutorialFingers[tutorialCounter].SetActive(true);
    }
    public void UpdateColorForRay(Vector3 direction)
    {

        rayController.ChangeColor(Color.Lerp(Color.red, Color.green, 20 - Vector3.Angle(direction, tutorialMoveDirections[tutorialCounter])));
    }
    public bool CheckDirection(Vector3 direction)
    {
        var checkDirectionAngle = Vector3.Angle(direction, tutorialMoveDirections[tutorialCounter]);
        Debug.Log(checkDirectionAngle);
        return checkDirectionAngle < 20;
    }
    public Vector3 MoveDirection()
    {
        var moveDirecton = tutorialMoveDirections[tutorialCounter];
        NextTutorial();
        return moveDirecton;
    }
    private void NextTutorial()
    {
        tutorialFingers[tutorialCounter].SetActive(false);
        if (tutorialCounter < tutorialFingers.Length - 1)
            tutorialCounter++;
        else
            Invoke("FinishTutorial", 1);
        tutorialFingers[tutorialCounter].SetActive(true);
    }
    private void FinishTutorial()
    {
        sceneController.ShowWinPanel();
        LevelManager.Instance.LevelComplete();
    }
}
