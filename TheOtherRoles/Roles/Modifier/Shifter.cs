using System;
using System.Collections.Generic;
using TheOtherRoles.Roles.Crewmate;
using TheOtherRoles.Roles.Neutral;
using UnityEngine;

namespace TheOtherRoles.Roles.Modifier
{
    internal static class Shifter
    {
        public static PlayerControl shifter;
        public static PlayerControl futureShift;
        public static PlayerControl currentTarget;
        public static bool shiftsMedicShield = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            return buttonSprite ??= Helpers.loadSpriteFromResources(
                "TheOtherRoles.Resources.ShiftButton.png", 115f);
        }

        private delegate void RoleTransferHandler(PlayerControl player1, PlayerControl player2);
        private static readonly Dictionary<Type, RoleTransferHandler> RoleHandlers = new()
        {
            { typeof(Mayor), (p1, p2) => Mayor.mayor = p1 },
            { typeof(Portalmaker), (p1, p2) => Portalmaker.portalmaker = p1 },
            { typeof(Engineer), (p1, p2) => Engineer.engineer = p1 },
            { typeof(Sheriff), (p1, p2) => p1.isRole(RoleId.Sheriff) },
            { typeof(Deputy), (p1, p2) => p1.isRole(RoleId.Deputy) },
            { typeof(Lighter), (p1, p2) => Lighter.lighter = p1 },
            { typeof(Detective), (p1, p2) => Detective.detective = p1 },
            { typeof(TimeMaster), (p1, p2) => TimeMaster.timeMaster = p1 },
            { typeof(Medic), (p1, p2) => {
                Medic.medic = p1;
                if (Medic.shielded == p1 && shiftsMedicShield)
                    Medic.shielded = p2;
            }},
            { typeof(Swapper), (p1, p2) => Swapper.swapper = p1 },
            { typeof(Seer), (p1, p2) => Seer.seer = p1 },
            { typeof(Hacker), (p1, p2) => Hacker.hacker = p1 },
            { typeof(Tracker), (p1, p2) => Tracker.tracker = p1 },
            { typeof(Snitch), (p1, p2) => Snitch.snitch = p1 },
            { typeof(Spy), (p1, p2) => Spy.spy = p1 },
            { typeof(SecurityGuard), (p1, p2) => SecurityGuard.securityGuard = p1 },
            { typeof(Guesser), (p1, p2) => Guesser.niceGuesser = p1 },
            { typeof(Medium), (p1, p2) => Medium.medium = p1 },
            { typeof(Pursuer), (p1, p2) => Pursuer.pursuer = p1 },
            { typeof(Trapper), (p1, p2) => Trapper.trapper = p1 }
        };

        public static void shiftRole(PlayerControl player1, PlayerControl player2, bool repeat = true)
        {
            foreach (var handler in RoleHandlers)
            {
                var roleProperty = handler.Key.GetField(handler.Key.Name.ToLowerInvariant());
                if (roleProperty?.GetValue(null) is PlayerControl rolePlayer && rolePlayer == player2)
                {
                    if (repeat) shiftRole(player2, player1, false);

                    handler.Value(player1, player2);
                    return;
                }
            }
        }

        public static void clearAndReload()
        {
            shifter = null;
            currentTarget = null;
            futureShift = null;
            shiftsMedicShield = CustomOptionHolder.modifierShifterShiftsMedicShield.getBool();
        }
    }
}