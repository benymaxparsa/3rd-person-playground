﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float _fireRate = 0.3f;

    [SerializeField]
    [Range(1, 10)]
    private int _damage = 1;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private ParticleSystem _muzzleParticle;

    [SerializeField]
    private AudioSource _gunFireSource;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                _timer = 0f;
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        //Debug.DrawRay(_firePoint.position, _firePoint.forward * 100, Color.red, 2f);

        _muzzleParticle.Play();
        _gunFireSource.Play();

        Ray ray = new Ray(_firePoint.position, _firePoint.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }
        }

    }
}
