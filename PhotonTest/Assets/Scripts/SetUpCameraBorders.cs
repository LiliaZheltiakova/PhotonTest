using UnityEngine;
using Cinemachine;

public class SetUpCameraBorders : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameObject.FindGameObjectWithTag("CameraBorders").GetComponent<PolygonCollider2D>();
    }
}
