using Akka.TestKit;
using Akka.TestKit.Xunit;
using CalculatorApp;
using Xunit;

namespace Calculator.Tests
{
	public class CalculatorUnitTests : TestKit
	{
		[Fact]
		public void Answer_should_initially_be_0()
		{			
			var calculatorRef = ActorOfAsTestActorRef<CalculatorActor>("calculator");
			var calculator = calculatorRef.UnderlyingActor;
			Assert.Equal(0, calculator.Answer);
		}

		[Fact]
		public void After_adding_1_and_1_Answer_should_be_2()
		{
			TestActorRef<CalculatorActor> calculatorRef = ActorOfAsTestActorRef<CalculatorActor>("calculator");
			calculatorRef.Tell(new Add(1,1));
			CalculatorActor calculator = calculatorRef.UnderlyingActor;
			Assert.Equal(2, calculator.Answer);
		}
	}
}
