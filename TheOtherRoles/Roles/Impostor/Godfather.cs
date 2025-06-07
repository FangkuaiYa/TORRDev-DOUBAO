using UnityEngine;

namespace TheOtherRoles.Roles.Impostor
{
    internal static class Godfather
    {
        public static PlayerControl godfather;
        public static Color color = Palette.ImpostorRed;

        public static void clearAndReload()
        {
            godfather = null;
        }
    }
}
