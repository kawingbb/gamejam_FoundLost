using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int BlockSize = 1;
    public LayerMask BlockMovementLayer;
    public float MoveRoundDuration = 0.25f;
    public float NextControlTime = 0;
    public bool ReadyToControl => NextControlTime <= Time.time;

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
}
