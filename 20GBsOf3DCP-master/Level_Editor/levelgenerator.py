# -*- coding: utf-8 -*-

from PIL import Image
import math

im = Image.open("test.jpeg") #Can be many different formats.

pix = im.load()
width = im.size[0] #Get the width and hight of the image for iterating over
height = im.size[1]

f = open('worldoutput.txt', 'w')


f.write("<enemyPositions type = \"Vector2List\">\n")
for x in range(width):
    for y in range(height):
        tempRGB = pix[x,y]
        if tempRGB[0] >= 250 and tempRGB[0] <=255 and tempRGB[1] == 0 and tempRGB[2] == 0:
            f.write("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
f.write("</enemyPositions>\n")


f.write("\n")

f.write("<platformPositions type = \"Vector2List\">\n")
for x in range(width):
    for y in range(height):
        tempRGB = pix[x,y]
        if tempRGB[0] >= 0 and tempRGB[0] <=10 and tempRGB[1] >= 0 and tempRGB[1] <=10 and tempRGB[2] >= 0 and tempRGB[2] <=10:
            f.write("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
f.write("</platformPositions>\n")


f.write("\n")

f.write("<spikePositions type = \"Vector2List\">\n")
for x in range(width):
    for y in range(height):
        tempRGB = pix[x,y]
        if tempRGB[2] >= 240 and tempRGB[2] <=255 and tempRGB[1] >= 0 and tempRGB[1] <= 10 and tempRGB[0] >= 0 and tempRGB[0] <= 10:
            print "BLUE"
            f.write("\t<pos> <x>" + str(x * 35) + "</x> <y>" + str(y * 35) + "</y> </pos>\n")
f.write("</spikePositions>\n")

print "DONE"
