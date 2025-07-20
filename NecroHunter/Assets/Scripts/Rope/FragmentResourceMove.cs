using System.Collections;
using System.Collections.Generic;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;
using UnityEngine.AI;

public class FragmentResourceMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rope rope;
    private Rigidbody rb;

    float followDistance = 2.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float maxDistance = 2.0f;
        float currentDistance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float pullForce = 10f;

        rope.ropeLength = currentDistance + 0.25f;

        if (currentDistance > maxDistance)
        {
            rb.AddForce(direction * pullForce, ForceMode.Force);
        }
    }
}
