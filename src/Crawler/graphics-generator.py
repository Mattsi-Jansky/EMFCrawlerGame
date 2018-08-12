classes = ["Warlock", "HellKnight", "Druid", "HighMage", "Mage", "Mindcrafter", "Mystic", "Paladin", "Priest", "Ranger", "Rogue", "Warrior", "WarriorMage"]
races = ["Barbarian", "Cyclops", "DarkElf", "Devilspawn", "Drawconian", "Dwarf", "Elf", "Gnome", "Golem", "HalfElf", "HalfGiant", "HalfOgre", "HalfOrc","HalfTitan", "HalfTroll", "HighElf", "Hobbit", "Human", "Imp", "Klackon", "Kobold", "MindFlayer", "Nephilim", "Nibelung", "Saracen", "Skeleton", "Spectre", "Sprite", "Vampire", "Yeek", "Zombie"]

envTiles = ["NWall", "NWallTorch", "NWallTorch2", "DamagedNWall", "Wall", "Floor", "SpiralStaircase", "StairsUp", "StairsDown", "Hole", "Hatch", "HatchOpen", "AltFloor", "FancyFloor", "Nothing", "Nothing2"]
envColours = ["Gray", "Brown", "Blue", "Red"]
envTilesStart = 402

print "found %s races" % (len(races))
print "found %s classes" % (len(classes))

print "found %s envTiles" % (len(envTiles))
print "found %s envColours" % (len(envColours))

print "Classes"
for i in range(len(classes)):
    print(i, classes[i])

print ""
print "Races"
for i in range(len(races)):
    print(i, races[i])

print ""
print "envTiles"
for i in range(len(envTiles)):
    print(i, envTiles[i])

print ""
print "envColours"
for i in range(len(envColours)):
    print(i, envColours[i])

print ""
print "Race+Class numbers"
for r in range(len(races)):
    for c in range(len(classes)):
        print((r * len(classes)) + c, races[r] + " " + classes[c])

print ""
print "EnvTiles+Colours numbers"
for c in range(len(envColours)):
    for t in range(len(envTiles)):
        print(envTilesStart + (c * len(envTiles)) + c, envTiles[t] + " " + envColours[c])

print ""
print "Enum entries"
for r in range(len(races)):
    for c in range(len(classes)):
        print("        %s%s," % (races[r], classes[c]))
for c in range(len(envColours)):
    for t in range(len(envTiles)):
        print("        %s%s," % (envTiles[t], envColours[c]))
