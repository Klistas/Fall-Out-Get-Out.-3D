using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform[] destination;
    public Transform AttackPosition;
    public Transform TargetAttackedPosition;
    public AudioClip MonsterGrowl;
    public AudioClip MonsterHowling;
    public AudioClip MonsterAttack;
    public AudioClip MonsterRun;

    private MonsterSight _monsterSight;
    private AudioSource _audioSource;
    private Animator _animator;
    private NavMeshAgent _naviMesh;
    private int _nextGoal;
    public bool _targetingPlayer;
    public bool _isWalk;
    public bool _isRun;
    public bool _isHowling;
    public bool _isAttacked;
    public static bool _cantAttck;
    private float _elapsedTime;

    private static class AnimID
    {
        public static readonly int IS_WALK = Animator.StringToHash("IsWalk");
        public static readonly int IS_RUN = Animator.StringToHash("IsRun");
        public static readonly int IS_ATTACK = Animator.StringToHash("IsAttack");
        public static readonly int IS_HOWLING = Animator.StringToHash("IsHowling");
        public static readonly int IS_ATTACK_END = Animator.StringToHash("IsAttackEnd");
    }
    private void Start()
    {
        _naviMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _monsterSight = GetComponent<MonsterSight>();
        GameManager.Instance._isMonsterActive = true;
        _elapsedTime = 10f;
        StartCoroutine(AIMoveToGoal());
    }

    private void Update()
    {
        _animator.SetBool(AnimID.IS_RUN, _isRun);
        _elapsedTime += Time.deltaTime;
        _targetingPlayer = _monsterSight._isTargetingPlayer;
        if (_isAttacked)
        {
            _monsterSight._target.transform.position = TargetAttackedPosition.position;
            _monsterSight._target.transform.rotation = Quaternion.Euler(0f, -75f, 0f);
            _monsterSight._target.transform.LookAt(AttackPosition.position);
        }
        if (GameManager.Instance._isEnd)
        {
            GameEnd();
        }

    }


  

    IEnumerator AttackPlayer()
    {
        _animator.SetTrigger(AnimID.IS_ATTACK);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.OnDamagedPlayer();
        _audioSource.Stop();
        _audioSource.PlayOneShot(MonsterAttack);
        yield return new WaitForSeconds(1.5f);

        _audioSource.clip = MonsterGrowl;
        _audioSource.loop = true;
        _audioSource.Play();
        _isAttacked = false;
        _animator.SetTrigger(AnimID.IS_ATTACK_END);
        _targetingPlayer = false;
        _cantAttck = true;
        yield return new WaitForSeconds(10.0f);
        
        
        _isWalk = false;
        _isRun = false;
        _cantAttck = false;
        StartCoroutine(AIMoveToGoal());

        yield return null;
    }

    IEnumerator AIMoveToGoal()
    {
        while(true)
        {
            if(_isAttacked)
            {
                break;
                
            }
            
            if (_targetingPlayer == false)
            {
                _isRun = false;
                _isHowling = true;
                _isWalk = true;
                _audioSource.Stop();
                _audioSource.clip = MonsterGrowl;
                _audioSource.loop = true;
                _audioSource.Play();
                _animator.SetBool(AnimID.IS_WALK, _isWalk);
                if(_naviMesh.velocity == Vector3.zero)
                {
                    _naviMesh.SetDestination(destination[_nextGoal++].position);
                    if (_nextGoal >= destination.Length)
                    {
                         _nextGoal = 0;
                    }

                }
                
            yield return new WaitForSeconds(2.0f);

            }

            else
            {
                
                if(_isHowling)
                {
                    _naviMesh.SetDestination(transform.position);
                    _animator.SetTrigger(AnimID.IS_HOWLING);
                    _audioSource.Stop();
                    _audioSource.PlayOneShot(MonsterHowling);
                    yield return new WaitForSeconds(1.5f);
                    _isHowling = false;
                    _isRun = true;

                }
                _audioSource.Stop();
                _audioSource.clip = MonsterRun;
                _audioSource.loop = true;
                _audioSource.Play();
                _naviMesh.SetDestination(_monsterSight._target.localPosition);

                
            }
            

            yield return null;
        }
    }
 


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && _elapsedTime > 10f && _targetingPlayer && _isHowling == false)
        {
            _isWalk = false;
            _isHowling = true;
            _isAttacked = true;
             StartCoroutine(AttackPlayer());
             _elapsedTime = 0;

        }

        if (other.CompareTag("Door"))
        {
            other.GetComponent<Door>().ChangeDoorState();
        }
    }

    private void OnTriggerExit(Collider other)
    {


        if (other.CompareTag("Door"))
        {
            other.GetComponent<Door>().ChangeDoorState();
        }
    }

    void GameEnd()
    {
        _isAttacked = false;
        _isHowling = false;
        _isRun = false;
        _isWalk = false;
        Time.timeScale = 0;
        StopAllCoroutines();
    }
}

