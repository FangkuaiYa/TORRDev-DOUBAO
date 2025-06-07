using System.Collections.Generic;

namespace TheOtherRoles.Roles.Modifier
{
    internal static class Vip
    {
        public static List<PlayerControl> vip = new List<PlayerControl>();
        public static bool showColor = true;

        public static void clearAndReload()
        {
            vip = new List<PlayerControl>();
            showColor = CustomOptionHolder.modifierVipShowColor.getBool();
        }
    }
}