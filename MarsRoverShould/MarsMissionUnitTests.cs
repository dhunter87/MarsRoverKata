using System;
using NUnit.Framework;

namespace MarsRoverKataUnitTests
{
	[TestFixture]
	public class MarsMissionUnitTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void MarsMissionShould_Not_Be_Null_When_Initialised()
		{
			var masrMission = new MarsMission();

			Assert.That(masrMission, Is.Not.Null);
		}
	}

    internal class MarsMission
    {
        public MarsMission()
        {
			throw new NotImplementedException();
        }
    }
}

