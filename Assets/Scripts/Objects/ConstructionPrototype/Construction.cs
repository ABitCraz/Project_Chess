using UnityEngine;

public class Construction : BasicUnit, IConstruction
{
    public string ConstructionName;
    public ConstructionType ConstructionType;
    public int ConstructionHealth;

    public void LoadConstructionSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[ConstructionType]);
        this.UnitGameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(ResourcePaths.TargetAnimators[ConstructionType]) as RuntimeAnimatorController;
    }

    public virtual void ChessStepOff(ref Chess stepchess){}
    public virtual void ChessStepOn(ref Chess stepchess){}
    public virtual void LandscapeDestoryedOn(ref Landscape plantlandscape){}
    public virtual void LandscapePlantOn(ref Landscape plantlandscape){}
}
