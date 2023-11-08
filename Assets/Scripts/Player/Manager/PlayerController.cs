using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Player State Manager")]
    public PlayerStateManager PlayerStateManager;
    [HideInInspector]
    public PlayerInputManager PlayerInputManager;
    [HideInInspector]
    public VFXManager VFXManager;

    [Header("Player input and Player locomotion")]
    [HideInInspector]
    public Rigidbody2D PlayerRigidBody2D;
    [HideInInspector]
    public Collider2D PlayerCollider2D;
    public PlayerData PlayerData;
    public PlayerMouvement PlayerMouvement;
    [HideInInspector]
    public Vector3 InitialPosition;


    public DefaultCharacterData DefaultCharacterData;

    public SpaceShipCharacterData SpaceShipCharacterData;

    public GearModeData GearModeData;

    public UfoCharacterData UfoCharacterData;

    public RobotData RobotData;

    public MeteoraModeData MeteoraModeData;

    
    


    private void Awake()
    {
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerInputManager = GetComponent<PlayerInputManager>();
        VFXManager = GetComponent<VFXManager>();
        PlayerStateManager = new PlayerStateManager(this);
        PlayerMouvement = new PlayerMouvement(this);
        PlayerData.WalkingSpeed = PlayerData.DefaultWalkingSpeed;
        //Debug.Log(InitialPosition);
    }

    private void Start()
    {
        PlayerCollider2D = GetComponent<Collider2D>();
        transform.localScale = PlayerData.DefaultSize;
        InitialPosition = transform.position;
        PlayerData.Direction = 1f;
        PlayerRigidBody2D.gravityScale = DefaultCharacterData.GravityScale;
        
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

    

    public void ChangeCharacter(bool isActive,int index)
    {
        PlayerData.Ships[index].SetActive(isActive);
    }



}




[System.Serializable]
public struct PlayerData
{
    public GameObject[] Ships;
    [Header("PlayerWalkValue")]
    public float DefaultWalkingSpeed;
    [HideInInspector]
    public float WalkingSpeed;
    [HideInInspector]
    public float Direction;
    [Header("PlayerLayer")]
    public LayerMask GroundLayer;
    [HideInInspector]
    public bool isWin;
    [HideInInspector]
    public float Time;
    [HideInInspector]
    public bool IsGravityChange;
    public Vector3 DefaultSize;
    public Vector3 Size;


}

[System.Serializable]
public struct DefaultCharacterData
{
    [Header("PlayerShips")]
    public bool IsDefaultCharacter;
    [Header("PlayerJumpValue")]
    public float JumpHeight;
    public float RotationSpeed;
    
    [Header("PlayerGravity")]
    public float GravityScale;
 
    

}
[System.Serializable]
public struct SpaceShipCharacterData
{
    [Header("PlayerShips")]
    public bool IsSpaceShip;
    public float JumpHeight;
    public float JumpImpulse;
    public SpriteRenderer SpriteRenderer;
    [Header("PlayerGravity")]
    public float GravityScale;

}

[System.Serializable]
public struct GearModeData
{
    [Header("PlayerShips")]
    public bool IsGearMode;    

    [Header("PlayerGravity")]
    public float GravityScale;
    public float GravityVelocity;

 
}

[System.Serializable]
public struct UfoCharacterData
{
    [Header("PlayerShips")]
    public bool IsUfo;
    public float JumpHeight;
    [Header("PlayerGravity")]
    public float GravityScale;
  

}

[System.Serializable]
public struct MeteoraModeData
{
    [Header("PlayerShips")]
    public bool IsMeteora;
    public float MeteoraVelocity;

}



[System.Serializable]
public struct RobotData
{
    [Header("PlayerShips")]
    public bool IsRobot;

    [Header("PlayerGravity")]
    public float GravityScale;
    public float JumpHeight;
    public float MaxJumpHight;
    public SpriteRenderer SpriteRenderer;
    public float TimerHoldJump;


}


