using UnityEngine;
using DG.Tweening;
using System.Collections;
public class PlayerSwipe : MonoBehaviour
{
    public Vector3 swipe;
    [SerializeField] Transform playerArrow;
    [SerializeField] PlayerController pc;
    [SerializeField] private TutorialDirectionCheck tutorialDirectionCheck;
    public float MovementRange = 4f;

    public static PlayerSwipe instance;

    public bool SlowMotionActive = true;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MovementRange = 3f;
        SlowMo(1, 0.7f, false);
    }

    bool isEnable = false;

    private void OnEnable()
    {
        isEnable = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        tw1.Kill();
        tw2.Kill();
        isEnable = false;
        //Time.timeScale = 0;
    }

    private void Update()
    {
        if (isEnable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("DOKANDIR");
                swipe = new Vector3(pc.transform.position.x, pc.transform.position.z, 0);
                SlowMo(0, 0.01f);
                pc.DOKill();
            }

            if (Input.GetMouseButton(0))
            {
                float x = Mathf.Clamp(swipe.x, pc.transform.position.x - MovementRange, pc.transform.position.x + MovementRange);
                float z = Mathf.Clamp(swipe.y, pc.transform.position.z - MovementRange, pc.transform.position.z + MovementRange);

                swipe = new Vector3(x, z, 0);

                Vector3 clampVector = new Vector3(x, 0, z);
               
                playerArrow.transform.position = clampVector;
                if(tutorialDirectionCheck == null) return;
                tutorialDirectionCheck.UpdateColorForRay(clampVector - pc.transform.position); 

            }

            if (Input.GetMouseButtonUp(0))
            {
                //
                Vector2 pos = new Vector3(pc.transform.position.x, pc.transform.position.z);
                if (Vector2.Distance(swipe, pos) > 1f)
                {
                    pc.DOKill();
                    float x = Mathf.Clamp(swipe.x, pc.transform.position.x - MovementRange, pc.transform.position.x + MovementRange);
                    float z = Mathf.Clamp(swipe.y, pc.transform.position.z - MovementRange, pc.transform.position.z + MovementRange);

                    Vector3 clampVector = new Vector3(x, 0, z);
                    if (tutorialDirectionCheck != null)
                    {
                        if (!tutorialDirectionCheck.CheckDirection(clampVector - pc.transform.position)) return;
                        pc.Movement(tutorialDirectionCheck.MoveDirection() + pc.transform.position);
                        return;
                    }

                    pc.Movement(clampVector);
                }
                else
                {
                    SlowMo(1, 0.7f, true);
                }

                swipe = new Vector3(pc.transform.position.x, pc.transform.position.z, 0);
            }
        }
    }

    DG.Tweening.Tweener tw1, tw2;

    public void SlowMo(float slowMoScale, float timing, bool afterZero = false)
    {
        if (SlowMotionActive)
        {
            StopAllCoroutines();
            StartCoroutine(slowMoCorot(slowMoScale, timing, afterZero));
        }
    }

    IEnumerator slowMoCorot(float slowMoScale, float timing, bool afterZero = false)
    {
        tw1.Complete();
        tw2.Complete();
        this.DOKill();

        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        if (slowMoScale < 0.5f)
            Time.timeScale = 1;
        else
        {
            Time.timeScale = 0.01f;
        }

        tw1 = DOTween.To(value => Time.timeScale = value, Time.timeScale, Mathf.Clamp(slowMoScale, 0.01f, 1f), timing).OnComplete(() =>
        {
            if (afterZero)
            {
                tw2 = DOTween.To(value => Time.timeScale = value, Time.timeScale, Mathf.Clamp(0.01f, 0.01f, 1f), timing);
            }
        });
    }
}
