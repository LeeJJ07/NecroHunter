using DG.Tweening;
using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;
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

        rope = ObjectPoolManager.SpawnObject(ropePrefab, transform.position, Quaternion.identity);

        rope.SetStartPoint(player.GetComponent<Player>().RopePoint);
        rope.SetEndPoint(transform);
        rope.RecalculateRope();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "ResourceCollectorZone":
                MoveToCenter(other.transform.position);
                break;

            case "BaseCamp":
                GetResource();
                break;

            default:
                break;
        }
    }
    private void MoveToCenter(Vector3 center)
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float duration = 1.2f;
        float arcHeight = 1.0f;

        Vector3 startPos = transform.position;
        Vector3 endPos = center;

        Vector3 midPoint = (startPos + endPos) * 0.5f;
        midPoint.y += arcHeight;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOJump(midPoint, arcHeight, 1, duration * 0.5f).SetEase(Ease.OutQuad));
        seq.Append(transform.DOMove(endPos, duration * 0.5f).SetEase(Ease.InQuad));

        seq.OnComplete(() =>
        {
            rb.useGravity = true;
        });

        ReturnRope();
        player.GetComponent<Player>().ReturnHaulResource();
    }
    
    private void GetResource()
    {
        player = null;
        IsLinkedToPlayer = false;

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    private void ReturnRope()
    {
        ObjectPoolManager.ReturnObjectToPool(rope.gameObject);
    }
}
