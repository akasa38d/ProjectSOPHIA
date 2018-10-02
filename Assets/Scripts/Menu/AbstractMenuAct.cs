using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMenuAct
{
    public string Name;

    List<SubMenu> subMenuList;
    public abstract class SubMenu
    {
        public string Name;
        public void Action() { }
    }

    public virtual void Update() { }
}

public class MenuBaseAct : AbstractMenuAct
{
    public MenuBaseAct(string name)
    {
        Name = name;
    }
}

public class StatusMenuAct : AbstractMenuAct
{
    public StatusMenuAct(string name)
    {
        Name = name;
    }
}

public class ItemMenuAct : AbstractMenuAct
{
    public ItemMenuAct(string name)
    {
        Name = name;
    }
}

public class ConfigMenuAct : AbstractMenuAct
{
    public ConfigMenuAct(string name)
    {
        Name = name;
    }
}

public class SaveLoadMenuAct : AbstractMenuAct
{
    public SaveLoadMenuAct(string name)
    {
        Name = name;
    }
}
