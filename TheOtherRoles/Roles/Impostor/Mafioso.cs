using UnityEngine;

namespace TheOtherRoles.Roles.Impostor
{
    internal static class Mafioso
    {
        public static PlayerControl mafioso;
        public static Color color = Palette.ImpostorRed;

        public static void clearAndReload()
        {
            mafioso = null;
        }
    }
}
