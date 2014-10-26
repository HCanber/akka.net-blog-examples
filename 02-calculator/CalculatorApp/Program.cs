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
			Console.WriteLine("1 + 2 = " + answer.Value);

			var answerSubtract = calculator.Ask<Answer>(new Subtract(5, 3)).Result;
			Console.WriteLine("5 - 3 = " + answerSubtract.Value);

			var lastAnswer = calculator.Ask<Answer>(GetLastAnswer.Instance).Result;
			Console.WriteLine("Last answer = " + lastAnswer.Value);


			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}
	}

	public class CalculatorActor : ReceiveActor
	{
		public CalculatorActor()
		{
			var answer = 0d;

			Receive<Add>(add =>
			{
				answer = add.Term1 + add.Term2;
				Sender.Tell(new Answer(answer));
			});

			Receive<Subtract>(sub =>
			{
				answer = sub.Term1 - sub.Term2;
				Sender.Tell(new Answer(answer));
			});

			Receive<GetLastAnswer>(m => Sender.Tell(new Answer(answer)));
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

	public class Subtract
	{
		private readonly double _term1;
		private readonly double _term2;

		public Subtract(double term1, double term2)
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

	public class GetLastAnswer
	{
		private static readonly GetLastAnswer _instance = new GetLastAnswer();
		private GetLastAnswer() { }
		public static GetLastAnswer Instance { get { return _instance; } }
	}
}
