using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SmertAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SmertAgent : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;

        // Start is called before the first frame update
        void Start()
        {
            stateMachine.Setup(gameObject.GetComponent<NavMeshAgent>());
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Run();
        }
    }
}