using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosionEffectDeadTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 0.1f);
    }

}
