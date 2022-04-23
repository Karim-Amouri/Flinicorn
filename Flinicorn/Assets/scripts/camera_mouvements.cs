using System.Collections;
using UnityEngine;

public class camera_mouvements : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float CameraDistance = 30.0f;
    [SerializeField] private float CameraHeight = 0.0f;

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / CameraDistance);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y + CameraHeight, transform.position.z);
    }
}
