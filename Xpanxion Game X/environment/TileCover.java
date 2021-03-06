package environment;

import system.GameObject;
import system.GameObjectList;

public abstract class TileCover {
	
	private String symbol;
	protected GameObjectList gameObjects;
	private String name;
	
	/** Instance representing no tile cover at all */
	public static final TileCover EMPTY_COVER = new TileCover("Empty Cover", " "){
		@Override
		public boolean isTraversible() { return true; }
	};
	
	public TileCover(String name, String symbol) {
		this.name = name;
		this.symbol = symbol;
		gameObjects = new GameObjectList();
	}
	
	public String getName() { return name; }
	
	public String getSymbol() { return symbol; }

	public GameObjectList getContents() { return gameObjects; }
	
	void addContents(GameObject obj) { gameObjects.add(obj); }
	
	/**
	 * Subclasses can override this method
	 * 
	 * @return {@code true} if a {@link player.Player Player} can occupy the same {@link Tile} as this {@code TileCover}
	 */
	public boolean isTraversible() { return false; }

	/**
	 * Subclasses can override this method
	 * 
	 * @return {@code true} if this tile cover can be dug out
	 */
	public boolean isDiggable() { return false; }
}
