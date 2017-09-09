package environment;

import java.awt.Point;

import player.Player;
import player.Team;
import system.GameObjectList;
import static environment.TileCover.EMPTY_COVER;

/**
 * A class representing a square element of space that can be occupied with 0..n elements or objects
 * @author Alex White
 *
 */
public class Tile {
	
	char symbol = '-';
	Point coordinates;
	GameObjectList gameObjects;
	private TileCover cover = EMPTY_COVER;
	private int inherentValue;
	Player occupyingPlayer; // not sure how this should be implemented; we'll see
	private Team owner;
	
	public Tile(Point assignedCoords) {
		this(assignedCoords, EMPTY_COVER);
	}
	
	public Tile(Point assignedCoords, TileCover cover) {
		this.coordinates = assignedCoords;
		this.gameObjects = new GameObjectList();
		this.cover = cover;
	}
	
	public Point getCoordinates() { return coordinates; }
	
	public TileCover getCover() { return cover; }
	
	/**
	 * Removes the cover of the tile, makes the reference to the {@code TileCover} null
	 * @return the {@code TileCover}
	 */
	public TileCover removeCover() {
		TileCover theCover = cover;
		cover = EMPTY_COVER;
		return theCover;
	}
	
	public int getValue() { return inherentValue; }
	
	public boolean hasCover() { return cover != EMPTY_COVER; }
	
	public char getSymbol() {
		if (occupyingPlayer != null) {
			return occupyingPlayer.getSymbol();
		} else if (hasCover()) {
			return cover.symbol;
		} else {
			return symbol;
		}
	}
	
	public void addPlayer(Player player) throws TileOccupiedException {
		if (occupyingPlayer != null || !cover.isTraversible()) {
			throw new TileOccupiedException();
		}
		occupyingPlayer = player;
	}
	
	public void removePlayer() { occupyingPlayer = null; }
	
	public Team getOwner() { return owner; }
	public void setOwner(Team newOwner) { this.owner = newOwner; }
	
	public class TileOccupiedException extends Exception {
		public TileOccupiedException() {
			super("Tile occupied!");
		}
	}

}
