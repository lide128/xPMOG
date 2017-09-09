package environment;

import system.GameObjectList;

public abstract class TileCover {
	
	protected char symbol;
	protected GameObjectList gameObjects;
	public static final TileCover EMPTY_COVER = new TileCover(){
		@Override
		public boolean isTraversible() { return true; }
	};
	
	public TileCover() {
		gameObjects = new GameObjectList();
	}
	
	public GameObjectList getContents() { return gameObjects; }
	
	/**
	 * Subclasses can override this method
	 * 
	 * @return {@code true} if a {@link player.Player Player} can occupy the same {@link Tile} as this {@code TileCover}
	 */
	public boolean isTraversible() { return false; }
}
