namespace environment {
	public class Base : Structure {

		public Base() : base("Base", 'B')
		{
			this.value = Integer.MAX_VALUE;
		}

		public boolean isTraversible() { return true; }
		
	}
}