using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorCode
{
    Rock,
    TreeAndFence,
    Grass,
    Grave,
    WallAndFurniture,
    Door,
    Item,
    Fire,
}

public class GameManager : MonoBehaviour
{
    public int BlockSize = 1;
    public LayerMask BlockMovementLayer;
    public LayerMask InteractTrigger;
    public float MoveRoundDuration = 0.25f;
    public float NextControlTime = 0;
    public bool ReadyToControl => NextControlTime <= Time.time;

    public bool movementInputLock;

    public GameObject rockTilemap;
    public GameObject treeTilemap;
    public GameObject fenceTilemap;
    public GameObject grassTilemap;
    public GameObject graveTilemap;
    public GameObject wallTilemap;
    public GameObject furnitureTilemap;
    public GameObject doorTilemap;
    public GameObject itemTilemap;
    public GameObject fireTilemap;

    private static GameManager _instance;
    public static GameManager Instance{
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        NextControlTime = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetNextControlTime()
    {
        NextControlTime = Time.time + MoveRoundDuration;
    }

    public void HideColor(ColorCode colorCode)
    {
        switch (colorCode)
        {
            case ColorCode.Rock:
                rockTilemap.SetActive(false);
                break;
            case ColorCode.TreeAndFence:
                treeTilemap.SetActive(false);
                fenceTilemap.SetActive(false);
                break;
            case ColorCode.Grass:
                grassTilemap.SetActive(false);
                break;
            case ColorCode.Grave:
                graveTilemap.SetActive(false);
                break;
            case ColorCode.WallAndFurniture:
                wallTilemap.SetActive(false);
                furnitureTilemap.SetActive(false);
                break;
            case ColorCode.Door:
                doorTilemap.SetActive(false);
                break;
            case ColorCode.Item:
                itemTilemap.SetActive(false);
                break;
            case ColorCode.Fire:
                fireTilemap.SetActive(false);
                break;
        }
    }
}
