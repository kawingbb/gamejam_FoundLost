using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    private static TriggerManager _instance;
    public static TriggerManager Instance{
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<TriggerManager>();
            }

            return _instance;
        }
    }
    
    public Trigger mayerChatTrigger;
    public Trigger woodCutterChatTrigger;
    public Trigger hunterChatTrigger;
    public Trigger farmerChatTrigger;
    public Trigger bardChatTrigger;
    public Trigger blackSmithChatTrigger;
    public Trigger priestChatTrigger;
    public Trigger merchantChatTrigger;
    public Trigger cookChatTrigger;
    
    public Trigger MayorDeadClueTrigger;
    public Trigger WoodCutterDeadClueTrigger;
    public Trigger FarmerDeadClueTrigger;
    public Trigger PriestDeadClueTrigger;
    public Trigger MerchantBeforeDeadClueTrigger;
    public Trigger MousePoisonClueTrigger;
    public Trigger Grave1ClueTrigger;
    public Trigger Grave2ClueTrigger;
    public Trigger SignClueTrigger;
    public Trigger AnvilClueTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
