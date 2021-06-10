using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agents;

namespace Door
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        //[SerializeField] private float speed = 1f;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = start.position;
            
        }

        // Update is called once per frame
        void Update()
        {
            //transform.position = Vector3.Lerp(start.position, end.position, Mathf.PingPong(Time.time * speed, 1));
            transform.rotation = start.rotation;
        }
        private void OnTriggerEnter(Collider other)
        {
            
        }

        // Add switch/button for the accesible doors, using a list might be viable in this situation, you should now have to press switches to open doors   *
        // Threat - Done
        // End goal area   *
        // Designated to Door2 - Two Switches for the AI to choose from in order to open a door   *
        // Also for the final 3rd door it needs a key as a collectable item   *
        // AI needs to be an animated character   *
        // Start function with a quit button - done 
        // Reset button that will start the maze all over again  *


    }
}

