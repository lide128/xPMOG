package environment;

import java.awt.Point;
import java.util.HashMap;
import java.util.Map;

import environment.Tile.TileOccupiedException;
import player.Player;

/**
 * A class that represents the randomly generated game play area
 * The size of the GameMap is determined by the user in the constructor.
 * @author Alex White 
 *
 */
public class GameMap {
	
	int mapX;
	int mapY;
	char horizontalLineChar = '-';
	Tile gameMap[][];
	private Map<Player, Tile> playerLocations;
	

	public GameMap(int x, int y) {
		mapX = x;
		mapY = y;
		gameMap = new Tile[mapX][mapY];
		playerLocations = new HashMap<>();
	}
	
	public void assignTile(Tile toAssign, Point coordinates) {
		gameMap[coordinates.x][coordinates.y] = toAssign;
	}
	
	/**
	 * A method which prints the GameMap to the console showing all of the Tiles and their representative symbol
	 * @param cellLength - the number of characters the cells in the GameMap will be printed out with in the console
	 */
	public void printMap(int cellLength) {
		for(int j = 0; j < (mapY * 2)+1; j++) {
			System.out.print('|');
			
			for(int i = 0; i < mapX; i++) {
				if(j % 2 != 0) {
					System.out.print(cellFit(gameMap[i][j/2].getSymbol() + "", cellLength));
				}
				else {
					printGridLine(cellLength);
				}
				System.out.print('|');
			}
			System.out.println();
		}
	}
	
	/**
	 * Print the map to the console without any of the formatting and lines.
	 */
	public void basicPrintMap() {
		for(int j = mapY-1; j >= 0; j--) {
			
			for(int i = 0; i < mapX; i++) {
				System.out.print(gameMap[i][j].getSymbol());
			}
			System.out.println();
		}
	}
	
	/**
	 * A method to recursively create lines for the GameMap grid in the console output
	 * @param num the length of the grid line desired
	 */
	public void printGridLine(int num) {
		if(num > 0) {
			System.out.print(horizontalLineChar);
			printGridLine(num-1);
		}
	}
	
	/**
	 * A method to add space to a string that will go in a tile of the GameMap grid in the console output
	 * @param num the number of spaces of which to add
	 * @return the string with spaces added
	 */
	public String addSpace(int num) {
		String space = "";
		for(int i = 0; i < num; i++) {
			space += ' ';
		}
		return space;
	}
	
	/**
	 * A method to fit Strings shorter than the cell length to the map output
	 * @param toFit the string with which to fit
	 * @return
	 */
	public String cellFit(String toFit, int cellLength) {
		int spaceToAdd = cellLength - toFit.length();
		return toFit + addSpace(spaceToAdd);
	}
	
	public void movePlayer(Player player, Point newPoint) throws TileOccupiedException {
		Tile tile = gameMap[newPoint.x][newPoint.y];
		tile.addPlayer(player);
		Tile oldTile = playerLocations.get(player);
		if (oldTile != null)
			oldTile.removePlayer();
		playerLocations.put(player, tile);
	}
	
	public void movePlayer(Player player, Direction direction) throws TileOccupiedException {
		Tile oldTile = playerLocations.get(player);
		Point coordinates = oldTile.getCoordinates();
		Point newPoint = coordinates.getLocation(); // don't overwrite the old Tile's location
		newPoint.translate(direction.dx, direction.dy);
		movePlayer(player, newPoint);
	}
	
	public enum Direction {
		UP(0,1), DOWN(0,-1), LEFT(-1,0), RIGHT(1,0);
		
		public final int dx, dy;
		
		private Direction(int dx, int dy) {
			this.dx = dx;
			this.dy = dy;
		}
	}
	
	
	
	
	
	
}
