package environment;

import java.awt.Point;

/**
 * A class that randomly generates a game play map 
 * @author Alex White
 *
 */
public class GenerateMap {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		GameMap test = new GameMap(15, 6);
		populateMap(test);
		test.basicPrintMap();
		test.printMap();

	}
	
	/**
	 * 
	 */
	public GenerateMap() {
		
	}
	
	/**
	 * Create a new game map with all unexcavated empty Tiles
	 * @param map
	 */
	public static void populateMap(GameMap map) {
		Tile newGameTile;
		Point coords;
		for(int j = 0; j < map.mapY; j++) {
			
			for(int i = 0; i < map.mapX; i++) {
				coords = new Point(i, j);
				newGameTile = new Tile(coords);
				map.assignTile(newGameTile);
			}
		}
	}

}
