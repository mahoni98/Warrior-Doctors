using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] GameObject UpgradeUI, SliderObject, canvasParent, canvasEffect;
    public static UpgradeSystem instance;

    [SerializeField] Levels[] PlayerLevels;

    float coinCount = 0;
    int PlayerLevel = 0;

    PlayerController pc;

    [SerializeField] Image levelSlider;

    [SerializeField] TextMeshProUGUI MovementLevelText, BulletWaveLevelText, BulletCountText, ShotGunText, levelText;
    [SerializeField] Button RangeBtn;

    [SerializeField] int MovementLevel, BulletWaveLevel, BulletCountLevel = 0, ShotGunLevel;

    public bool isPaused = false;

    public PlayerSwipe ps;

    Vector3 scale;

    [SerializeField] Sprite maxLevelBackground;

    [SerializeField] Image shotGunImage;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 5f;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            Movement();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            BulletCount();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            BulletWave();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ShotGun();
        }
    }

    public void AddCoin()
    {
        coinCount++;

        if (PlayerLevels.Length - 1 >= PlayerLevel)
        {
            if (coinCount >= PlayerLevels[PlayerLevel].CoinCount)
            {
                isPaused = true;

                ps.enabled = false;
                pc.enabled = false;
                ps.gameObject.SetActive(false);
                //pc.gameObject.SetActive(false);

                PlayerLevel++;
                coinCount = 0;
                UpgradeUI.SetActive(true);

                DG.Tweening.DOTween.KillAll();

                Time.timeScale = 0;

                var obj = Instantiate(canvasEffect);
                obj.transform.SetParent(canvasParent.transform);
                obj.transform.localPosition = Vector3.zero + transform.up * -100f;
                obj.GetComponent<UiEffect>().effectName = "Level : " + (PlayerLevel + 1).ToString();

                Time.timeScale = 0;

            }
        }
        //scale = SliderObject.transform.localScale;
        //SliderObject.transform.DOKill();
        //SliderObject.transform.DOScale(scale*1.3f, 0.2f).OnComplete(() =>
        //{
        //    SliderObject.transform.DOScale(scale, 0.2f);
        //});
        canvasUpdate();
    }

    private void Start()
    {
        //scale = SliderObject.transform.localScale;
        UpgradeUI.SetActive(false);
        pc = PlayerController.instance;
        canvasUpdate();
    }

    public void Movement()
    {
        PlayerSwipe.instance.MovementRange++;
        MovementLevel++;

        Time.timeScale = 1;
        UpgradeUI.SetActive(false);
        pc.enabled = true;
        isPaused = false;
        ps.enabled = true;
        RangeBtn.interactable = true;

        canvasUpdate();
        UpgradeAfter();
    }

    public void BulletCount()
    {
        PlayerController.instance.PU.ProjectileCount++;
        BulletCountLevel++;
        Time.timeScale = 1;
        UpgradeUI.SetActive(false);
        pc.enabled = true;
        isPaused = false;
        ps.enabled = true;
        RangeBtn.interactable = true;

        canvasUpdate();
        UpgradeAfter();
    }

    public void BulletWave()
    {
        PlayerController.instance.PU.ProjectileRepeat++;

        BulletWaveLevel++;

        Time.timeScale = 1;
        UpgradeUI.SetActive(false);
        pc.enabled = true;
        isPaused = false;
        ps.enabled = true;
        RangeBtn.interactable = true;

        canvasUpdate();
        UpgradeAfter();
    }

    public void ShotGun()
    {
        if (!PlayerController.instance.PU.ProjectileAngular)
        {
            PlayerController.instance.PU.ProjectileAngular = true;
            PlayerController.instance.PU.ProjectileCount++;
            ShotGunLevel++;
            Time.timeScale = 1;
            UpgradeUI.SetActive(false);
            pc.enabled = true;
            isPaused = false;
            ps.enabled = true;
        }
        else
        {
            RangeBtn.interactable = false;
        }
        canvasUpdate();
        UpgradeAfter();

    }

    void canvasUpdate()
    {
        if (PlayerLevels.Length - 1 >= PlayerLevel)
            levelSlider.fillAmount = coinCount / PlayerLevels[PlayerLevel].CoinCount;

        if (PlayerController.instance.PU.ProjectileAngular)
        {
            shotGunImage.sprite = maxLevelBackground;
            ShotGunText.text = "Level Max";
        }
        else
        {
            ShotGunText.text = "Level 0";
        }

        ShotGunText.text = ShotGunLevel.ToString();
        MovementLevelText.text = MovementLevel.ToString();
        BulletCountText.text = BulletCountLevel.ToString();
        BulletWaveLevelText.text = BulletWaveLevel.ToString();

        //levelText.text = (PlayerLevel + 1).ToString();

    }
    void UpgradeAfter()
    {
        ps.gameObject.SetActive(true);
        //pc.gameObject.SetActive(true);
        Time.timeScale = 0.01f;
    }
}

[Serializable]
public class Levels
{
    public int CoinCount;
}
