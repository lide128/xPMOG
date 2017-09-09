package environment;

import java.awt.Point;
import java.util.Iterator;
import java.util.List;
import java.util.Random;

import environment.Tile.TileOccupiedException;
import player.Player;
import player.Team;

/**
 * A class that randomly generates a game play map 
 * @author Alex White
 *
 */
public class GenerateMap {

	public static GameMap generateMap(int xSize, int ySize, List<Team> teams) throws Exception {
		if (teams.size() != 4)
			throw new Exception("DON'T DO THAT -- YOU KNOW WHAT I MEAN");
		GameMap map = new GameMap(xSize, ySize);
		populateMap(map, teams);
		return map;
	}
	
	/**
	 * Create a new game map with all unexcavated empty Tiles
	 * @param map
	 */
	public static void populateMap(GameMap map, List<Team> teams) {
		Iterator<Team> it = teams.iterator();
		Tile newGameTile;
		Point coords;
		Random rand = new Random();
		for(int j = 0; j < map.mapY; j++) {
			
			for(int i = 0; i < map.mapX; i++) {
				TileCover cover = TileCover.EMPTY_COVER;
				
				if (i == 0 && (j == 0 || j == (map.mapY - 1)) || i == (map.mapX - 1) && (j == 0 || j == (map.mapY - 1))) {
					cover = new Base();
				} else if (rand.nextInt(100) < 0) {
					cover = new Dirt();
				}
				
				coords = new Point(i, j);
				newGameTile = new Tile(coords, cover);
				map.assignTile(newGameTile, coords);
				if (cover instanceof Base) {
					try {
						Player player = it.next().getPlayers().get(0);
//						newGameTile.addPlayer(player);
						map.movePlayer(player, new Point(i,j));
					} catch (TileOccupiedException e) {
						// OH LORD, WHAT DO?
						e.printStackTrace();
					}
				}
			}
		}
	}

}
