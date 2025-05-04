using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Formats.Asn1.AsnWriter;

try
{
    string Path = Ask("Path: ");
    Path = Path.Trim('\"');

    Console.WriteLine(Path);

    Console.WriteLine("Foreground Tiles:\r\n1: Dirt\r\n3: Snow\r\n4: Girder\r\n5: Tower\r\n6: Stone\r\n7: Cement\r\n8: Rock\r\n9: Wood\r\na: Wood Stone Edges\r\nb: Cliffside\r\nc: Pool Edges\r\nd: Temple A\r\ne: Temple B\r\nf: Cliffside Alt\r\ng: Reflection\r\nG: Reflection Alt\r\nh: Grass\r\ni: Summit\r\nj: Summit No Snow\r\nk: Core\r\nl: Deadgrass\r\nm: Lostlevels (Default)\r\nn: Scifi\r\nz: Template");
    string foregroundTile = Ask("Enter your chosen foreground tile id (Case sensitive): ");
    Console.WriteLine();

    if (foregroundTile.Length != 1)
        foregroundTile = "m";

    Console.WriteLine("Background Tiles: \r\n1: Dirt (Default)\r\n2: Brick\r\n3: Brick Ruined\r\n4: Wood\r\n5: Resort Stone\r\n6: Cliffside\r\n7: Pool\r\n8: Temple A\r\n9: Temple B\r\na: Reflection\r\nb: Snow\r\nc: Summit\r\nd: Core\r\ne: Lostlevels\r\nz: Template");
    string backgroundTile = Ask("Enter your chosen background tile id (Case sensitive): ");
    Console.WriteLine();

    if (backgroundTile.Length != 1)
        backgroundTile = "1";

    string jumpthru = "wood";

    int expectedColorCount = 3;

    Bitmap bitmap = new Bitmap(Path);

    HashSet<Color> colors = new HashSet<Color>();

    for (int i = 0; i < bitmap.Height; i++)
    {
        for (int j = 0; j < bitmap.Width; j++)
        {
            colors.Add(bitmap.GetPixel(j, i));
        }
    }

    List<Color> colorsList = colors.ToList();

    List<Color> sortedColorsList = new List<Color>();

    for (int i = 0; i < colorsList.Count; i++)
    {
        if ((colorsList[i].R == 255 && colorsList[i].G == 0 && colorsList[i].B == 0))
        {
            colorsList.Remove(colorsList[i]);
            expectedColorCount++;
            break;
        }
    }

    for (int i = 0; i < colorsList.Count; i++)
    {
        if ((colorsList[i].R == 255 && colorsList[i].G == 255 && colorsList[i].B == 0))
        {
            colorsList.Remove(colorsList[i]);
            expectedColorCount++;
            Console.WriteLine("Jump Thrus:\r\n- wood\r\n- dream\r\n- temple\r\n- templeB\r\n- cliffside\r\n- reflection\r\n- core\r\n- moon");
            jumpthru = Ask("Enter the jump thru texture: ");

            if (jumpthru == string.Empty)
                jumpthru = "wood";

            break;
        }
    }

    for (int i = 0; i < 3; i++)
    {
        Color smallest = new Color();
        int smallestamount = 10000000;
        foreach (Color color2 in colorsList)
        {
            if (color2.R + color2.G + color2.B < smallestamount)
            {
                smallest = color2;
                smallestamount = color2.R + color2.G + color2.B;
            }
        }
        colorsList.Remove(smallest);
        sortedColorsList.Add(smallest);
    }

    colorsList = sortedColorsList;

    if (colors.Count != expectedColorCount)
        throw (new Exception("Image must have exactly 3 colors (Excluding red and yellow)"));

    int FGRed = 211;
    int BGRed = 79;

    string appdataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CelestePixelArtGenerator\";

    Directory.CreateDirectory(appdataDir);
    StreamWriter sw = new StreamWriter(appdataDir + @"output.txt");
    try
    {
        using (sw)
        {
            // Writing foreground tiles
            sw.Write("{\r\n    {\r\n        _fromLayer = \"tilesFg\",\r\n        height = ");
            sw.Write(bitmap.Height);
            sw.Write(",\r\n        tiles = \"");
            for (int i = 0; i < bitmap.Height; i++)
            {
                if (i != 0)
                    sw.Write("\\n");
                string zeroes = string.Empty;
                for (int j = 0; j < bitmap.Width; j++)
                {
                    if (bitmap.GetPixel(j, i) == colorsList[2])
                    {
                        sw.Write(zeroes);
                        sw.Write(foregroundTile);
                        zeroes = string.Empty;
                    }
                    else
                    {
                        zeroes += "0";
                    }
                }
            }
            sw.Write("\",\r\n        width = ");
            sw.Write(bitmap.Width);
            sw.Write(",\r\n        x = 0,\r\n        y = 0\r\n    },\r\n    {\r\n        _fromLayer = \"tilesBg\",\r\n        height = ");
            sw.Write(bitmap.Height);
            // Writing background tiles
            sw.Write(",\r\n        tiles = \"");
            for (int i = 0; i < bitmap.Height; i++)
            {
                if (i != 0)
                    sw.Write("\\n");
                string zeroes = string.Empty;
                for (int j = 0; j < bitmap.Width; j++)
                {
                    if (bitmap.GetPixel(j, i) == colorsList[1])
                    {
                        sw.Write(zeroes);
                        sw.Write(backgroundTile);
                        zeroes = string.Empty;
                    }
                    else
                    {
                        zeroes += "0";
                    }
                }
            }
            sw.Write("\",\r\n        width = ");
            sw.Write(bitmap.Width);
            sw.Write(",\r\n        x = 0,\r\n        y = 0\r\n    }");

            int k = 1;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    if (bitmap.GetPixel(j, i).R == 255 && bitmap.GetPixel(j, i).G == 0 && bitmap.GetPixel(j, i).B == 0)
                    {
                        sw.Write($",\n    {{\r\n        _fromLayer = \"entities\",\r\n        _id = {k},\r\n        _name = \"player\",\r\n        isDefaultSpawn = false,\r\n        x = {(j - 1) * 8},\r\n        y = {(i) * 8}\r\n    }}");
                        k++;
                    }
                }
            }

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    if (bitmap.GetPixel(j, i).R == 255 && bitmap.GetPixel(j, i).G == 255 && bitmap.GetPixel(j, i).B == 0)
                    {
                        int startx = j;
                        int l = 0;
                        for (; j < bitmap.Width && (bitmap.GetPixel(j, i).R == 255 && bitmap.GetPixel(j, i).G == 255 && bitmap.GetPixel(j, i).B == 0); j++)
                        {
                            l++;
                        }
                        sw.Write($",\n    {{\r\n        _fromLayer = \"entities\",\r\n        _id = {k},\r\n        _name = \"jumpThru\",\r\n        surfaceIndex = -1,\r\n        texture = \"{jumpthru}\",\r\n        width = {l * 8},\r\n        x = {(startx - 1) * 8},\r\n        y = {(i - 1) * 8}\r\n    }}");
                        k++;

                    }
                }
            }



            sw.Write("\r\n}");
        }
    }
    finally
    { sw.Close(); }

    Console.WriteLine($"IMAGE GENERATED! ({bitmap.Width}x{bitmap.Height})");

    if (File.Exists(appdataDir + @"output.txt"))
    {
        Process.Start("notepad.exe", appdataDir + @"output.txt");
    }

    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);

    string Ask(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}
catch (Exception ex) 
{
    Console.WriteLine(ex);
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
}
