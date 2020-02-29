using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory
{
    public Content Create()
    {
        Content c = CreateMethod();
        RegisterContent(c);
        return c;
    }

    protected abstract Content CreateMethod();
    protected abstract void RegisterContent(Content content);
}


public abstract class Content
{
    public Content(Factory fact)
    {

    }

    public abstract void Use();
}
