# CelestePixelArtGenerator

## [DOWNLOAD LINK](https://www.dropbox.com/scl/fi/jgx647bm8p4n07jabqhr7/CelestePixelInstaller.Installer.zip?rlkey=p9gb9wwxr5r5yh5w7gdn0odq6&dl=0)

## Step by step guide
1. Install [paint.net](https://www.getpaint.net/download.html).
2. Open the desired image in paint.net.
3. On the top navigation bar, select Effects > Color > Quantize.
4. Set the color count to 3. Make sure the image is devoid of transparent pixels. (The lightest color will be turned into foreground tiles, the second lightest will be turned into background tiles, and the darkest color will have no tiles.)
5. Download the [Celeste Pixel Art Generator Installer](https://www.dropbox.com/scl/fi/jgx647bm8p4n07jabqhr7/CelestePixelInstaller.Installer.zip?rlkey=p9gb9wwxr5r5yh5w7gdn0odq6&dl=0), export it and run setup.exe.
6. Run the program. (A shortcut should've appeared on your desktop.)
7. Paste your image's path into the input field. (Quotation marks will be automatically truncated)
8. Select your foreground and background tiles. Do not type their name, but only their ID. This also works with modded tiles.
9. A text file named "Output.txt" should've opened itself. Select everything with CTRL+A and copy with CTRL+C.
10. In Lönn, create a new room with the dimensions of your picture. (The program also displays the dimensions of the image.)
### RECOMMENDED: In Lönn, go to Edit > Settings > Editor > Clipboard and turn off "Paste Centered". This will make pasting the image easier.
11. Use the selection tool and set it to "All".
12. Put your cursor on the top left tile of the room, if you disabled "Paste Centered" in the settings. Otherwise, try to aim for the center of the room. Bigger images may take more time to get pasted.

## Adding red and yellow pixels
1. In paint.net, instead of selecting 3 colors during quantization, you can use 4 or 5.
2. Select the bucket tool, and set the tolerance to 0%.
3. Click on the button labeled "Aliased rendering" to disable anti-aliasing.
4. For red pixels, set your color to #FF0000 exactly. For yellow pixels, use #FFFF00.
### WARNING: Do not place too many red or yellow pixels. Everest can handle a few thousand entities but too many can cause the game to be unable to run the map.
5. Using the bucket tool, shift + right-click on the color you wish to turn into your desired color. The image cannot contain more than 3 colors excluding red and yellow.
6. While generating the image, if you have yellow pixels, there will be a prompt asking you to enter your desired jump through type. Type the full name. (Example: "wood" or "cliffside". This also works with modded textures. Type what you'd type in the "texture" box in Lönn. For example: "RadioactiveBluePlatypus/CoolMap/AwesomeCustomJumpThroughTexture")

