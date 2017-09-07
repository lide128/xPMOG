package environment;


/**
 * A class that represents the randomly generated game play area
 * Which is a 10 (y axis) by 15 (x axis) field of game squares to be populated for game space
 * @author Alex White 
 *
 */
public class GameMap {
	
	int mapX = 15;
	int mapY = 10;
	int defaultCellLength = 10;
	char horizontalLineChar = '-';
	char unexcavatedTile = 'X';
	Tile gameMap[][] = new Tile[mapX][mapY];
	
	/**
	 * A method which prints out the gameMap showing all of the Tiles and the first GameObject they contain.
	 * @param toPrint
	 */
	public void printMap() {
		String unexcavated = cellFit(unexcavatedTile + "");
		for(int i = 0; i < (mapY * 2)+1; i++) {
			System.out.print('|');
			
			for(int j = 0; j < mapX; j++) {
				if(i % 2 != 0) {
					System.out.print(unexcavated);
				}
				else {
					printGridLine(defaultCellLength);
				}
				System.out.print('|');
			}
			System.out.println();
		}
	}
	
	public void printGridLine(int num) {
		if(num > 0) {
			System.out.print(horizontalLineChar);
			printGridLine(num-1);
		}
	}
	
	
	public String addSpaceOther(int num) {
		String space = "";
		for(int i = 0; i < num; i++) {
			space += ' ';
		}
		return space;
	}
	
	public String cellFit(String toFit) {
		int spaceToAdd = defaultCellLength - toFit.length();
		return toFit + addSpaceOther(spaceToAdd);
	}
	
}
