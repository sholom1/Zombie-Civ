using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInventoryFullCommand : OrCommand
{
    public IsInventoryFullCommand(AiController aiController, Command parent) : base(aiController, parent)
    {
    }

    public override bool Call()
    {
        if (controller.Inventory.IsFull())
        {
            return Next();
        }
        return false;
    }
}
