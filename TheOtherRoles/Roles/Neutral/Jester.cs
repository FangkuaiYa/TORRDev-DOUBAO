using UnityEngine;

namespace TheOtherRoles.Roles.Neutral
{
    internal static class Jester
    {
        public static PlayerControl jester;
        public static Color color = new Color32(236, 98, 165, byte.MaxValue);

        public static bool triggerJesterWin = false;
        public static bool canCallEmergency = true;
        public static bool hasImpostorVision = false;

        public static void clearAndReload()
        {
            jester = null;
            triggerJesterWin = false;
            canCallEmergency = CustomOptionHolder.jesterCanCallEmergency.getBool();
            hasImpostorVision = CustomOptionHolder.jesterHasImpostorVision.getBool();
        }
    }
}
