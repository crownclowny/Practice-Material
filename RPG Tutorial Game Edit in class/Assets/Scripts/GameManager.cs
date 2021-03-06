﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        clickTarget();
	}
    private void clickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,512);

            if (hit.collider != null)
            {
                if(hit.collider.tag == "Enemy")
                player.myTarget = hit.transform;
            }
            else
            {
                player.myTarget = null;
            }
        }

    }
}
