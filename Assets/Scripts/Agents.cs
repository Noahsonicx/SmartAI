using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace agents
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Agents : MonoBehaviour
    {
        public bool isThreat;

        private NavMeshAgent agent;
        private List<Waypoints> waypoints = new List<Waypoints>();
        private List<Waypoints> threatWaypoints = new List<Waypoints>();

        // Generating a random waypoint range for the different agents
        private Waypoints NextPoint => isThreat ? threatWaypoints[Random.Range(0, threatWaypoints.Count)] : waypoints[GetIndex()];

        private int currentWaypoint;

        // Start is called before the first frame update
        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            //FindObjectsOfType gets every instance of this component in the scene
            Waypoints[] allWaypoints = FindObjectsOfType<Waypoints>();
            foreach (Waypoints waypoint in allWaypoints)
            {
                // if waypoint is threat then add it to threatWaypoints
                if (waypoint.isThreat)
                    threatWaypoints.Add(waypoint);
                // Else added it to regular waypoints 
                else
                    waypoints.Add(waypoint);
            }

            // telling the agent to move to a random position in the waypoints in the scene
            agent.SetDestination(NextPoint.Position);
        }

        // Update is called once per frame
        void Update()
        {
            // Locating how far the agent is from his objective
            if (!agent.pathPending && agent.remainingDistance == 0)
            {
                // Setting the next waypoint position or current position
                agent.SetDestination(NextPoint.Position);
            }

            // We can't reach our destination, so we should swap to the button state
            if(agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                Debug.LogError("HALP, NO PETH");
            }
        }
        // add a start and end for each door

        private int GetIndex()
        {
            int index = currentWaypoint++;
            if(currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
            }

            return index;
        }
    }

}
