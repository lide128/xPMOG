namespace environment {

	public class Dirt : TileCover {

		public Dirt() {
			super("Dirt", 'X');
		}
		
		@Override
		public boolean isDiggable() { return true; }
		
	}
}