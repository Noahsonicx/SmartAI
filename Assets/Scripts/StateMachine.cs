using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SmertAI
{
    public delegate void AIStateDelegate();

    public enum AIState
    {
        FollowingPath,
        TargettingCollectable,
        TargettingSwitch
    }


    [System.Serializable]
    public class StateMachine
    {
        [SerializeField] private Waypoints[] waypoints;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] GameObject Door1;

        private Dictionary<AIState, AIStateDelegate> states = new Dictionary<AIState, AIStateDelegate>();
        [SerializeField] private AIState currentState = AIState.FollowingPath;

        private Transform closestSwitch;

        public void Setup(NavMeshAgent _agent)
        {
            agent = _agent;

            // Sync up the states with the relevant functions
            states.Add(AIState.FollowingPath, FollowPathState);
            states.Add(AIState.TargettingCollectable, TargetCollectableState);
            states.Add(AIState.TargettingSwitch, TargetSwitchState);
        }

        public void Run()
        {
            // Get the relevant function from the state and run it
            if(states.TryGetValue(currentState, out AIStateDelegate state))
                state.Invoke();
        }

        public void SwitchState(AIState _newState) => currentState = _newState;

        private void FollowPathState()
        {
            Debug.Log("FollowPathState");
            agent.SetDestination(waypoints[1].Position);

            if(agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                DoorTriggerButton[] buttons = GameObject.FindObjectsOfType<DoorTriggerButton>();
                closestSwitch = null;
                float closestDistance = float.MaxValue;
                foreach (DoorTriggerButton button in buttons)
                {
                    float distance = Vector3.Distance(button.transform.position, agent.transform.position);
                    if (distance < closestDistance)
                    {
                        closestSwitch = button.transform;
                        closestDistance = distance;
                    }
                }

                SwitchState(AIState.TargettingSwitch);
                // Swao to target state
            }
            // Tell the agent to move to the current waypoint

            // I can't reach the waypoint, so I will try to find a switch
                // I found a switch, so move to the TargetSwitchState
            
            // There is a collectable nearby, let's grab it!
        // Vector3.Distance
        // agent.pathStatus == NavMeshPathStatus.PathPartial // Can't find my way to the waypoint
        }

        private void TargetCollectableState()
        {
            Debug.Log("TargetCollectableState");
        }

        private void TargetSwitchState()
        {
            agent.SetDestination(closestSwitch.position);

            if (agent.remainingDistance <= .1f)
            {
                Door1.transform.position += new Vector3(0, 4, 0);
                closestSwitch = null;
                SwitchState(AIState.FollowingPath);
            }
        }
    } 
}
