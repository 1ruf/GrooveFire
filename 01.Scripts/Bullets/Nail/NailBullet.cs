using DG.Tweening;
using GondrLib.ObjectPool.Runtime;
using LCM._01.Scripts;
using System.Collections;
using UnityEngine;

namespace KHG.Bullets
{
    public class NailBullet : Bullet
    {
        [SerializeField] private GameObject warnningLine;
        [SerializeField] private Transform nail;
        public float MoveSpeed = 10;
    public float WaitTime = 0.5f;

        private Vector3 _spawnPosition;

        private SpriteRenderer _warnRenderer;
        private Pool _currentPool;
        private bool _moveable;

        protected override void OnEnable()
        {
            base.OnEnable();
            _warnRenderer = warnningLine.GetComponent<SpriteRenderer>();
            StartCoroutine(Warnning());
        }
        public override void ResetItem()
        {
            _moveable = false;
            _warnRenderer.color = new Color(_warnRenderer.color.r, _warnRenderer.color.g, _warnRenderer.color.b, 0);
        }
        public void SetSpawnValues(Vector3 pos, Vector3 rotation,float moveSpeed = 10f,float waitTime = .5f)
        {
            _spawnPosition = pos;
            transform.rotation = Quaternion.Euler(rotation);
            MoveSpeed = moveSpeed;
            WaitTime = waitTime;
        }
        public override void SetUpPool(Pool pool) => _currentPool = pool;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            ApplyDamage(other);
        }
        private void FixedUpdate()
        {
            SetMovement();
        }

        private void SetMovement()
        {
            if (_moveable) nail.transform.position += nail.transform.up * MoveSpeed * Time.fixedDeltaTime;
        }

        private IEnumerator Warnning()
        {
            warnningLine.SetActive(true);
            _warnRenderer.DOFade(1, WaitTime);
            yield return new WaitForSeconds(WaitTime);
            _moveable = true;
            yield return new WaitForSeconds(WaitTime);
            _warnRenderer.DOFade(0, WaitTime);
            yield return new WaitForSeconds(10);

            if(_currentPool != null) _currentPool.Push(this);
            else Destroy(gameObject);
        }
    }
}