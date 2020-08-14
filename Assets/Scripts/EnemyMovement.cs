using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private AggroDetection _aggroDetection;
    private Transform target;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _aggroDetection = GetComponent<AggroDetection>();
        _aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.position);

            float currentSpeed = _navMeshAgent.velocity.magnitude;
            _animator.SetFloat("Speed", currentSpeed);
        }

        
    }
}
