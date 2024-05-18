from team import Team
import csv

def getTeamInformation(teamName):











#import csv
#def createTeams():
#    teamDict = {}
#    with open('../playersTeamsAndStats.csv', newline='', encoding='utf-8-sig') as PlayersTeamsStats:
#        file = csv.reader(PlayersTeamsStats, delimiter=',')
#        for row in file:
#            team = row[1]
#            if team not in teamDict:
#                teamDict[team] = []
#            teamDict[team].append(row[0])
#        new_dictionary = teamDict.copy()
#        for key, value in teamDict.items():
#            if len(value) < 5:
#                del new_dictionary[key]
#        return new_dictionary

#def createPlayerDict():
#    playerDict = {}
#    with open('../playersTeamsAndStats.csv', newline='', encoding='utf-8-sig') as PlayersTeamsStats:
#        file = csv.reader(PlayersTeamsStats, delimiter=',')
#        for row in file:
#            appendList = []
#            appendList.append(row[2])
#            appendList.append(row[3])
#            appendList.append(row[4])
#            appendList.append(row[5])
#            appendList.append(row[6])
#            appendList.append(row[7])
#            playerDict[row[0]] = appendList
#    return playerDict


#if __name__ == "__main__":
#    print(createTeams())
#    print(createPlayerDict())
