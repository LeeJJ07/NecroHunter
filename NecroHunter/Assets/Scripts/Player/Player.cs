using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerHarvestState HarvestState { get; private set; } 
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    #endregion

    #region Other Variables
    [SerializeField]
    private PlayerData playerData;

    public IHarvestable HarvestableTarget { get; private set; }

    [SerializeField]
    private GameObject curWeapon;
    [SerializeField]
    private GameObject[] tools;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        HarvestState = new PlayerHarvestState(this, StateMachine, playerData, "harvest");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Agent = GetComponent<NavMeshAgent>();

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

    public void EquipTool(int toolIndex)
    {
        curWeapon.SetActive(false);
        tools[toolIndex].SetActive(true);
    }
    public void UnEquipTool(int toolIndex)
    {
        tools[toolIndex].SetActive(false);
        curWeapon.SetActive(true);

        HarvestableTarget = null;
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
    void OnDrawGizmosSelected()
    {
        if (playerData == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerData.detectionRadius);
    }

}
