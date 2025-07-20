using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerHarvestState HarvestState { get; private set; } 
    public PlayerHaulingState HaulingState { get; private set; }
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    public StatHandler StatHandler { get; private set; }
    #endregion

    #region Other Variables
    [SerializeField]
    private PlayerData playerData;

    private int currentHaulCount;
    public IHarvestable HarvestableTarget { get; private set; }

    [SerializeField]
    private GameObject curWeapon;
    [SerializeField]
    private GameObject[] tools;

    public Transform RopePoint { get { return ropePoint; } }
    [SerializeField] private Transform ropePoint;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        HarvestState = new PlayerHarvestState(this, StateMachine, playerData, "harvest");
        HaulingState = new PlayerHaulingState(this, StateMachine, playerData, "hauling");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Agent = GetComponent<NavMeshAgent>();
        StatHandler = GetComponent<StatHandler>();

        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region About HaulingState Functions
    public bool IsHauling()
    {
        return currentHaulCount > 0;
    }
    public bool CanGetHaulResource()
    {
        return currentHaulCount < playerData.maxCountHaulResource;
    }
    public void GetHaulResource()
    {
        currentHaulCount++;
        StatHandler.ModifyStat(EStatType.MOVE_SPEED, playerData.decreaseSpeedPerHaulResource);
    }
    public void ReturnHaulResource() 
    {
        currentHaulCount--;
        StatHandler.ModifyStat(EStatType.MOVE_SPEED, -playerData.decreaseSpeedPerHaulResource);
    }

    public void ClearHaulResource()
    {
        float recoveredSpeed = -playerData.decreaseSpeedPerHaulResource * currentHaulCount;
        StatHandler.ModifyStat(EStatType.MOVE_SPEED, recoveredSpeed);
        currentHaulCount = 0;
    }
    #endregion

    #region About HarvestState Functions
    public void SetToolActive(int toolIndex, bool active)
    {
        curWeapon.SetActive(!active);
        tools[toolIndex].SetActive(active);

        if (!active) HarvestableTarget = null;
    }

    public Vector3 FindHarvestableObject()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, playerData.detectionRadius);

        foreach(var hit in hits)
        {
            IHarvestable harvestable = hit.GetComponent<IHarvestable>();
            if (harvestable == null)
                continue;

            HarvestableTarget = harvestable;
            return hit.transform.position;
        }

        return Vector3.zero;
    }
    #endregion

    #region Animator Event
    public void PerformHarvestAction()
    {
        HarvestableTarget?.Harvested(5);
    }
    #endregion

    #region Gizmos
    void OnDrawGizmosSelected()
    {
        if (playerData == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerData.detectionRadius);
    }
    #endregion
}
