package environment;

import java.awt.Point;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Random;
import java.util.Set;

import player.Player;
import environment.Tile.TileOccupiedException;

/**
 * A class that represents the randomly generated game play area
 * The size of the GameMap is determined by the user in the constructor.
 * @author Alex White 
 *
 */
public class GameMap {
	
	int mapX;
	int mapY;
	Tile[][] gameMap;
	private Map<Player, Tile> playerLocations;
	

	public GameMap(int x, int y) {
		mapX = x;
		mapY = y;
		gameMap = new Tile[mapX][mapY];
		playerLocations = new HashMap<>();
	}
	
	public Set<Point> corners() {
		Set<Point> corners = new HashSet<>();
		corners.add(new Point(0,0));
		corners.add(new Point(mapX-1,0));
		corners.add(new Point(0,mapY-1));
		corners.add(new Point(mapX-1,mapY-1));
		return corners;
	}
	
	public Tile getLocation(Player player) {
		return playerLocations.get(player);
	}
	
	public Tile getTile(Point coordinates) {
		return gameMap[coordinates.x][coordinates.y];
	}
	
	void assignTile(Tile toAssign, Point coordinates) {
		gameMap[coordinates.x][coordinates.y] = toAssign;
	}
	
	/**
	 * @param player to be moved
	 * @param direction to move in, for a distance of one {@link Tile}
	 * @return {@code true} if the player was successfully moved, 
	 * or {@code false} if the adjacent tile was not {@link TileCover#isTraversible() traversible} 
	 */
	public boolean movePlayer(Player player, Direction direction) {
		boolean pass = false;
		try {
			pass = movePlayer(player, translate(getLocation(player), direction));
		} catch (ArrayIndexOutOfBoundsException | TileNotFoundException e) {
//			System.out.println("Can't move off the map!");
			// do nothing, return false
		}
		return pass;
	}
	
//	void movePlayer(Player player, Point newPoint) throws TileOccupiedException {
//		movePlayer(player, getTile(newPoint));
//	}
	
	/**
	 * @param player to be moved
	 * @param tile to move to
	 * @return {@code true} if the player was successfully moved, 
	 * or {@code false} if the tile was not {@link TileCover#isTraversible() traversible} 
	 */
	boolean movePlayer(Player player, Tile tile) {
		try {
			tile.addPlayer(player);
		} catch (TileOccupiedException e) {
			return false;
		}
		Tile oldTile = playerLocations.put(player, tile);
		if (oldTile != null)
			oldTile.removePlayer();
		return true;
	}
	
	/**
	 * @return the tile that is adjacent to the given tile in the given direction
	 * @throws TileNotFoundException 
	 */
	public Tile translate(Tile tile, Direction direction) throws TileNotFoundException {
		return translate(tile, direction, 1);
	}
	
	public Tile translate(Tile tile, Direction direction, int distance) throws TileNotFoundException {
		try {
			return getTile(translate(tile.getCoordinates(), direction, distance));
		} catch (ArrayIndexOutOfBoundsException e) {
			throw new TileNotFoundException(e);
		}
	}
	
//	private static Point translate(Point point, Direction direction) {
//		return translate(point, direction, 1);
//	}
	
	public static Point translate(Point point, Direction direction, int distance) {
		Point newPoint = point.getLocation(); // don't overwrite the old Tile's location
		newPoint.translate(distance*direction.dx, distance*direction.dy);
		return newPoint;
	}
	
	public void printMap(int cellLength) {
		MapPrinter.printMap(this, cellLength);
	}
	
	public void basicPrintMap() {
		MapPrinter.basicPrintMap(this);
	}
	
	private static class MapPrinter {
	
		private static char horizontalLineChar = '-';
	
		/**
		 * A method which prints the GameMap to the console showing all of the Tiles and their representative symbol
		 * @param cellLength - the number of characters the cells in the GameMap will be printed out with in the console
		 */
		public static void printMap(GameMap map, int cellLength) {
			for(int j = (map.mapY * 2); j >= 0; j--) {
				System.out.print('|');
				
				for(int i = 0; i < map.mapX; i++) {
					if(j % 2 != 0) {
						System.out.print(cellFit(map.gameMap[i][j/2].getSymbol() + "", cellLength));
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
		public static void basicPrintMap(GameMap map) {
			for(int j = map.mapY-1; j >= 0; j--) {
				
				for(int i = 0; i < map.mapX; i++) {
					System.out.print(map.gameMap[i][j].getSymbol());
				}
				System.out.println();
			}
		}
	
		/**
		 * A method to recursively create lines for the GameMap grid in the console output
		 * @param num the length of the grid line desired
		 */
		private static void printGridLine(int num) {
			if(num > 0) {
				System.out.print(horizontalLineChar);
				printGridLine(num-1);
			}
		}
	
		/**
		 * A method to fit Strings shorter than the cell length to the map output
		 * @param toFit the string with which to fit
		 * @return
		 */
		private static String cellFit(String toFit, int cellLength) {
			int spaceToAdd = cellLength - toFit.length();
			return toFit + addSpace(spaceToAdd);
		}
	
		/**
		 * A method to add space to a string that will go in a tile of the GameMap grid in the console output
		 * @param num the number of spaces of which to add
		 * @return the string with spaces added
		 */
		private static String addSpace(int num) {
			String space = "";
			for(int i = 0; i < num; i++) {
				space += ' ';
			}
			return space;
		}
		
		
	}

	/**
	 * Unit vectors: {@link #UP UP}, {@link #DOWN DOWN}, {@link #LEFT LEFT}, {@link #RIGHT RIGHT}
	 * 
	 * @author xpxKSS
	 *
	 */
	public enum Direction {
		/**(0,1)*/ 	UP(0,1), 
		/**(0,-1)*/ DOWN(0,-1), 
		/**(-1,0)*/ LEFT(-1,0), 
		/**(1,0)*/ 	RIGHT(1,0);
		
		public final int dx, dy;
		
		private Direction(int dx, int dy) {
			this.dx = dx;
			this.dy = dy;
		}
		
		public static Direction random(Random rand) {
			Direction[] directions = Direction.values();
			return directions[rand.nextInt(directions.length)];
		}
	}
	
	public class TileNotFoundException extends Exception {
		private static final long serialVersionUID = 8626777410520409827L;

		private TileNotFoundException() {
			super("Tile not found!");
		}
		private TileNotFoundException(Throwable t) {
			super("Tile not found!", t);
		}
	}
	
}
