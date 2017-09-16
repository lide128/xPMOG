using System;
using system;

namespace Environment {
	public class Base : Structure {

		public Base() : base("Base", 'B')
		{
			this.value = Integer.MAX_VALUE;
		}

		public boolean isTraversible() { return true; }

		public GameObject getObject(GameObject obj) {
			return inventory.get(inventory.indexOf(obj));
		}

		public void storeObject(GameObject obj) {
			inventory.add(obj);
		}

		public void storeAll(GameObjectList objects) {
			inventory.addAll(objects);
		}
	}
}