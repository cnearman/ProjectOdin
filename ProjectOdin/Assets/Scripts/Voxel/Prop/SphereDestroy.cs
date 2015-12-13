using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereDestroy : MonoBehaviour
{
    List<GameObject> hits = new List<GameObject>();
    public float life = 0.2f;


    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "voxProp" && !hits.Contains(other.gameObject))
        {
            other.gameObject.GetComponent<VoxelProp>().TakeSphereDamage(gameObject);
            hits.Add(other.gameObject);
        }
    }
}
