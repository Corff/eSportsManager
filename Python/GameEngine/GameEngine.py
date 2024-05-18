from ProcessPlayers import createPlayerDict, createTeams
from buyPhase import decideLoadout

teams = createTeams()
playerStats = createPlayerDict()

team1 = "Vitality"
team2 = "FaZe"

team1MatchStats = dict.fromkeys(teams[team1], [800,0,0,0])
team2MatchStats = dict.fromkeys(teams[team2], [800,0,0,0])
allMatchStats = dict(**team1MatchStats, **team2MatchStats)
#Loadout = [Primary, Secondary,
team1Loadout = dict.fromkeys(teams[team1], ["glock"])
team2Loadout = dict.fromkeys(teams[team2],  ["usp-s"])
allLoadouts = dict(**team1Loadout, **team2Loadout)

for round in range(1,31):
    print("======================")
    print(allLoadouts)
    #Buy Phase
    for i in allMatchStats.keys():
        print(i, allMatchStats[i][0], allLoadouts[i])

