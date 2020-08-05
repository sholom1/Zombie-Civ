using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTemplateTree : MonoBehaviour
{
    public List<CommandRequest> commandRequests;
    public CommandRequest Parent;

    
}
[System.Serializable]
public class CommandRequest
{
    public CommandType CommandTypeEnum;
    public string CommandTypeInStringFormat;
}
public enum CommandType
{
    None,
    Custom,
    CompareTwoTargetsCommand,
    FindClosestPickupCommand,
    FindClosestResourceCommand,
    MoveToTargetCommand,
    IsInventoryFullCommand
}