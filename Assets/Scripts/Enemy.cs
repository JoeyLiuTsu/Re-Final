﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<GameObject> players;
    private PlayerHealth playHealth;

    public GameObject explosion;
    public GameObject target;
    private bool isLocked;
    public static Vector3 toTarget;
    public GameObject bullet;
    public float health;

    private void Awake()
    {
        players = new List<GameObject>();
        health = 100;
    }
    private void Update()
    {
        if (players.Count != 0)
        {
            target = players[0];
            foreach (GameObject unit in players.ToArray())
            {

                if (unit == null)
                {
                    players.Remove(unit);
                    StopAllCoroutines();
                }
            }
        }
        else
        {
            target = null;
        }

        if (target)
        {

            toTarget = target.transform.position - transform.position;
            if (!isLocked)
            {
                isLocked = true;
                StartCoroutine(Fire());
            }
            playHealth = target.GetComponent<PlayerHealth>();
            if (playHealth.health <= 0)
            {
                target = null;
            }
            //Health

        }
        else
        {

            isLocked = false;
            StopAllCoroutines();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject UPlayer = other.gameObject;
            if (UPlayer != null)
            {
                if (!players.Contains(UPlayer))
                {

                    players.Add(UPlayer);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject UPlayer = other.gameObject;
            if (UPlayer != null)
            {
                if (players.Contains(UPlayer))
                {

                    players.Remove(UPlayer);
                }
            }
        }

    }
    public void Damage()
    {
        health -= 25;
    }
    public void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 up = transform.position;
        up.y += 1;
        Instantiate(bullet, up, Quaternion.identity);
        StartCoroutine(Fire());
    }
}
