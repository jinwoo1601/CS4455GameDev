using System.Collections;
using System.Collections.Generic;

public class Buff 
{

    private static int luckBuffPrice = 100;
    private static int speedBuffPrice = 100;
    private static int attackBuffPrice = 100;
    private static int reviveBuffPrice = 100;

    private static string luckBuffDesc = "";
    private static string speedBuffDesc = "";
    private static string attackBuffDesc = "";
    private static string reviveBuffDesc = "";

    public enum BuffType
    {
        speed = 1,
        attack = 2,
        luck = 3,
        revive = 4
    }

    public static int GetBuffPrice(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.luck:
                return luckBuffPrice;
            case BuffType.speed:
                return speedBuffPrice;
            case BuffType.attack:
                return attackBuffPrice;
            case BuffType.revive:
                return reviveBuffPrice;
        }
        return -1;
    }

    public static string GetBuffDescription(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.luck:
                return luckBuffDesc;
            case BuffType.speed:
                return speedBuffDesc;
            case BuffType.attack:
                return attackBuffDesc;
            case BuffType.revive:
                return reviveBuffDesc;
        }
        return "Buff not implemented";
    }
}
