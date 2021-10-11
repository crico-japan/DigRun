//using DG.Tweening;
//using IceMilkTea.Core;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Animal : Character, IObserver
//{
//    [SerializeField]
//    private float walkSpeed = 2.0f;

//    [SerializeField]
//    private float runSpeed = 3.0f;

//    [SerializeField]
//    private float dist = 2.0f;

//    [SerializeField]
//    Transform[] rayStarts = null;

//    bool isStart = false;

//    private enum Direction
//    {
//        Right = 0,
//        Left = 1,
//    }

//    [SerializeField]
//    Direction direction = Direction.Left;

//    private ImtStateMachine<Animal> stateMachine;
//    private int layerMask;

//    [SerializeField]
//    List<Character> characters;
//    private enum StateEventID
//    {
//        Idle,
//        Patrol,
//        Hit,
//        Attack,
//        Dead,
//        Bark,
//    }

//    //待機状態
//    private class IdleState : ImtStateMachine<Animal>.State
//    {
//        protected internal override void Enter()
//        {
            
//        }

//        protected internal override void Update()
//        {
//            if (Context.isStart == false)
//            {
//                return;
//            }

//            Vector3 dir = new Vector3();
//            switch (Context.direction)
//            {
//                case Direction.Left:
//                    dir = Vector3.left;
//                    break;

//                case Direction.Right:
//                    dir = Vector3.right;
//                    break;
//            }

//            foreach (var obj in Context.rayStarts)
//            {
//                RaycastHit2D hit = Physics2D.Raycast(obj.position, dir, Context.dist, Context.layerMask);
//                if(hit.collider != null)
//                {
//                    //if (hit.transform.gameObject.tag != TagName.Player && hit.transform.gameObject.tag != TagName.Enemy)
//                    //{
//                    //    return;
//                    //}
//                }

//                RaycastHit hit3d;
//                Debug.DrawRay(obj.position, dir, Color.white, Context.dist);
//                if (Physics.Raycast(obj.position, dir, out hit3d, Context.dist))
//                {
//                    if (hit3d.transform.gameObject.tag != TagName.Player && hit3d.transform.gameObject.tag != TagName.Enemy)
//                    {
//                        return;
//                    }
//                }
//            }

//            stateMachine.SendEvent((int)StateEventID.Hit);
//        }

//        protected internal override void Exit()
//        {
            
//        }
//    }

//    //見回り状態
//    private class PatrolState : ImtStateMachine<Animal>.State
//    {
//        protected internal override void Enter()
//        {
//            Context.animator.SetBool("isPatrol", true);
//        }

//        protected internal override void Update()
//        {
//            Vector3 dir = new Vector3();
//            switch (Context.direction)
//            {
//                case Direction.Left:
//                    dir = Vector3.left;
//                    break;

//                case Direction.Right:
//                    dir = Vector3.right;
//                    break;
//            }

//            Context.transform.position += dir * (Context.walkSpeed * Time.deltaTime);

//            if(Context.isStart == false)
//            {
//                return;
//            }

//            foreach (var obj in Context.rayStarts)
//            {
//                RaycastHit hit;

//                if (Physics.Raycast(obj.position, dir, out hit, Context.dist))
//                {
//                    if (hit.transform.gameObject.tag != TagName.Player && hit.transform.gameObject.tag != TagName.Enemy)
//                    {
//                        return;
//                    }
//                }
//            }

//            stateMachine.SendEvent((int)StateEventID.Hit);
//        }

//        protected internal override void Exit()
//        {
//            Context.animator.SetBool("isPatrol", false);
//        }
//    }

//    private class RunState : ImtStateMachine<Animal>.State
//    {
//        protected internal override void Enter()
//        {
//            Context.animator.SetBool("isRun", true);
//        }

//        protected internal override void Update()
//        {
//            Vector3 dir = new Vector3();
//            switch (Context.direction)
//            {
//                case Direction.Left:
//                    dir = Vector3.left;
//                    break;

//                case Direction.Right:
//                    dir = Vector3.right;
//                    break;
//            }

//            Context.transform.position += dir * (Context.runSpeed * Time.deltaTime);
//        }

//        protected internal override void Exit()
//        {
//            Context.animator.SetBool("isRun", false);
//        }
//    }

//    private class AttackState : ImtStateMachine<Animal>.State
//    {
//        protected internal override void Enter()
//        {
//            Context.animator.SetBool("isAttack", true);
//        }

//        protected internal override void Exit()
//        {
//            Context.animator.SetBool("isAttack", false);
//        }
//    }

//    private class DeadState : ImtStateMachine<Animal>.State
//    {
//        protected internal override void Enter()
//        {
//            //Destroy(Context.gameObject);
//            Context.animator.SetBool("isDead", true);
//        }
//    }

//    private class BarkState : ImtStateMachine<Animal>.State
//    {
//        protected internal override void Enter()
//        {
//            Context.animator.SetBool("isBark", true);
//        }
//    }

//    protected override void Awake()
//    {
//        //base.Awake();
//        //base.Init();

//        //ステートマシン設定
//        stateMachine = new ImtStateMachine<Animal>(this);
//        stateMachine.AddTransition<IdleState, PatrolState>((int)StateEventID.Patrol);
//        stateMachine.AddTransition<IdleState, RunState>((int)StateEventID.Hit);
//        //stateMachine.AddTransition<PatrolState, RunState>((int)StateEventID.Hit);
//        stateMachine.AddTransition<RunState, AttackState>((int)StateEventID.Attack);
//        stateMachine.AddTransition<AttackState, IdleState>((int)StateEventID.Idle);
//        stateMachine.AddAnyTransition<DeadState>((int)StateEventID.Dead);
//        stateMachine.AddAnyTransition<BarkState>((int)StateEventID.Bark);

//        stateMachine.SetStartState<IdleState>();

//        animator = GetComponent<Animator>();


//        rigidbody = GetComponent<Rigidbody2D>();

//        gameManager.AddObserver(this);
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        layerMask = 1 << LayerName.Player | 1 << LayerName.Enemy | 1 << LayerName.Ground | 1 << LayerName.StageObject;
//        stateMachine.Update();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(characters.Count <= 0 && stateMachine.CurrentStateName != "BarkState")
//        {
//            stateMachine.SendEvent((int)StateEventID.Bark);
//        }
//        stateMachine.Update();
//    }

//    public override void OnNotify(Subject subject)
//    {
//        isStart = true;
//    }

//    private Character character;
//    private void OnCollisionEnter2D(Collision2D collision)
//    {

//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        switch (collision.gameObject.tag)
//        {
//            case TagName.Player:
//            case TagName.Enemy:
//                stateMachine.SendEvent((int)StateEventID.Attack);
//                collision.gameObject.GetComponent<HitBox>().Collider.enabled = false;
//                character = collision.gameObject.GetComponent<HitBox>().Parent;
//                break;
//        }
//    }

//    public void AttackFinish()
//    {
//        stateMachine.SendEvent((int)StateEventID.Idle);
//    }

//    public void Attack()
//    {
//        character.SendDeadNotify();
//        for (int i = 0; i < characters.Count; i++)
//        {
//            if (character == characters[i])
//            {
//                characters.RemoveAt(i);
//            }
//        }
//    }

//    public override void SendDeadNotify()
//    {
//        stateMachine.SendEvent((int)StateEventID.Dead);
//    }

//    public override void SendBombDeathNotify(Transform transform)
//    {
//        stateMachine.SendEvent((int)StateEventID.Dead);
//    }

//    private void DeadAnimation()
//    {
//        transform.DOScaleY(0, 0.5f).OnComplete(() =>
//        {
//            Destroy(gameObject);
//        });
//    }

//    public override void SendThornWallDeadNotify()
//    {
//        throw new System.NotImplementedException();
//    }
//}
