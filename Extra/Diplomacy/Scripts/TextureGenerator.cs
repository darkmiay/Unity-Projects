using UnityEngine;

public static class TextureGenerator {		

	// Height Map Colors
	//private static Color DeepColor = new Color(0, 0, 0.5f, 1);
	//private static Color ShallowColor = new Color(25/255f, 25/255f, 150/255f, 1);
    private static Color SandColor = new Color(226 / 255f, 183 / 255f, 135 / 255f, 1);
    private static Color GrassColor = new Color(34 / 255f, 139 / 255f, 34 / 255f, 1);
	//private static Color ForestColor = new Color(16 / 255f, 160 / 255f, 0, 1);
	//private static Color RockColor = new Color(0.5f, 0.5f, 0.5f, 1);            
    private static Color SnowColor = new Color(255 / 255f, 250 / 255f, 250 / 255f, 1);

	public static Texture2D GetTexture(int width, int height, Tile[,] tiles)
	{
		var texture = new Texture2D(width, height);
		var pixels = new Color[width * height];

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				switch (tiles[x,y].biomeType)
				{

				case BiomeType.sand:
					pixels[x + y * width] = SandColor;
					break;
				case BiomeType.grass:
					pixels[x + y * width] = GrassColor;
					break;
				case BiomeType.snow:
					pixels[x + y * width] = SnowColor;
					break;
				}
			}
		}
		
		texture.SetPixels(pixels);
		texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
		texture.Apply();
       
		return texture;
	}
	
}
