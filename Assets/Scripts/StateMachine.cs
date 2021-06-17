using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CollectableScoring;

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
        [SerializeField] GameObject Door3;
        [SerializeField] GameObject Door1;
        [SerializeField] GameObject Door2;

        private Dictionary<AIState, AIStateDelegate> states = new Dictionary<AIState, AIStateDelegate>();
        [SerializeField] private AIState currentState = AIState.FollowingPath;

        private Transform closestSwitch;
        private Transform nearestCollectableDoorway;
        private int index = 0;

        public void Setup(NavMeshAgent _agent)
        {
            agent = _agent;

            // Sync up the states with the relevant functions
            states.Add(AIState.FollowingPath, FollowPathState);
            states.Add(AIState.TargettingCollectable, TargetCollectableState);
            states.Add(AIState.TargettingSwitch, TargetSwitchState);

            agent.SetDestination(waypoints[0].Position);
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
            //Debug.Log("FollowPathState");
            agent.SetDestination(waypoints[index].Position);

            if (agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                // Finds the switch button to turn the door on and locates how much distance it needs to travel to arrive at the switch
                DoorTriggerButton[] buttons = GameObject.FindObjectsOfType<DoorTriggerButton>();
                closestSwitch = null;
                float closestDistance = float.MaxValue;
                foreach (DoorTriggerButton button in buttons)
                {
                    //Distance in order to enter the switch function so that the door may open
                    float distance = Vector3.Distance(button.transform.position, agent.transform.position);
                    if (distance < closestDistance)
                    {
                        closestSwitch = button.transform;
                        closestDistance = distance;
                    }
                }

                agent.SetDestination(closestSwitch.position);
                SwitchState(AIState.TargettingSwitch);
                // Swap to target state
            }

            else if (agent.remainingDistance <= .1f && !agent.pathPending)
            {
                index++;
            }

            #region Jibber Jabber
            //foreach (index++ > .1f)
            //{
            //    SwitchState(AIState.FollowingPath);
            //}
            /*for(int i = 0; i < agent.remainingDistance; i++)
            {
                return;
            }
            */
            // Tell the agent to move to the current waypoint

            // I can't reach the waypoint, so I will try to find a switch
            // I found a switch, so move to the TargetSwitchState

            // There is a collectable nearby, let's grab it!
            // Vector3.Distance
            // agent.pathStatus == NavMeshPathStatus.PathPartial // Can't find my way to the waypoint
            #endregion
        }

        private void TargetCollectableState()
        {
            Debug.Log("TargetCollectableState");

            /*agent.SetDestination(waypoints[index].Position);

            if (agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                Collectable[] collectables = GameObject.FindObjectsOfType<Collectable>();
                nearestCollectableDoorway = null;
                float closestDistance = float.MaxValue;
                foreach (Collectable collectable in collectables)
                {
                    float distance = Vector3.Distance(collectable.transform.position, agent.transform.position);
                    if (distance < closestDistance)
                    {
                        nearestCollectableDoorway = collectable.transform;
                        closestDistance = distance;
                    }
                }

            }
            */
            if (agent.remainingDistance <= .1f && !agent.pathPending)
            {
                Door1.transform.position += new Vector3(0, 4, 0);
                nearestCollectableDoorway = null;
                SwitchState(AIState.FollowingPath);
            }
        }

        private void TargetSwitchState()
        {
            //agent.SetDestination(closestSwitch.position);

            if (agent.remainingDistance <= .1f && !agent.pathPending)
            {
                Door3.transform.position += new Vector3(0, 4, 0);
                closestSwitch = null;
                SwitchState(AIState.FollowingPath);
            }
            else if (agent.remainingDistance <= .1f && !agent.pathPending)
            {
                Door2.transform.position += new Vector3(0, 4, 0);
                closestSwitch = null;
                SwitchState(AIState.FollowingPath);
            }
        }
    } 
}
