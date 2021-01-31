using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ColorCode
{
    Rock,
    Tree,
    Fence,
    Grass,
    Grave,
    Wall,
    Furniture,
    Door,
    Item,
    Fire,
}

public enum NPC
{
    Mayer,
    WoodCutter,
    Hunter,
    Farmer,
    Bard,
    BlackSmith,
    Priest,
    Merchant,
}

public class GameManager : MonoBehaviour
{
    public int BlockSize = 1;
    public LayerMask BlockMovementLayer;
    public LayerMask InteractTrigger;
    public LayerMask PlayerLayer;
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

    public Transform mayer;
    public Transform woodCutter;
    public Transform hunter;
    public Transform farmer;
    public Transform bard;
    public Transform blackSmith;
    public Transform priest;
    public Transform merchant;
    public Transform cook;
    public int clueFoundCount;

    private bool _loseGame;
    public GameObject jumpScareSprite;

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
        _loseGame = false;
        jumpScareSprite.SetActive(false);
        NextControlTime = Time.time;
        KillNPC(NPC.Mayer);
        clueFoundCount = 1;
        EnemyController.Instance.disable = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseGame()
    {
        if (_loseGame) return;
        _loseGame = true;
        Debug.Log("Lose game");
        jumpScareSprite.SetActive(true);
        movementInputLock = true;
        AudioManager.Instance.stopAllClip();
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
            case ColorCode.Tree:
                treeTilemap.SetActive(false);
                break;
            case ColorCode.Fence:
                fenceTilemap.SetActive(false);
                break;
            case ColorCode.Grass:
                grassTilemap.SetActive(false);
                break;
            case ColorCode.Grave:
                graveTilemap.SetActive(false);
                break;
            case ColorCode.Wall:
                wallTilemap.SetActive(false);
                break;
            case ColorCode.Furniture:
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
        if (clueFoundCount < 8)
            KillNPC((NPC)clueFoundCount);
        clueFoundCount++;
        if (clueFoundCount >= 4)
        {
            EnemyController.Instance.disable = false;
            EnemyController.Instance.isChasingPlayer = true;
            cook.gameObject.SetActive(false);
        }
    }

    public void KillNPC(NPC npc)
    {
        switch (npc)
        {
            case NPC.Mayer:
                mayer.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.mayerChatTrigger.enableInteract = false;
                TriggerManager.Instance.MayorDeadClueTrigger.enableInteract = true;
                TriggerManager.Instance.FireOffTrigger.enableInteract = true;
                break;
            case NPC.WoodCutter:
                woodCutter.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.woodCutterChatTrigger.enableInteract = false;
                TriggerManager.Instance.WoodCutterDeadClueTrigger.enableInteract = true;
                TriggerManager.Instance.TreeOffTrigger.enableInteract = true;
                break;
            case NPC.Hunter:
                hunter.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.hunterChatTrigger.enableInteract = false;
                break;
            case NPC.Farmer:
                farmer.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.farmerChatTrigger.enableInteract = false;
                TriggerManager.Instance.FarmerDeadClueTrigger.enableInteract = true;
                TriggerManager.Instance.GrassOffTrigger.enableInteract = true;
                break;
            case NPC.Bard:
                bard.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.bardChatTrigger.enableInteract = false;
                break;
            case NPC.BlackSmith:
                blackSmith.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.blackSmithChatTrigger.enableInteract = false;
                break;
            case NPC.Priest:
                priest.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.priestChatTrigger.enableInteract = false;
                TriggerManager.Instance.PriestDeadClueTrigger.enableInteract = true;
                TriggerManager.Instance.GraveOffTrigger.enableInteract = true;
                break;
            case NPC.Merchant:
                merchant.rotation = Quaternion.Euler(0, 0, 270);
                TriggerManager.Instance.merchantChatTrigger.enableInteract = false;
                TriggerManager.Instance.MerchantBeforeDeadClueTrigger.enableInteract = true;
                TriggerManager.Instance.FurnitureOffTrigger.enableInteract = true;
                break;
        }
    }
}
