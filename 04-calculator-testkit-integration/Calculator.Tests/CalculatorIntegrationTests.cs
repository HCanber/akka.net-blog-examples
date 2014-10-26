using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.Xunit;
using CalculatorApp;
using Xunit;

namespace Calculator.Tests
{
	public class CalculatorIntegrationTests : TestKit
	{
		[Fact]
		public void Answer_should_initially_be_0()
		{
			var calculator = ActorOf<CalculatorActor>("calculator");
			calculator.Tell(GetLastAnswer.Instance);

			var answer = ExpectMsg<Answer>();		//Wait up to 3 seconds for the answer to arrive 
			Assert.Equal(0, answer.Value);

			//or
			//ExpectMsg<Answer>(a => a.Value == 0);
		}

		[Fact]
		public void After_adding_1_and_1_Answer_should_be_2()
		{
			var calculator = ActorOf<CalculatorActor>("calculator");

			calculator.Tell(new Add(1, 1));
			var answer = ExpectMsg<Answer>();		//Wait up to 3 seconds for the answer to arrive 

			Assert.Equal(2, answer.Value);
		}
	}
}