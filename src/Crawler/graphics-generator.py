classes = ["Warlock", "HellKnight", "Druid", "HighMage", "Mage", "Mindcrafter", "Mystic", "Paladin", "Priest", "Ranger", "Rogue", "Warrior", "WarriorMage"]
races = ["Barbarian", "Cyclops", "DarkElf", "Devilspawn", "Drawconian", "Dwarf", "Elf", "Gnome", "Golem", "HalfElf", "HalfGiant", "HalfOgre", "HalfOrc","HalfTitan", "HalfTroll", "HighElf", "Hobbit", "Human", "Imp", "Klackon", "Kobold", "MindFlayer", "Nephilim", "Nibelung", "Saracen", "Skeleton", "Sprite", "Vampire", "Yeek", "Zombie"]

print "found %s races" % (len(races))
print "found %s classes" % (len(classes))

print "Classes"
for i in range(len(classes)):
    print(i, classes[i])

print ""
print "Races"
for i in range(len(races)):
    print(i, races[i])

print ""
print "Race+Class numbers"
for r in range(len(races)):
    for c in range(len(classes)):
        print((r * len(classes)) + c, races[r] + " " + classes[c])
    
print ""
print "Enum entries"
for r in range(len(races)):
    for c in range(len(classes)):
        print("        %s%s," % (races[r], classes[c]))