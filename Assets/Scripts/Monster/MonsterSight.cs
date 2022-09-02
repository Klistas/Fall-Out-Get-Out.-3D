using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    
    public bool DebugMode;
    public bool _isTargetingPlayer;
    public Transform _target;
    public LayerMask ObstacleMask;
    public LayerMask TargetLayer;

    private float _sightAngle = 160f;
    private float _sightDistance = 10f;
    private void Start()
    {
        StartCoroutine(SightToPlayer());
    }

    private void Update()
    {
    }


    private void OnDrawGizmos()
    {
        if (!DebugMode)
            return;
        Vector3 MyPosition = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(MyPosition, _sightDistance);
        float lookingAngle = transform.eulerAngles.y;
        Vector3 rightDirection = AngleToDirection(transform.eulerAngles.y + _sightAngle * 0.5f);
        Vector3 leftDirection = AngleToDirection(transform.eulerAngles.y - _sightAngle * 0.5f);
        Vector3 lookDirection = AngleToDirection(lookingAngle);

        Debug.DrawRay(MyPosition, rightDirection * _sightDistance, Color.blue);
        Debug.DrawRay(MyPosition, leftDirection * _sightDistance, Color.blue);
        Debug.DrawRay(MyPosition, lookDirection * _sightDistance, Color.red);
    }
   



    Vector3 AngleToDirection(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }



    IEnumerator SightToPlayer()
    {
        while (true)
        {
            Vector3 MyPosition = transform.position + Vector3.up * 0.5f;
            float lookingAngle = transform.eulerAngles.y;

            Vector3 lookDirection = AngleToDirection(lookingAngle);
            Collider[] Targets = Physics.OverlapSphere(MyPosition, _sightDistance, TargetLayer);

            if (Targets.Length == 0)
            {
                _isTargetingPlayer = false;

                yield return null;
            }
            foreach (Collider EnemyCollider in Targets)
            {
                Vector3 TargetPosition = EnemyCollider.transform.position;
                Vector3 TargetDirection = (TargetPosition - MyPosition).normalized;
                float TargetAngle = Mathf.Acos(Vector3.Dot(lookDirection, TargetDirection)) * Mathf.Rad2Deg;

                Ray ray = new Ray(transform.position,transform.forward);
                RaycastHit hit;

                if (TargetAngle <= _sightAngle * 0.5f)
                {

                    if (Physics.Raycast(transform.position, TargetDirection, out hit, _sightDistance))
                    {
                        if(hit.collider.CompareTag("Player") && MonsterAI._cantAttck == false)
                        {
                            _isTargetingPlayer = true;
                            _target = hit.collider.transform;
                            yield return new WaitForSeconds(5f);
                            if (_isTargetingPlayer == true)
                            {
                                Debug.DrawRay(transform.position, _target.position, Color.cyan, _sightDistance);
                            }
                        }
                        else
                        {
                            _isTargetingPlayer = false;

                        }


                    }
                    else
                    {
                        _isTargetingPlayer = false;

                    }

                }
                else
                {
                    _isTargetingPlayer = false;

                }
            }
            

            yield return null;
        }
    }






















    //private float _sightAngle = 120f;
    //private float _sightDistance = 10f;
    //private LayerMask _layerMask = 1 << 3;
    //private void Start()
    //{
    //    Target = GetComponent<Transform>();
    //}

    //void Update()
    //{
    //    SightToPlayer();

    //}

    //void SightToPlayer()
    //{
    //    Collider[] PlayerCollider = Physics.OverlapSphere(transform.position, _sightDistance, TargetLayer);
    //    if (PlayerCollider.Length > 0)
    //    {
    //        Transform Player = PlayerCollider[0].transform;

    //        Vector3 DirectionToPlayer = (Player.position - transform.position).normalized;
    //        float AngleToPlayer = Vector3.Angle(DirectionToPlayer, transform.forward);
    //        if (AngleToPlayer < _sightAngle * 0.5f)
    //        {
    //            if (Physics.Raycast(transform.position, DirectionToPlayer, out RaycastHit hit, _sightDistance))
    //            {
    //                _target.name = hit.transform.name;
    //                if (hit.collider.CompareTag("Player"))
    //                {
    //                    transform.position = Vector3.Lerp(transform.position, hit.transform.position, 0.02f);


    //                }

    //            }
    //        }
    //    }
    //}

}
