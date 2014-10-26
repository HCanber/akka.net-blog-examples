using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace CalculatorApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var system = ActorSystem.Create("calculator-system");
			var calculator = system.ActorOf<CalculatorActor>("calculator");
			var answer = calculator.Ask<Answer>(new Add(1, 2)).Result;
			Console.WriteLine("Answer: " + answer.Value);
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}
	}

	public class CalculatorActor : ReceiveActor
	{
		public CalculatorActor()
		{
			Receive<Add>(add => Sender.Tell(new Answer(add.Term1 + add.Term2)));
		}
	}

	public class Add
	{
		private readonly double _term1;
		private readonly double _term2;

		public Add(double term1, double term2)
		{
			_term1 = term1;
			_term2 = term2;
		}

		public double Term1 { get { return _term1; } }
		public double Term2 { get { return _term2; } }
	}

	public class Answer
	{
		private readonly double _value;

		public Answer(double value)
		{
			_value = value;
		}

		public double Value { get { return _value; } }
	}
}
