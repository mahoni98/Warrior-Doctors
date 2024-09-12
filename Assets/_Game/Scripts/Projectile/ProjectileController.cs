using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class ProjectileController : MonoBehaviour
{
    public float speed = 8f;
    public float distance = 10f; // Merminin gideceði mesafe
    public Ease easeType = Ease.Linear; // Ease tipi


    void Start()
    {
        // Merminin hedef pozisyonunu hesaplayalým (mesafe kadar ileri)
        Vector3 targetPosition = transform.position + transform.forward * distance;

        // DoTween ile hareket ettiriyoruz
        transform.DOMove(targetPosition, distance / speed) // Hareket süresi = mesafe / hýz
            .SetEase(easeType); // Ease tipini burada ekliyoruz
    }
}
