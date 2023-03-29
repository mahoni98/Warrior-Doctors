using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class GroundController : MonoBehaviour
{
    [SerializeField] Chapter[] Chapters;

    [SerializeField] List<Chapter> realChapters;

    public float EnemyKill = 0;

    int myChapter = 0;

    [SerializeField] Material GroundMaterial;

    public static GroundController instance;

    [SerializeField] TextMeshProUGUI chapterText;

    [SerializeField] Slider levelSlider;

    [SerializeField] List<GameObject> grounds;

    [SerializeField] List<GameObject> trees;

    public GameObject treeparent, SliderObject,canvasParent, canvasEffect;

    Vector3 sliderObjectScale;

    [SerializeField]
    [Range(0, 20f)] float tilingSpeed;
    [SerializeField] float treeParentSpeed;

    public List<GameObject> enemies;

    [SerializeField] Image SceneGecisFade;

    [SerializeField] GameObject portalObject;

    PlayerSwipe ps;
    PlayerController pc;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        ps = PlayerSwipe.instance;
        pc = PlayerController.instance;
        
        enemies = new List<GameObject>();

        realChapters = new List<Chapter>();
        
        for (int x = 0; x < 50; x++)
        {
            for (int i = 0; i < Chapters.Length; i++)
            {
                realChapters.Add(Chapters[i]);
            }
        }

        Chapters = realChapters.ToArray();

        sliderObjectScale = SliderObject.transform.localScale;
        //GroundMaterial.DOColor(Chapters[0].groundMaterial.color, 1f);
        Debug.Log("ChangeMat");

        chapterText.text = (myChapter + 1).ToString();
        levelSlider.value = EnemyKill / Chapters[1].EnemyCount;

        List<GameObject> newlist = new List<GameObject>();

        foreach (var obj in grounds)
        {
            var _obj = Instantiate(Chapters[0].prefab, obj.transform.position, Quaternion.identity);
            newlist.Add(_obj);
            _obj.transform.SetParent(this.transform);
            Destroy(obj);
        }

        grounds = newlist;

        yield return new WaitForEndOfFrame();

        GroundMaterial = Chapters[0].groundMaterial;

        treeparent = new GameObject();

        treeparent.name = "Tree_Parent";
        
        for (int t = 0; t < 2; t++)
        {
            for (int i = 0; i < 30f; i++)
            {
                float rand1 = UnityEngine.Random.Range(-10f, 10f);
                float rand2 = UnityEngine.Random.Range(-10f, 10f);

                int x = UnityEngine.Random.Range(0, 10);
                int y = UnityEngine.Random.Range(0, 10);

                var treeobj = Instantiate(trees[UnityEngine.Random.Range(0, trees.Count)]);

                if (rand1 > 0)
                    if (rand2 > 0)
                        treeobj.transform.position = new Vector3(x * i + 5 + rand1, 0, -y * i + 3 + rand2);
                    else
                        treeobj.transform.position = new Vector3(x * i + 2 + rand2, 0, y * i + 5 + rand1);
                else
                    if (rand2 > 0)
                    treeobj.transform.position = new Vector3(-x * i + 3 + rand1, 0, -y * i + 3 + rand2);
                else
                    treeobj.transform.position = new Vector3(-x * i + 5 + rand2, 0, y * i + 5 + rand1);


                treeobj.transform.SetParent(treeparent.transform);
            }
        }
        
    }

    private void FixedUpdate()
    {
        GroundMaterial.mainTextureOffset = new Vector2(transform.position.x, transform.position.z) / -tilingSpeed;

        if (treeparent != null)
            treeparent.transform.position = -new Vector3(transform.position.x, 0, transform.position.z) * treeParentSpeed;
    }

    public void AddKill()
    {
        EnemyKill++;
        if (Chapters.Length - 1 > myChapter)
            if (EnemyKill >= Chapters[myChapter].EnemyCount)
            {
                myChapter++;
                EnemyKill = 0;
                //GroundMaterial.DOColor(Chapters[myChapter].groundMaterial.color, 1f);

                Debug.Log("ChangeMat");

                StartCoroutine(NewWave());
            }

        chapterText.text = (myChapter + 1).ToString();

        levelSlider.value = EnemyKill / Chapters[myChapter].EnemyCount;

        SliderObject.transform.localScale = sliderObjectScale;
        SliderObject.transform.DOScale(sliderObjectScale * 1.3f, 0.2f).OnComplete(() =>
        {
            SliderObject.transform.DOScale(sliderObjectScale, 0.2f);
        });
    }

    IEnumerator NewWave()
    {

        ps.enabled = false;
        pc.enabled = false;

        Time.timeScale = 1;
        var portal = Instantiate(portalObject);
        
        portal.transform.SetParent(PlayerController.instance.transform);
        portal.transform.localPosition = Vector3.zero;
        portal.transform.localScale = Vector3.one ;
        portal.transform.DOScale(Vector3.one * 3f, 1f);

        //var ds = portal.AddComponent<Destoryer>();
        //ds.destroyTiming = 1f;

        Camera.main.DOFieldOfView(60, 1.5f).OnComplete(() =>
        {
            
        });
        
        yield return new WaitForSecondsRealtime(1f);
        
        ps.enabled = false;
        pc.enabled = false;
        
        //Time.timeScale = 1;
        SceneGecisFade.enabled = true;
        Time.timeScale = 1;

        SceneGecisFade.color = new Color(SceneGecisFade.color.r, SceneGecisFade.color.g, SceneGecisFade.color.b, 0);
        
        SceneGecisFade.DOFade(1, 1).OnComplete(() =>
        {
            portal.SetActive(false);
            Camera.main.DOFieldOfView(90, 1f).OnComplete(() =>
            {

            });
            
            if (enemies.Count > 0)
            {
                foreach (var obj in enemies)
                {
                    Destroy(obj);
                }
                enemies.Clear();
            }

            ///
            List<GameObject> newlist = new List<GameObject>();

            foreach (var obj in grounds)
            {
                var _obj = Instantiate(Chapters[myChapter].prefab, obj.transform.position, Quaternion.identity);
                newlist.Add(_obj);
                _obj.transform.SetParent(this.transform);
                Destroy(obj);
            }

            grounds = newlist;

            GroundMaterial = Chapters[myChapter].groundMaterial;
            ////
            ///
            //portal.SetActive(false);
            SceneGecisFade.DOFade(0, 1).OnComplete(() =>
            {
                SceneGecisFade.enabled = false;

                var objEffect = Instantiate(canvasEffect);
                objEffect.transform.SetParent(canvasParent.transform);
                objEffect.transform.localPosition = Vector3.zero + transform.up * 800f;
                objEffect.GetComponent<UiEffect>().effectName = "Chapter : " + (myChapter + 1).ToString();
                
                ps.enabled = true;
                pc.enabled = true;
            });
        });
    }
}

[Serializable]
public class Chapter
{
    public int EnemyCount;
    public Material groundMaterial;
    public GameObject prefab;
}