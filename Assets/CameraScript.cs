using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = player.transform.position;
        var position = transform.position;
        position.x = playerPosition.x;
        position.y = playerPosition.y + 2;
        position.z = playerPosition.z - 8;
        transform.position = position;
    }
}
