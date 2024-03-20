# Donjon2DD
 A parser to create Dungeondraft Maps out of Donjoin created maps.

## Step 1: Generate a Donjon Map

Through the use of donjon random dungeon generator (https://donjon.bin.sh/adnd/dungeon/) we can create a randomly generated map. Currently the map that were generated used the following settings (asterisks denote crucial settings that should not be changed as the software won't work with a different option)


**Dungeon Size:** Medium <br>
**Dungeon Layout:** *Rectangle<br>
**Peripheral Egress?**: No<br>
**Room Layout:** Dense<br>
**Room Size:** Large<br>
**Polymorph Rooms?**: *No<br>
**Doors:** Standard<br>
**Corridors:** Errant<br>
**Remove Deadends?**: Some<br>
**Stairs?** Many<br>
**Map Style:** Standard<br>
**Grid:** *Square<br>


![image](https://github.com/Drejn/Donjon2DD/assets/6476999/ef0bb2da-fecb-425a-9d57-02f8565637de)

## Step 2: Download the Donjon Map in JSON format

After tweaking the above settings, click on "Construct Dungeon". After the construction is done, go to the bottom of the new page and click on "Download" and after that "JSON"

![image](https://github.com/Drejn/Donjon2DD/assets/6476999/f6aba2f6-51d1-4767-9e4b-2a685e706837)

## Step 2: Convert the map with Donjon2DD

After downloading the JSON from the Donjon website, open the Donjon2DD.exe file. The following window will appear

![image](https://github.com/Drejn/Donjon2DD/assets/6476999/03f8d7e6-ea4e-49ad-becc-984eef8463fb)

Click on the load button and select the json file you downloaded from the Donjon website. After it's loaded the software will show you some data about the map.

![image](https://github.com/Drejn/Donjon2DD/assets/6476999/a37e8037-67f1-4edb-810b-ecac8fd2ef2f)

Once you loaded your JSON file it's time to click on the "Generate" button!

## Step 3: Check your result

Once the generation is done you will receive a pop up message. Inside the Donjon2DD folder there should be a directory named "OUTPUT". Inside there should now be your converted map called "**donjonmap.dungeondraft_map**". **BE SURE TO COPY IT SOMEWHERELSE AS THE MAPS ARE OVERWRITTEN EVERY TIME YOU CONVERT A NEW MAP**

![image](https://github.com/Drejn/Donjon2DD/assets/6476999/25da0d79-daeb-441c-802b-0771e7a06fed)


# More Techical Details

- Currently the map is created with default wall,patterns and doors texture.
- The map is limited to create only walls, doors and patterns (so no stairs or different door type are taken in consideration)
- 

