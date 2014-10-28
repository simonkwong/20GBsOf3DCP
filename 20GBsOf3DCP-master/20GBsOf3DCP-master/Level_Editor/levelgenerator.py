# -*- coding: utf-8 -*-

from PIL import Image
import math

im = Image.open("test.png") #Can be many different formats.

pix = im.load()
width = im.size[0] #Get the width and hight of the image for iterating over
height = im.size[1]

f = open('worldoutput.txt', 'w')

def isBlack(rgba):
    if (rgba[0] == 0 and rgba[1] == 0 and rgba[2] == 0 and rgba[3] == 255):
        return True
    else:
        return False

def isYellow(rgba):
    if (rgba[0] == 255 and rgba[1] == 255 and rgba[2] == 0 and rgba[3] == 255):
        return True
    else:
        return False

def isRed(rgba):
    if (rgba[0] == 255 and rgba[1] == 0 and rgba[2] == 0 and rgba[3] == 255):
        return True
    else:
        return False

def isBlue(rgba):
    if (rgba[0] == 0 and rgba[1] == 0 and rgba[2] == 255 and rgba[3] == 255):
        return True
    else:
        return False

def isGreen(rgba):
    if (rgba[0] == 0 and rgba[1] == 255 and rgba[2] == 0 and rgba[3] == 255):
        return True
    else:
        return False
    

coins = []
enemies = []
platforms = []
spikes = []
scrolls = []

for x in range(width):
    for y in range(height):
        RGBA = pix[x,y]
        if isBlack(RGBA):
            platforms.append("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
            print '*',
        elif isYellow(RGBA):
            print 'Y',
            coins.append("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
        elif isRed(RGBA):
            enemies.append("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
            print 'R',
        elif isBlue(RGBA):
            print 'B',
            spikes.append("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
        elif isGreen(RGBA):
            print 'G',
            scrolls.append("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
        else:
            print '_',
    print ""
                        
f.write("<enemyPositions type = \"Vector2List\">\n")
for x in enemies:
    f.write(x)
f.write("</enemyPositions>\n")


f.write("\n")

f.write("<platformPositions type = \"Vector2List\">\n")
for x in platforms:
    f.write(x)
f.write("</platformPositions>\n")


f.write("\n")

f.write("<spikePositions type = \"Vector2List\">\n")
for x in spikes:
    f.write(x)
f.write("</spikePositions>\n")


f.write("<coinPositions type = \"Vector2List\">\n")
for x in coins:
    f.write(x)
f.write("</coinPositions>\n")


f.write("<scrollPositions type = \"Vector2List\">\n")
for x in scrolls:
    f.write(x)
f.write("</scrollPositions>\n")
