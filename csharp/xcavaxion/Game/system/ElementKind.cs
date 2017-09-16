using System;

namespace system {

	public class ElementKind {

		// densities are real-world densities in kg/m^3
		// NOTE: 1 kg/m^3 = 1000 g/cm^3
		// value-by-weight is in credits per cm^3

		// TODO change relativePrevalence values to something more realistic

		public readonly ElementKind 
			WATER   = new ElementKind("Water", 	"you can drink it", 							"H2O", 	1, 12, 	4, 	1000),
			HELIUM  = new ElementKind("Helium", 	"lighter than air", 							"He", 	5, 50, 	1, 	1), // actually, .164
			GOLD    = new ElementKind("Gold", 	"shiny, rare, conductive, and easily maliable", "Au", 	4, 200, 1, 	19320),
			SILVER  = new ElementKind("Silver", 	"shiny, and less rare", 						"Ag", 	4, 100, 1, 	10490),
			URANIUM = new ElementKind("Uranium", "dense, toxic, and radioactive", 				"U", 	4, 75, 	2, 	19100),
			LEAD    = new ElementKind("Lead", 	"toxic and malliable", 							"Pb", 	3, 30, 	4, 	11340),
			COPPER  = new ElementKind("Copper", 	"conductive, shiny, and easily tarnishes", 		"Cu", 	3, 35, 	3, 	8960),
			IRON    = new ElementKind("Iron", 	"strong, magnetic, and malliable", 				"Fe", 	2, 25, 	4, 	7870),
		CARBON  = new ElementKind("Carbon", 	"forms strong bonds, useful in many forms", 	"C", 	1, 10, 	5, 	2000);

		private readonly String name, description, symbol;
		private readonly int elementDifficulty, valueByWeight, relativePrevalence, density;

		private ElementKind(String elementName, 
			String description, 
			String symbol, 
			int elementDifficulty, 
			int valueByWeight, 
			int relativePrevalence,
			int density) {
			this.name = elementName;
			this.description = description;
			this.symbol = symbol;
			this.elementDifficulty = elementDifficulty;
			this.valueByWeight = valueByWeight;
			this.relativePrevalence = relativePrevalence;
			this.density = density;
		}

		public String getName() { return name; }

		public String getDescription() { return description; }

		public String getSymbol() { return symbol; }

		/** @return time-to-dig in seconds per cm^3 */
		public int getDifficulty() { return elementDifficulty; }

		/** @return value-by-weight of this element in credits per cm^3 */
		public int getValue() { return valueByWeight; }

		public int getPrevalence() { return relativePrevalence; }

		/** @return density of this element in kg/m^3, which is 1000 g/cm^3 */
		public int getDensity() { return density; }

		public static ElementKind randomElementWeighted(Random rand) {
			List<ElementKind> rouletteWheel = new ArrayList<>();
			Consumer<ElementKind> weightedAdd = kind -> {
				for (int i=0; i < kind.getPrevalence() ; i++)
					rouletteWheel.add(kind);
			};
			Arrays.asList(ElementKind.values())
				.stream()
				.forEach(weightedAdd);
			return rouletteWheel.get(rand.nextInt(rouletteWheel.size()));
		}

		// a guard against poor enum maintenance
		public static ElementKind heaviestElement() {
			BinaryOperator<ElementKind> reducer = new BinaryOperator<ElementKind>() {
				@Override
				public ElementKind apply(ElementKind t, ElementKind u) {
					return u.density >= t.density ? u : t;
				}
			};
			return Arrays.asList(ElementKind.values())
				.stream()
				.reduce(reducer)
				.orElseThrow(() -> new RuntimeException("This exception should be impossible"));
		}

		public String toString() { return getName(); }

	}

}