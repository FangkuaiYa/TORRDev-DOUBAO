using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using TheOtherRoles.Roles.Crewmate;

namespace TheOtherRoles.Roles;

public static class RoleData
{
    public static Dictionary<RoleId, Type> allRoleIds = new()
            {
                // Crewmate
                { RoleId.Sheriff, typeof(RoleBase<Sheriff>) },
                { RoleId.Deputy, typeof(RoleBase<Deputy>) },

                // Impostor
                //{ RoleId.Godfather, typeof(RoleBase<Godfather>) },
                //{ RoleId.Mafioso, typeof(RoleBase<Mafioso>) },
                //{ RoleId.Janitor, typeof(RoleBase<Janitor>) },

                // Neutral
                //{ RoleId.Jester, typeof(RoleBase<Jester>) }
            };
}

public abstract class Role
{
    public static List<Role> allRoles = new();
    public PlayerControl player;
    public RoleId roleId;

    /// <summary>
    /// On meeting starts
    /// </summary>
    public virtual void OnMeetingStart() { }
    /// <summary>
    /// On meeting ends after exile
    /// </summary>
    public virtual void OnMeetingEnd(PlayerControl exiled = null) { }
    /// <summary>
    /// On fixed update for every player
    /// </summary>
    public virtual void FixedUpdate() { }
    /// <summary>
    /// On killing the target for every player
    /// </summary>
    /// <param name="target">Victim of the kill</param>
    public virtual void OnKill(PlayerControl target) { }
    /// <summary>
    /// On death for every player
    /// </summary>
    /// <param name="killer">The murderer (or null for exile)</param>
    public virtual void OnDeath(PlayerControl killer = null) { }
    public virtual void OnFinishShipStatusBegin() { }
    /// <summary>
    /// On role being erased/shifted
    /// </summary>
    public virtual void ResetRole() { }
    /// <summary>
    /// After being set as the role
    /// </summary>
    public virtual void PostInit() { }

    public static void ClearAll()
    {
        allRoles = new List<Role>();
    }
}

public abstract class RoleBase<T> : Role where T : RoleBase<T>, new()
{
    public static List<T> players = new();
    public static RoleId RoleId;

    public void Init(PlayerControl player)
    {
        this.player = player;
        players.Add((T)this);
        allRoles.Add(this);
    }

    public static T local
    {
        get
        {
            return players.FirstOrDefault(x => x.player == PlayerControl.LocalPlayer);
        }
    }

    public static bool exists
    {
        get { return players.Count > 0; }
    }

    public static List<PlayerControl> allPlayers
    {
        get
        {
            return players.Select(x => x.player).ToList();
        }
    }

    public static List<PlayerControl> livingPlayers
    {
        get
        {
            return players.Select(x => x.player).Where(x => !x.Data.IsDead).ToList();
        }
    }

    public static T getRole(PlayerControl player = null)
    {
        player ??= PlayerControl.LocalPlayer;
        return players.FirstOrDefault(x => x.player == player);
    }

    public static bool isRole(PlayerControl player)
    {
        return players.Any(x => x.player == player);
    }

    public static void setRole(PlayerControl player)
    {
        if (!isRole(player))
        {
            T role = new();
            role.Init(player);
            role.PostInit();
        }
    }

    public static void eraseRole(PlayerControl player)
    {
        players.DoIf(x => x.player == player, x => x.ResetRole());
        players.RemoveAll(x => x.player == player && x.roleId == RoleId);
        allRoles.RemoveAll(x => x.player == player && x.roleId == RoleId);
    }

    public static void swapRole(PlayerControl p1, PlayerControl p2)
    {
        var index = players.FindIndex(x => x.player == p1);
        if (index >= 0)
        {
            players.DoIf(x => x.player == p1, x => x.ResetRole());
            players[index].player = p2;
            players.DoIf(x => x.player == p2, x => x.PostInit());
        }
    }
}
