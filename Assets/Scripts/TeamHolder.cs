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

    public static List<Team> teamEnemiesOf(Teamable teamable)
    {
        Team t = teamable.getTeam();
        List<Team> ret = new();
        foreach(Team team in teams)
        {
            if (team != t) ret.Add(team);
        }
        return ret;
    }
    public static List<Unit> unitEnemiesOf(Teamable t)
    {
        List<Unit> ret = new();
        foreach (Team team in teamEnemiesOf(t))
        {
            ret.AddRange(team.getUnits());
        }
        return ret;
    }
}
