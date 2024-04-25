using UnityEngine;

[System.Serializable]
public abstract class Landscape : ILandscape
{
    public string LandscapeName;
    public LandscapeType LandscapeType;
    public GameObject LandscapeObject;

    public Landscape InitializeLandscapeGameObject(GameObject landscapeobject)
    {
        this.LandscapeObject = landscapeobject;
        return this;
    }

    public abstract void EffectChess(ref Chess stepchess);

    public virtual void EffectConstruction(ref Construction plantconstruction) { }
}
