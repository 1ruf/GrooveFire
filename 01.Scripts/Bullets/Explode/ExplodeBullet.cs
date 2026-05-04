using DG.Tweening;
using LCM._01.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KHG.Bullets
{
    public class ExplodeBullet : Bullet
    {
        [SerializeField] public bool Moveable;
        [SerializeField] public Vector3 TargetPosition;
        public UnityEvent ActiveEvent;
        public Vector3 SpawnPosition 
        { 
            get => transform.position;
            set => transform.position = value; 
        }

        private bool _damageable = false;
        private Pool _explodePool;

        protected override void OnEnable()
        {
            base.OnEnable();
            StartCoroutine(Move(0.1f));
        }
        private void Start()
        {
            
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if(_damageable == true) base.OnTriggerEnter2D(other);
        }

        public void DamageStart() => _damageable = true;
        public void DamageEnd() => _damageable = false;
        public void OnActivated() => ActiveEvent?.Invoke();
        public void DestroySelf()
        {
            _damageable = false;
            if (_explodePool != null) _explodePool.Push(this);
            else Destroy(gameObject);
        }
        private IEnumerator Move(float t)
        {
            yield return new WaitForSeconds(t);
            if (Moveable) transform.DOMove(TargetPosition, 1.5f);
        }
        public override void SetUpPool(Pool pool)
        {
            _explodePool = pool;
        }

        public override void ResetItem()
        {
            transform.DOKill();
            _damageable = false;
            Moveable = false;
            SpawnPosition = Vector3.zero;
            TargetPosition = Vector3.zero;
        }
    }
}
