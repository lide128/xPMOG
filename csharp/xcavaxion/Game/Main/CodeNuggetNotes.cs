using System;

namespace Main {

	public class CodeNuggetNotes {

		public static class Operation {
			CodeNugget nugget;
			Object param;
			public Operation(CodeNugget nugget, Object parameter) {
				this.nugget = nugget;
				this.param = parameter;
			}
		}
		
		public static enum CodeNugget {
			MoveForward(RobotMover::moveRobotForward),
			Dig(RobotMover::dig),
			TurnLeft(RobotMover::turnLeft),
			TurnRight(RobotMover::turnRight),
			InventoryFull(RobotMover::isInventoryFull),
			HasElement(RobotMover::hasElement),
			GoHome(RobotMover::goHome),
			;
			
			BiPredicate<Robot, Object> func;
			
			private CodeNugget(BiPredicate<Robot, Object> func) {
				this.func = func;
			}
			
			public BiPredicate<Robot, Object> getFunc() { return func; }
		}
		
		public class Robot {
			
			void executeThePlayersCode(List<Operation> steps) {
				for (Operation op : steps) {
					CodeNugget nugget = op.nugget;
					nugget.getFunc().test(this, op.param);
					
				}
			}
			
			// TODO implement basic actions
			void moveForward() {}
			void dig() {}
			void turnLeft() {}
			void turnRight() {}
			void isInventoryFull() {}
			void hasElement() {}
			void goHome() {}
		}
		
		public static class RobotMover {
			static Boolean moveRobotForward(Robot robot, Object param) {
				try { robot.moveForward(); } catch (Exception e) { return false; }
				return true;
			}
			static Boolean dig(Robot robot, Object param) {
				try { robot.dig(); } catch (Exception e) { return false; }
				return true;
			}
			static Boolean turnLeft(Robot robot, Object param) {
				try { robot.turnLeft(); } catch (Exception e) { return false; }
				return true;
			}
			static Boolean turnRight(Robot robot, Object param) {
				try { robot.turnRight(); } catch (Exception e) { return false; }
				return true;
			}
			static Boolean isInventoryFull(Robot robot, Object param) {
				try { robot.isInventoryFull(); } catch (Exception e) { return false; }
				return true;
			}
			static Boolean hasElement(Robot robot, Object param) {
				try { robot.hasElement(); } catch (Exception e) { return false; }
				return true;
			}
			static Boolean goHome(Robot robot, Object param) {
				try { robot.goHome(); } catch (Exception e) { return false; }
				return true;
			}
		}
		
	}
}