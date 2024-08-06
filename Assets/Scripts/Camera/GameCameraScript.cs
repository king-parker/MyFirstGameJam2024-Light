using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraScript : MonoBehaviour
{
    public GameObject player;
    public float minXPos = -10000;
    public float maxXPos = 10000;
    public float minYPos = -10000;
    public float maxYPos = 10000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var xCoord = Mathf.Clamp(player.transform.position.x, minXPos, maxXPos);
        var yCoord = Mathf.Clamp(player.transform.position.y, minYPos, maxYPos);
        var zCoord = transform.position.z;
        transform.position = new Vector3(xCoord, yCoord, zCoord);
    }
}
