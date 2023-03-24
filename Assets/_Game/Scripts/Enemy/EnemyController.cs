using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _speed = 1;

    [SerializeField] Transform _target;

    [SerializeField] Rigidbody _rb;

    [Header("Decals")]

    public GameObject m_Trails, LiquidSplat;

    public GameObject coin, heart;

    private void Start()
    {
        coin.SetActive(false);
        heart.SetActive(false);
        if (!_target)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void FixedUpdate()
    {
        transform.LookAt(_target.position);

        _rb.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            death();
        }
    }

    public void death()
    {
        GroundController.instance.AddKill();
        CreateTrail();
        Destroy(this.gameObject);
    }

    private void CreateTrail()
    {
        if (Random.Range(0, 100f) >= 30) //70percent
        {
            coin.SetActive(true);
            coin.GetComponent<GoToPlayer>().GoPlayer();
            coin.transform.SetParent(GroundController.instance.treeparent.transform);
        }
        else
        {
            if (Random.Range(0, 100f) >= 66f)
            {
                heart.SetActive(true);
                heart.transform.SetParent(GroundController.instance.treeparent.transform);
                var dss = heart.AddComponent<Destoryer>();
                dss.destroyTiming = 10f;
            }
        }

        var obj = Instantiate(LiquidSplat, transform.position, Quaternion.identity);
        var trail = Instantiate(m_Trails.gameObject, transform.position, Quaternion.identity);
        obj.transform.SetParent(trail.transform);
        var ds = trail.AddComponent<Destoryer>();
        ds.isFade = true;
        ds.isScale = true;

        trail.transform.SetParent(GroundController.instance.treeparent.transform);
        
    }
}
