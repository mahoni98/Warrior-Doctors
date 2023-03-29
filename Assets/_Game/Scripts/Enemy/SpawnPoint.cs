using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    public float RandomizeX, RandomizeZ;
    public float spawnRate;

    [SerializeField] GameObject[] _prefabs;
    [SerializeField] Transform _PrefabParent;



    private void Start()
    {
        StartCoroutine(Spawn());
        var tr = GameObject.Find("Player").transform;
        transform.SetParent(tr);
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            System.Random _Random = new System.Random();

            var _enemy = Instantiate(_prefabs[_Random.Next(0, _prefabs.Length)], new Vector3(Random.Range(-RandomizeX, RandomizeX) + transform.position.x,
                0, Random.Range(-RandomizeZ, RandomizeZ) + transform.position.z), Quaternion.identity);

            _enemy.transform.SetParent(GroundController.instance.treeparent.transform);
            GroundController.instance.enemies.Add(_enemy);
        }
    }
}
