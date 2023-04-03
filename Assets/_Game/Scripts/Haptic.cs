using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;

public class Haptic : SingletonManager<Haptic>
{
    // jump 
    // damage 
    // pub
    [SerializeField] private Image HapticImage;
    [SerializeField] private Sprite Open;
    [SerializeField] private Sprite Close;



    public int _Haptic => PlayerPrefs.GetInt("_Haptic", 1); // 1 is open Haptic
    public void HapticSoft()
    {
        if (_Haptic == 1)
            MMVibrationManager.Haptic(HapticTypes.SoftImpact, true);
    }
    public void HapticDamage()
    {
        if (_Haptic == 1)
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, true);
    }

    public void SetHaptic()
    {
        if (_Haptic == 0)
        {
            PlayerPrefs.SetInt("_Haptic", 1);
            HapticImage.sprite = Open;
        }
        else
        {
            PlayerPrefs.SetInt("_Haptic", 0);
            HapticImage.sprite = Close;
        }
    }
}
