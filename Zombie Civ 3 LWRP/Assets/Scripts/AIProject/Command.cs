using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;

public abstract class Command
{
    public AiController controller;
    public Command Parent;
    public Command Left;
    public Command Right;

    public Command (AiController aiController, Command parent)
    {
        if (parent != null)
            Parent = parent;
        controller = aiController;
    }
    public abstract bool Call();
    public virtual bool Next()
    {
        if (Left != null)
        {
            if (Left.Call())
            {
                if (Right != null)
                    return Right.Call();
                else
                    return true;
            }
            else return false;
        }
        else
        {
            if (Right != null)
            {
                return Right.Call();
            }
            //branch is complete
            return true;
        }
    }
}
