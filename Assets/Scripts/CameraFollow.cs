using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;
        Vector3 newPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        transform.position = newPosition;
    }
}
