using UnityEngine;
using System.Collections;

public class ProbeBlock : MonoBehaviour {
    public GameObject myMaster;


	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.5f);
	}

    void OnTriggerStay(Collider other)
    {
        myMaster.GetComponent<MeshToVoxel>().VoxelHere(transform.position);
        Destroy(gameObject);
    }

}
