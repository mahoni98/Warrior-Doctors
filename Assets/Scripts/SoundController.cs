using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    #region Singleton

    public static SoundController instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }

        else if (instance != this) { Destroy(gameObject); }
    }

    #endregion
    public int Music => PlayerPrefs.GetInt("Music", 1);// 1 open , // 0 close
    public int Sound => PlayerPrefs.GetInt("Sound", 1);// 1 open , // 0 close


    [SerializeField] private AudioSource GameMusic;
    [SerializeField] private AudioSource JumpSound;
    [SerializeField] private AudioSource EnemyDeathSound;
    [SerializeField] private AudioSource LoseSound;
    [SerializeField] private AudioSource HealSound;
    [SerializeField] private AudioSource PlayerDamageSound;
    [SerializeField] private AudioSource GunSound;




    [Header("GameObject")]
    [SerializeField] private Image MusicImage;
    [SerializeField] private Sprite MusicCloseSprite;
    [SerializeField] private Sprite MusicOpenSprite;

    [SerializeField] private Image SoundImage;
    [SerializeField] private Sprite SoundCloseSprite;
    [SerializeField] private Sprite SoundOpenSprite;


    private void Start()
    {
        StartChechk();
    }

    public void StartChechk()
    {
        if (Music == 1)
        {
            GameMusic.Play();
            MusicImage.sprite = MusicOpenSprite;
        }
        else
        {
            GameMusic.Stop();
            MusicImage.sprite = MusicCloseSprite;
        }
        if (Sound == 1)
        {
            SoundImage.sprite = SoundOpenSprite;
        }
        else
        {
            SoundImage.sprite = SoundCloseSprite;
        }
    }
    public void MusicChangeBtn()
    {
        if (Music == 1)
        {
            GameMusic.Stop();
            MusicImage.sprite = MusicCloseSprite;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            GameMusic.Play();
            MusicImage.sprite = MusicOpenSprite;
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    public void SoundFunc()
    {
        if (Sound == 1)
        {
            SoundImage.sprite = SoundCloseSprite;
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            SoundImage.sprite = SoundOpenSprite;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
    public enum Type
    {
        lose, Jump, Game, Heal, EnemyDeath, PlayerDamage, Gun
    }
    public void SetVolume(float Volume)
    {
        if (Music == 1)
            GameMusic.volume = Volume;
    }
    public void PlaySound(Type Type)
    {
        if (Sound == 1)
        {
            switch (Type)
            {
                case Type.Jump:
                    JumpSound.Play();
                    break;
                case Type.lose:
                    LoseSound.Play();
                    break;
                case Type.EnemyDeath:
                    EnemyDeathSound.Play();
                    break;
                case Type.Game:
                    break;
                case Type.Heal:
                    HealSound.Play();
                    break;
                case Type.PlayerDamage:
                    PlayerDamageSound.Play();
                    break;
                case Type.Gun:
                    GunSound.Play();
                    break;
            }
        }
    }
}
