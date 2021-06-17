using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmertAI;

namespace CollectableScoring
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private Transform door;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<SmertAgent>())
            {
                //ScoringSystem.theScore += 50;
                door.position += Vector3.up * 5;
                Destroy(gameObject);
            }
        }
    }
}
