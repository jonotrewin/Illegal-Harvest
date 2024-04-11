using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public static class Seed
{
    public static Dictionary<SeedType, int> seeds = new Dictionary<SeedType, int>();
    public enum SeedType { Aubermean, Zuccmeanie, Slaughtermelon, NotSoSweetPotatoe}

    
    
   public static void ChangeSeedCount(SeedType seedType, int amount)
    {
        seeds[seedType] += amount;

        
    }

    public static bool AreSeedsAvailable(SeedType seedType)
    {
        seeds.TryGetValue(seedType, out int amount);
        if(amount <= 0) return false;
        else return true;
    }






}
