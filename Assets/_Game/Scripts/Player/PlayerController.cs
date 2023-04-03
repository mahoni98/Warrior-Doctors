using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    bool isMove = false;

    [SerializeField] Image healthBarFill;

    [SerializeField] int health = 100;

    [SerializeField] Transform playerCube;

    [SerializeField] GameObject projectile;

    public ProjectileUpgrades PU;

    [Header("Decals")]

    public Transform m_Trails;

    public Transform m_Splashs;
    private Collider m_Cube;

    [SerializeField] GameObject GameOverCanvas;

    public static PlayerController instance;

    [SerializeField] GameObject deathEffect, playerObject;

    [SerializeField] BoxCollider col;
    [SerializeField] private Animator _Animator;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        canvasUpdate();
        m_Cube = this.GetComponent<Collider>();
        GameOverCanvas.SetActive(false);
        PlayerSwipe.instance.SlowMo(0, 0.7f); //stop at start
    }


    public void Movement(Vector3 Direction, bool CollisionTree = false)
    {
        Haptic.Instance.HapticSoft();
        SoundController.instance.PlaySound(SoundController.Type.Jump);
        Time.timeScale = 1;
        transform.DOKill();
        StopAllCoroutines();
        transform.LookAt(Direction);
        float MoveTime = Vector3.Distance(Direction, transform.position);
        float timing = MoveTime / 10f;

        transform.DOMove(Direction, timing).OnUpdate(() =>
        {
            Time.timeScale = 1;
            PlayerSwipe.instance.enabled = false;
            isMove = true;
        }).OnComplete(() =>
        {
            PlayerSwipe.instance.enabled = true;
            Time.timeScale = 1;
            isMove = false;
            StopAllCoroutines();

            if (!CollisionTree)
                StartCoroutine(Shot());
            else
            {
                PlayerSwipe.instance.SlowMo(0, 0.7f);
            }

        });

        Vector3 rot = new Vector3(360, 0, 0);
        _Animator.Play("Jump");
        playerCube.DOLocalRotate(rot, timing, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            playerCube.transform.localEulerAngles = new Vector3(0, 0, 0);
            //_Animator.SetBool("Jump", false);
            _Animator.Play("Idle");
            CreateTrail();
        });
    }

    public void collisionTree()
    {
        StopAllCoroutines();
        transform.DOKill();
        Movement(transform.position + -transform.forward * 2f);
    }

    IEnumerator Shot()
    {
        for (int x = 0; x < PU.ProjectileRepeat; x++)
        {
            List<ProjectileController> projectileList = new List<ProjectileController>();

            float winkel = 0;
            float AngularMix = 0;

            if (PU.ProjectileAngular && PU.ProjectileCount > 1)
            {
                winkel = 45 / (PU.ProjectileCount - 1);
                AngularMix = 45 / 2f;
            }

            for (int i = 0; i < PU.ProjectileCount; i++)
            {

                SoundController.instance.PlaySound(SoundController.Type.Gun);
                var _obj = Instantiate(projectile);
                _obj.AddComponent<Destoryer>();
                var _pc = _obj.GetComponent<ProjectileController>();
                projectileList.Add(_pc);
                _obj.transform.position = transform.position + transform.forward * 2f + transform.up;

                _obj.transform.eulerAngles = new Vector3(0, (transform.eulerAngles.y - AngularMix) + (winkel * (i)), 0);

                _obj.transform.SetParent(GroundController.instance.treeparent.transform);

                if (!PU.ProjectileAngular)
                    yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);

        PlayerSwipe.instance.SlowMo(0, 0.7f);
    }

    public void OnHit()
    {
        //SoundController.instance.PlaySound(SoundController.Type.PlayerDamage);
        GroundController.instance.AddKill();

        //if (isMove)
        //{
        //    Debug.Log("Score ++");
        //}
        //else
        //{
        //    Debug.Log("HitMe");
        //    health -= 10;
        //    Haptic.Instance.HapticDamage();
        //}
        Debug.Log("HitMe");
        health -= 10;
        Haptic.Instance.HapticDamage();

        if (health <= 0)
        {
            var fx = Instantiate(deathEffect);
            fx.transform.position = transform.position;

            StartCoroutine(deathWait());
            //this.enabled = false;

            col.enabled = false;
        }

        canvasUpdate();
    }

    IEnumerator deathWait()
    {
        playerObject.SetActive(false);
        PlayerSwipe.instance.gameObject.SetActive(false);

        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(2f);

        Debug.Log("GameOver");
        Time.timeScale = 1f;
        DOTween.KillAll();
        this.gameObject.SetActive(false);

        GameOverCanvas.SetActive(true);
        SoundController.instance.PlaySound(SoundController.Type.lose);
        LevelManager.Instance.LevelFailed();
    }

    public void Coin()
    {
        Debug.Log("Score ++");
        UpgradeSystem.instance.AddCoin();
        Haptic.Instance.HapticSoft();
    }

    void canvasUpdate()
    {
        healthBarFill.fillAmount = health / 100f;
    }
    private void CreateTrail()
    {
        if (m_Trails == null)
        {
            return;
        }

        Vector2 trailOffset = new Vector2(Random.Range(0, 2) * 0.5f, Random.Range(0, 2) * 0.5f);
        Quaternion decalRotation = Quaternion.Euler(new Vector3(90, Random.Range(0, 4) * 90f, 0));
        GameObject trail = Instantiate(m_Trails.gameObject, new Vector3(m_Cube.transform.position.x, m_Cube.bounds.min.y + 0.01f, m_Cube.transform.position.z), decalRotation) as GameObject;
        trail.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", trailOffset);
        trail.transform.SetParent(GroundController.instance.treeparent.transform);
    }

    private void CreateSplash()
    {
        if (m_Splashs == null)
        {
            return;
        }

        Vector2 splashOffset = new Vector2(Random.Range(0, 4) * 0.25f, Random.Range(0, 4) * 0.25f);
        Quaternion decalRotation = Quaternion.Euler(new Vector3(90, Random.Range(0f, 360f), 0));
        GameObject splash = Instantiate(m_Splashs.gameObject, new Vector3(m_Cube.transform.position.x, m_Cube.bounds.min.y + 0.02f, m_Cube.transform.position.z), decalRotation) as GameObject;
        splash.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", splashOffset);
        splash.transform.SetParent(GroundController.instance.treeparent.transform);
    }

    public void addHealth()
    {
        SoundController.instance.PlaySound(SoundController.Type.Heal);
        health += 25;
        if (health > 100)
            health = 100;
        canvasUpdate();
        Haptic.Instance.HapticSoft();
    }
}

[Serializable]
public class ProjectileUpgrades
{
    public int ProjectileCount;
    public int ProjectileRepeat;
    public bool ProjectileAngular;
}
