using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) { Destroy(gameObject); }
    }
    public List<AdsInfo> _Ads;
    public string SetAdsId(AdsType Type)
    {
        foreach (var item in _Ads)
        {
            if (item.Type == Type)
            {
                return item.Decision();
            }
        }
        return null;
    }
    public enum AdsType
    {
        Banner, Instertitial, Rewarded
    }

    [Serializable]
    public class AdsInfo
    {
        public AdsType Type;
        public string IdTest;
        public string Id;
        public bool Test;

        public string Decision()
        {
            if (Test)
            {
                Id = IdTest;
                return Id;
            }
            else
                return Id;
        }
    }
}

