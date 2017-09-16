namespace environment {

	public abstract class Structure : TileCover {

		int value;

		public Structure(String name, char symbol) : base(name, symbol) {}
		
		public int getValue() { return value; }

	}
}