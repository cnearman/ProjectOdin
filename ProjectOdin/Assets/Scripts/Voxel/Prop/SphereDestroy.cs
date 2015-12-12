using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereDestroy : MonoBehaviour
{
    List<GameObject> hits = new List<GameObject>();


    // Use this for initialization
    void Start()
    {

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
