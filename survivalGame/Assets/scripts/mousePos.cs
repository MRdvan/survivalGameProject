using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePos : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var pos = Input.mousePosition;
        pos.z = 45;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = pos;
    }
}
