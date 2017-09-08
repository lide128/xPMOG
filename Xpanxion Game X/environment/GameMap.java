package environment;

import java.awt.Point;

/**
 * A class that represents the randomly generated game play area
 * The size of the GameMap is determined by the user in the constructor.
 * @author Alex White 
 *
 */
public class GameMap {
	
	int mapX;
	int mapY;
	int defaultCellLength = 10; //the number of characters the cells in the GameMap will be printed out with in the console
	char horizontalLineChar = '-';
	Tile gameMap[][];
	

	public GameMap(int x, int y) {
		mapX = x;
		mapY = y;
		gameMap = new Tile[mapX][mapY];
	}
	
	public void assignTile(Tile toAssign) {
		gameMap[toAssign.coordinates.x][toAssign.coordinates.y] = toAssign;
	}
	
	/**
	 * A method which prints the GameMap to the console showing all of the Tiles and their representative symbol
	 * @param toPrint
	 */
	//TODO
	public void printMap() {
		for(int j = 0; j < (mapY * 2)+1; j++) {
			System.out.print('|');
			
			for(int i = 0; i < mapX; i++) {
				if(j % 2 != 0) {
					System.out.print(cellFit(gameMap[i][j].getSymbol() + ""));
				}
				else {
					printGridLine(defaultCellLength);
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
		for(int j = 0; j < mapY; j++) {
			
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
	public String cellFit(String toFit) {
		int spaceToAdd = defaultCellLength - toFit.length();
		return toFit + addSpace(spaceToAdd);
	}
	
}
