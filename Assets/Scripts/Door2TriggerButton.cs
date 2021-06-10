using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Door;
using agents;

public class Door2TriggerButton : MonoBehaviour
{
    [SerializeField]
    GameObject Door2;

    bool isOpened = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!isOpened)
        {
            isOpened = true;
            Door2.transform.position += new Vector3(-0.561f, -1.23f, 2.13f);
        }
    }
}
