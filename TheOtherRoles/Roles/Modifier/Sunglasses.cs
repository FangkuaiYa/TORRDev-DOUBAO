﻿using System.Collections.Generic;

namespace TheOtherRoles.Roles.Modifier
{
    internal static class Sunglasses
    {
        public static List<PlayerControl> sunglasses = new List<PlayerControl>();
        public static int vision = 1;

        public static void clearAndReload()
        {
            sunglasses = new List<PlayerControl>();
            vision = CustomOptionHolder.modifierSunglassesVision.getSelection() + 1;
        }
    }
}
