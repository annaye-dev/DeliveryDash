using System;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public bool hasPackage = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Package"))
        {
            if (!hasPackage)
            {
                hasPackage = true;
                GetComponent<ParticleSystem>().Play();
                Destroy(collision.gameObject, 0.5f);
            }
        }
        else if (collision.CompareTag("Customer"))
        {
            if (hasPackage)
            {
                hasPackage = false;
                GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
