using System;

namespace system {


	/**
	 * A class representing resources, map features, and code nuggets that occupy GameSquares
	 * @author Alex White
	 *
	 */
	public class GameElement : GameObject {
		
		private final ElementKind kind;
		private int volume; // cm^3
		
		/**
		 * @param kind
		 * @param volume in cm^3
		 */
		public GameElement(ElementKind kind, int volume) {
			super(kind.getName());
			this.kind = kind;
			this.volume = volume;
		}
		
		public ElementKind getKind() { return kind; }
		
		/**
		 * @param other element to be combined with this one
		 * @return {@code true} if this element successfully absorbed the given element
		 */
		public boolean absorb(GameElement other) {
			if (other.kind != this.kind)
				return false;
			volume += other.volume;
			return true;
		}

		@Override
		public int getWeight() {
			// cm^3 * (1000 g/cm^3) / 1000 = grams
			return volume * kind.getDensity() / 1000;
		}
		
		@Override
		public int getVolume() { return volume; }

		@Override
		public int getValue() {
			return kind.getValue() * getWeight();
		}

	}
}