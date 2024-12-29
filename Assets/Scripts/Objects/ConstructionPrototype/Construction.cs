using UnityEngine;

public class Construction : BasicUnit, IConstruction
{
    public string ConstructionName;
    public ConstructionType ConstructionType;
    public int ConstructionHealth;
    public bool CanBeCaptured;
    public int CaptureProgress = 0;
    public int BuildDifficulty;
    public int BuildProgress = 0;

    public override void LoadSpriteAndAnimation()
    {
        if (this.ConstructionType == ConstructionType.Empty)
            return;
        LoadSprite(EssentialDatumLoader.SpriteDictionary[ConstructionType]);
        this.UnitGameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(ResourcePaths.TargetAnimators[ConstructionType])
            as RuntimeAnimatorController;
    }

    public virtual void ChessStepOff(ref Chess step_chess) { }

    public virtual void ChessStepOn(ref Chess step_chess) { }

    public virtual void LandscapeDestroyedOn(ref Landscape plant_landscape) { }

    public virtual void LandscapePlantOn(ref Landscape plant_landscape) { }
}
