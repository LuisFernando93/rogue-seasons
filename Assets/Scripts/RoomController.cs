using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RoomGrid grid = new RoomGrid(8, 8, 0.64f, new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
