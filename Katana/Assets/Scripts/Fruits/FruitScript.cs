using System.Collections.Generic;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Fruits
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class FruitScript : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _pointsMask;
        [SerializeField] private float _destroyForce = 25;
        [SerializeField] private int _coins;
        
        [Header("Particles")]
        [SerializeField] private Transform _particles;
        [SerializeField] private List<ParticleSystem> _explosionParticles;
        private float _destroyParticlesAfter = 11;

        private void Reset()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision col)
        {
            var layer = col.gameObject.layer;
            var getPoints = (_pointsMask & 1 << layer) != 0;
            if (col.relativeVelocity.magnitude >= _destroyForce) GetDestroyed(getPoints);
        }

        public void GetDestroyed(bool getPoints)
        {
            if (getPoints) GameManager.ItemManager.Coins += _coins;
            _collider.enabled = false;
            _explosionParticles.ForEach(x => x.Play());
            _particles.SetParent(null);
            Destroy(_particles.gameObject, _destroyParticlesAfter);
            Destroy(gameObject);
        }
    }
}