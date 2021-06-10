using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Door;
using agents;
public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField]
    GameObject Door1;


    bool isOnOpened = false;

    void OnTriggerEnter(Collider other)
    {
        if (!isOnOpened)
        {
            isOnOpened = true;
            Door1.transform.position += new Vector3(0, 4, 0);
        }
    }
}
