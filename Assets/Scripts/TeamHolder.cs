using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Teams
{
    private static List<Team> teams = new();

    public static void regTeam(Team t)
    {
        teams.Add(t);
    }

    public static List<Team> teamEnemiesOf(ITeamable teamable)
    {
        Team t = teamable.GetTeam();
        List<Team> ret = new();
        foreach(Team team in teams)
        {
            if (team != t) ret.Add(team);
        }
        return ret;
    }
    public static List<Unit> unitEnemiesOf(ITeamable t)
    {
        List<Unit> ret = new();
        foreach (Team team in teamEnemiesOf(t))
        {
            ret.AddRange(team.GetUnits());
        }
        return ret;
    }
}
