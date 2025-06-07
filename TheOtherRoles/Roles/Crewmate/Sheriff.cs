using System.Linq;
using TheOtherRoles.Patches;
using UnityEngine;

namespace TheOtherRoles.Roles.Crewmate
{
    internal class Sheriff : RoleBase<Sheriff>
    {
        public static Color color = new Color32(248, 205, 70, byte.MaxValue);

        public static float cooldown = 30f;
        public static bool canKillNeutrals = false;
        public static bool spyCanDieToSheriff = false;
        public bool isFormerDeputy = false;
        public float remainingHandcuffs = 0;

        public Sheriff()
        {
            RoleId = roleId = RoleId.Sheriff;
            currentTarget = null;
            isFormerDeputy = false;
            remainingHandcuffs = 0;
        }

        public PlayerControl currentTarget;

        public override void FixedUpdate()
        {
            if (player != PlayerControl.LocalPlayer) return;
            currentTarget = PlayerControlFixedUpdatePatch.setTarget();
            PlayerControlFixedUpdatePatch.setPlayerOutline(currentTarget, color);
        }

        public static void replaceCurrentSheriff(PlayerControl deputy)
        {
            setRole(deputy);
            getRole(deputy).currentTarget = null;
            cooldown = CustomOptionHolder.sheriffCooldown.getFloat();
        }

        public override void OnKill(PlayerControl target)
        {
        }

        public static Deputy getDeputy(PlayerControl sheriff)
        {
            return Deputy.players.FirstOrDefault(x => x.sheriff != null && x.sheriff?.player == sheriff);
        }

        public static void clearAndReload()
        {
            cooldown = CustomOptionHolder.sheriffCooldown.getFloat();
            canKillNeutrals = CustomOptionHolder.sheriffCanKillNeutrals.getBool();
            spyCanDieToSheriff = CustomOptionHolder.spyCanDieToSheriff.getBool();
            players = new();
        }
    }
}
