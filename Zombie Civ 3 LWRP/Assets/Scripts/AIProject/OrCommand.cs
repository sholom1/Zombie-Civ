using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrCommand : Command
{
    public OrCommand(AiController aiController, Command parent) : base(aiController, parent)
    {
    }

    public override bool Next()
    {
        if (Right != null)
        {
            if (Right.Call())
            {
                if (Left != null)
                    return Left.Call();
                else
                    return true;
            }
            else return false;
        }
        else
        {
            if (Left != null)
            {
                return Left.Call();
            }
            //branch is complete
            return true;
        }
    }
}
