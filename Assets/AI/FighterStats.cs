using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FighterStats : MonoBehaviour
{
    [SerializeField] GameObject _AoeSpherePrefab;
    protected static GameObject _AoeSphere;

    [SerializeField] GameObject _bulletPrefab;
    protected static GameObject _bullet;

    public enum AttackType {shooter, puller, Exploder, AoE, MeleeMover}

    [SerializeField] protected AttackType _typeOfAttacker; 


    [SerializeField]protected int _maxHealth = 100;
    protected int _currentHealth = 100;
    [SerializeField] public int CurrentHealth { get { return _currentHealth; } }


    [SerializeField] protected float _rateOfFire = 1000;
    [SerializeField] protected float _shootTimer=0;
    public float RateOfFire { get { return _rateOfFire; } }


    [SerializeField] protected bool _hasTarget;
    public bool HasTarget { get { return _hasTarget; } }
    [SerializeField] protected FighterStats _target;

    [SerializeField] protected float _range = 50f;
    public float Range { get { return _range; } }

    [SerializeField] protected int _damage = 1;

    float _distanceToTarget;

    protected void Awake()
    {
        if(_AoeSphere == null) _AoeSphere = _AoeSpherePrefab;
        if (_bullet == null) _bullet = _bulletPrefab;
    }
    protected void Start()
    {
        _currentHealth = _maxHealth;
    }
    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    protected void Update()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }

        
        GetTargetDistance();

        if (_target == null || !_target.isActiveAndEnabled)
        {
            FindTarget();
        }
        if (_target != null && _target.isActiveAndEnabled)
        {
            RunAttackTimer(_target);
        }
        if (_typeOfAttacker == AttackType.shooter) LookAtTarget();
        if (_typeOfAttacker == AttackType.MeleeMover) MoveToTarget();

    }

    private void MoveToTarget()
    {
        this.GetComponent<NavMeshAgent>().destination = _target.transform.position;
    }

    private void LookAtTarget()
    {
        transform.LookAt(_target.transform);
    }

    private void GetTargetDistance()
    {
        if (_target == null) return; 

        _distanceToTarget = Vector3.Distance(_target.transform.position, this.transform.position);
        if (_distanceToTarget > _range || _target.isActiveAndEnabled == false )
        {
            _target = null;
        }
    }

    public virtual void Die()
    {
        this.gameObject.SetActive(false);

    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;
    }

    public virtual void FindTarget()
    {

    }

    protected virtual void RunAttackTimer(FighterStats enemy)
    {
        if (_shootTimer >= _rateOfFire)
        {
            Debug.Log("Attacking: " + enemy.name);
            _shootTimer = 0;
            Attack();
            
        }
        else
        {
            _shootTimer++;
        }
    }

    private void Attack()
    {
        switch (_typeOfAttacker)
        {
            case AttackType.shooter:
                {
                    Shoot();
                    break;
                }
            case AttackType.Exploder:
                {
                    Explode();
                    break;
                }
            case AttackType.AoE:
                {
                    AoE();
                    break;
                }
            case AttackType.puller:
                {
                    Pull();
                    break;
                }
            case AttackType.MeleeMover:
                {
                    MoveToAndPunch();
                    break;

                }
            default:
                {
                    Debug.Log("No AttackerType assigned");
                    break;
                }
        }
    }

    

    private void MoveToAndPunch()
    {
        
        if(Vector3.Distance(_target.transform.position, this.transform.position) <= GetComponent<NavMeshAgent>().stoppingDistance)
        {
            
            _target.Damage(_damage);


        }

    }

    private void Pull()
    {
        _rateOfFire = 0;
        Vector3 targetStartPosition = _target.transform.position;
        _target.transform.position = Vector3.Lerp(targetStartPosition, this.transform.position, Time.deltaTime);


    }

    private void Explode()
    {
        AoE();
        Destroy(GetComponentInParent<Planter>().gameObject);
        Destroy(this.gameObject);
    }

    private void AoE()
    {
        GameObject sphere = Instantiate(_AoeSphere);
        sphere.transform.SetParent(this.transform);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _range);

        


        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent<FighterStats>(out FighterStats enemy))
            {
                if(enemy.GetType() == this.GetType())
                {
                    continue;
                }

                if (enemy.GetType() != this.GetType())
                {
                    enemy.Damage(_damage);
                    
                }
            }
            

            
        }

        


    }

    private void Shoot()
    {
        
        
        Vector3 bulletTrajectory = _target.transform.position - this.transform.position;

        Collider collider = this.GetComponent<Collider>();
        

        GameObject bullet = Instantiate(_bullet);
        bullet.transform.position = this.transform.position + Vector3.up +transform.forward*0.2f;
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(bulletTrajectory.normalized * 30, ForceMode.Impulse);
        bullet.GetComponent<BulletLogic>().SetDamage(_damage);
    }
}
