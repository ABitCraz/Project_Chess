using UnityEngine;

public class SpriteColorFunctions
{
    //In my opinion,These functions base on struct,so disassemble them into Multi-Processors to calculate them is a better choice.
    //All these functions,Better,I mean,for pros.You better use them in Event Triggers instead of Coroutines.
    //If it's hard to use them in Event Triggers,I suggest use them in Async Processes.
    public bool ColorFadingInTargetColor(GameObject spritegameobject, Color32 targetcolor)
    {
        Color32 spritecolor = spritegameobject.GetComponent<SpriteRenderer>().color;
        if (spritecolor.Equals(targetcolor))
        {
            return true;
        }

        if (spritecolor.r > targetcolor.r)
        {
            spritecolor.r--;
        }
        else if (spritecolor.r < targetcolor.r)
        {
            spritecolor.r++;
        }

        if (spritecolor.g > targetcolor.g)
        {
            spritecolor.g--;
        }
        else if (spritecolor.g < targetcolor.g)
        {
            spritecolor.g++;
        }

        if (spritecolor.b > targetcolor.b)
        {
            spritecolor.b--;
        }
        else if (spritecolor.b < targetcolor.b)
        {
            spritecolor.b++;
        }

        if (spritecolor.a > targetcolor.a)
        {
            spritecolor.a--;
        }
        else if (spritecolor.a < targetcolor.a)
        {
            spritecolor.a++;
        }
        spritegameobject.GetComponent<SpriteRenderer>().color = spritecolor;
        return false;
    }

    public bool ColorFadingInTargetColor(
        GameObject spritegameobject,
        byte targetrgba,
        int colorfactor
    )
    {
        Color32 spritecolor = spritegameobject.GetComponent<SpriteRenderer>().color;
        switch (colorfactor)
        {
            case 0:
                if (spritecolor.r > targetrgba)
                {
                    spritecolor.r--;
                }
                else if (spritecolor.r < targetrgba)
                {
                    spritecolor.r++;
                }
                else
                {
                    return true;
                }
                break;
            case 1:
                if (spritecolor.g > targetrgba)
                {
                    spritecolor.g--;
                }
                else if (spritecolor.g < targetrgba)
                {
                    spritecolor.g++;
                }
                else
                {
                    return true;
                }
                break;
            case 2:
                if (spritecolor.b > targetrgba)
                {
                    spritecolor.b--;
                }
                else if (spritecolor.b < targetrgba)
                {
                    spritecolor.b++;
                }
                else
                {
                    return true;
                }
                break;
            default:
                if (spritecolor.a > targetrgba)
                {
                    spritecolor.a--;
                }
                else if (spritecolor.a < targetrgba)
                {
                    spritecolor.a++;
                }
                else
                {
                    return true;
                }
                break;
        }
        spritegameobject.GetComponent<SpriteRenderer>().color = spritecolor;
        return false;
    }

    public bool ColorFadingToNormal(GameObject spritegameobject, byte speed)
    {
        Color32 spritecolor = spritegameobject.GetComponent<SpriteRenderer>().color;
        if (spritecolor.Equals(new Color32(255, 255, 255, 255)))
        {
            return true;
        }
        spritegameobject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        return false;
    }

    public bool ColorFadingOut(GameObject spritegameobject, byte speed)
    {
        Color32 spritecolor = spritegameobject.GetComponent<SpriteRenderer>().color;
        if (spritecolor.a < 5)
        {
            spritecolor.a = 0;
            return true;
        }

        if (spritecolor.a > 255)
        {
            spritecolor.a -= speed;
        }
        spritegameobject.GetComponent<SpriteRenderer>().color = spritecolor;
        return false;
    }

    public bool ColorFadingOutAndDisactive(GameObject spritegameobject, byte speed)
    {
        Color32 spritecolor = spritegameobject.GetComponent<SpriteRenderer>().color;
        if (spritecolor.a < 5)
        {
            spritegameobject.SetActive(false);
            spritegameobject.GetComponent<SpriteRenderer>().color = Color.white;
            return true;
        }
        if (spritecolor.a > 255)
        {
            spritecolor.a -= speed;
        }
        spritegameobject.GetComponent<SpriteRenderer>().color = spritecolor;
        return false;
    }
}
