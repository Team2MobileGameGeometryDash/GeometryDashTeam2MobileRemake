using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Player State Manager")]
    public PlayerStateManager PlayerStateManager;
    [HideInInspector]
    public PlayerInputManager PlayerInputManager;
    [HideInInspector]
    public VFXManager VFXManager;
    [HideInInspector]
    public Animator animator;

    [Header("Player input and Player locomotion")]
    [HideInInspector]
    public Rigidbody2D PlayerRigidBody2D;
    [HideInInspector]
    public Collider2D PlayerCollider2D;

    public PlayerMouvement PlayerMouvement;
    [HideInInspector]
    public Vector3 InitialPosition;


    public SpriteRenderer PlayerSpriteRenderer;
    public GameObject PlayerSprite;

    #region PlayerData
    public PlayerSOBaseData PlayerSOBaseData;

    public PlayerSOCubeCharacter PlayerSOCubeCharacter;

    public PlayerSOShipCharacter PlayerSOShipCharacter;

    public PlayerSOGearModeCharacter PlayerSOGearModeCharacter;

    public PlayerSOUfoCharacter PlayerSOUfoCharacter;

    public PlayerSORobotCharacter PlayerSORobotCharacter;

    #endregion

    private void Awake()
    {
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerInputManager = GetComponent<PlayerInputManager>();
        VFXManager = GetComponent<VFXManager>();
        PlayerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PlayerStateManager = new PlayerStateManager(this);
        PlayerMouvement = new PlayerMouvement(this);
        
        //Debug.Log(InitialPosition);
    }

    private void Start()
    {
        PlayerCollider2D = GetComponent<Collider2D>();
        InitialPosition = transform.position;
        PlayerSOBaseData.WalkingSpeed = PlayerSOBaseData.DefaultWalkingSpeed;
        PlayerSOBaseData.Direction = 1;
    }


    private void Update()
    {
        //Debug.Log(data.CanJump);
        PlayerStateManager.CurrentState.OnUpdate();
        //Debug.Log("istouchended"  +  PlayerInputManager.IsTouchEnded );
        //Debug.Log("istouchedstationary"  +  PlayerInputManager.IsTouchStationary );
   
    }

    private void FixedUpdate()
    {
        PlayerStateManager.CurrentState.OnFixedUpdate();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateManager.CurrentState.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            PlayerStateManager.ChangeState(PlayerState.Death);
    }
    

}







