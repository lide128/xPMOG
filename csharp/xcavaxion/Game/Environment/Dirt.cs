namespace environment {

	public class Dirt : TileCover {

		public Dirt() : base("Dirt", 'X') {}
		
		public boolean isDiggable() { return true; }
		
	}
}