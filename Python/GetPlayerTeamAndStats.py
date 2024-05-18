import time
import csv
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from webdriver_manager.chrome import ChromeDriverManager
import undetected_chromedriver


def getPlayerPages():
    playerPageList = []
    driver.get("https://www.hltv.org/ranking/teams")
    time.sleep(1)
    driver.find_element(By.ID, "CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll").click()
    time.sleep(1)
    playerLinkSearch = driver.find_elements(By.CLASS_NAME, "pointer")
    for player in playerLinkSearch:
        playerPageList.append(player.get_attribute("href"))
    return playerPageList



def getPlayerTeam():
    playerInfo = driver.find_element(By.CLASS_NAME, "playerInfoWrapper")
    team = playerInfo.text.split("\n")[5]
    return team

def getPlayerInfo():
    age = driver.find_element(By.XPATH, "//*[contains(text(),'years')]").text.split(" ")[0]
    nation = driver.find_element(By.CLASS_NAME, "flag").get_attribute("title")
    realName = driver.find_element(By.CLASS_NAME, "playerRealname").text
    return [realName, nation, age]

def createList(row):
    time.sleep(3)
    rowList = []
    driver.get(row)
    playerName = driver.find_element(By.CLASS_NAME, "playerNickname").text
    playerInfo = getPlayerInfo()
    playerTeam = getPlayerTeam()
    rowList.append(playerName)
    rowList.append(playerInfo[0])
    rowList.append(playerInfo[1])
    rowList.append(playerInfo[2])
    rowList.append(playerTeam)
    for j in statistics:
        rowList.append(getStatsByName(j))
    return rowList


def getStatsByName(name):
    try:
        bVal = driver.find_element(By.XPATH, "//b[text()='" + name + "']")
        bParent = bVal.find_element(By.XPATH, "./..")
        bChildren = bParent.find_elements(By.XPATH, ".//*")
    except:
        return "0"
    return bChildren[1].text


options = Options()
options.add_argument('--disable-dev-shm-usage')
driver = undetected_chromedriver.Chrome(options=options)

statistics = ["Rating 2.0", "Kills per round", "Headshots", "Maps played", "Deaths per round",
              "Rounds contributed"]

playerPages = getPlayerPages()
newList = open('playersTeamsAndStats.csv', 'w', encoding='utf-8-sig', newline='')
writer = csv.writer(newList)
for i in range(len(playerPages)):
    newRow = createList(playerPages[i])
    writer.writerow(newRow)
    print(newRow)

newList.close()
driver.close()

