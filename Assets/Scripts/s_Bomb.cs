using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Bomb : MonoBehaviour
{
    [Header("Explosion Variables")]
    [SerializeField]
    private bool explode;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float power;

    void Start()
    {
        // set explosion
        explode = false;
    }   

    void Update()
    {
        if (explode)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Script found in Unity Documentation
        // https://docs.unity3d.com/ScriptReference/Rigidbody.AddExplosionForce.html
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
        }

        // reset explosion
        explode = false;
    }
}
