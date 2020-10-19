using UnityEngine;

public class PlanetsTrickery : MonoBehaviour
{

    [SerializeField] private Transform player;
    private Vector3 startPosOffset;

    private void Start()
    {
        startPosOffset = transform.position;
    }

    private void Update()
    {
        transform.position = startPosOffset + player.position;
    }
}
