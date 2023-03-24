using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] TriggerEventData[] triggerEventData;

    private void OnTriggerEnter(Collider other)
    {
        foreach (var data in triggerEventData)
        {
            if (other.CompareTag(data.tag))
            {
                data.TriggerEvents.Invoke();
                if (other.CompareTag("Enemy"))
                {
                    other.GetComponent<EnemyController>().death();
                }

                if (data.KillCollisionObject)
                    Destroy(other.gameObject);
            }
        }
    }
}

[Serializable]
public class TriggerEventData
{
    public string tag;
    public UnityEvent TriggerEvents;
    public bool KillCollisionObject;
}
