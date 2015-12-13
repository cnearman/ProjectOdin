using UnityEngine;

public class ForwardMotion : BaseClass, IProjectileMotion
{
    public float Velocity;

    private PhotonView m_PhotonView;

    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    void Update() 
    {
        if (m_PhotonView.isMine)
        {
            transform.Translate(Vector3.forward * 1.0f * Time.deltaTime * Velocity);
        }
    }

}
