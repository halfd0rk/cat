using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HibbyGames
{
    //This script tells a NavMesh Agent to walk to a random destination//
    //The gameObject you attach this script to must have a NavMeshAgent component//
    [RequireComponent(typeof(NavMeshAgent))]
    public class navController : MonoBehaviour
    {
        public bool randomDestination = false;
        public float randomDestRange = 20f;
        public float randomDestWaitTimeMin = 3f;
        public float randomDestWaitTimeMax = 4f;

        private NavMeshAgent nav;
        private float waitF = 0f;

        // Start is called before the first frame update
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            waitF = Random.Range(randomDestWaitTimeMin, randomDestWaitTimeMax);
        }

        // Update is called once per frame
        void Update()
        {
            //set destination to a random position//
            if(randomDestination)
            {
                if(waitF > 0f)
                {
                    waitF -= Time.deltaTime;
                }
                else
                {
                    waitF = Random.Range(randomDestWaitTimeMin, randomDestWaitTimeMax);

                    Vector3 randomPos = UnityEngine.Random.insideUnitSphere* randomDestRange;
                    NavMeshHit navHit;
                    NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);
                    nav.SetDestination(navHit.position);
                }
            }
        }
    }
}
//1.0.0 - Hibby Games//
