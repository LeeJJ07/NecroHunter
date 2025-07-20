using System.Collections;
using System.Collections.Generic;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;
using UnityEngine.AI;

public class FragmentResource : MonoBehaviour
{
    [SerializeField] private Rope ropePrefab;
    private Rope rope;

    [SerializeField] private GameObject player;
    private Rigidbody rb;

    private float maxFollowDistance = 2.0f;
    private float pullForce = 15.0f;
    private float ropeLengthOffset = 0.25f;
    public bool IsLinkedToPlayer { get; private set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (player != null)
            Move();
    }

    private void Move()
    {
        float currentDistance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 direction = (player.transform.position - transform.position).normalized;

        rope.ropeLength = currentDistance + ropeLengthOffset;

        if (currentDistance > maxFollowDistance)
        {
            rb.AddForce(direction * pullForce, ForceMode.Force);
        }
    }

    public void SetPlayer(GameObject curPlayer)
    {
        player = curPlayer;

        IsLinkedToPlayer = true;

        ropePrefab.SetStartPoint(player.GetComponent<Player>().RopePoint);
        ropePrefab.SetEndPoint(transform);
        rope = ObjectPoolManager.Instantiate(ropePrefab, transform.position, Quaternion.identity);
    }
}
