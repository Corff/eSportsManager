import csv

with open('playersTeamsAndStats.csv', 'r+', encoding='utf-8-sig') as f:
    #iterate through rows, and getting unique teams and amount of players in those teams. teams are [1]
    reader = csv.reader(f, delimiter=',')
    uniqueTeams = []
    for row in reader:
        if row[1] not in uniqueTeams:
            uniqueTeams.append(row[1])
    print(uniqueTeams)