using System.Linq;
using NUnit.Framework;

namespace SnakesAndLadders
{
    public class GivenAGameDieWhenRolled
    {
        [Test, Repeat(1000)]
        public void ThenAnyRollFallsBetweenOneAndSix()
        {
            const int sides = 6;
            Assert.That(new GameDie(sides).Roll(), Is.GreaterThanOrEqualTo(1).And.LessThanOrEqualTo(sides));
        }

        [Test, Repeat(10)]
        public void ThenThereIsAnEvenChanceOfGettingEachDiceValue()
        {
            const int rolls = 120000;
            var results = new int[rolls];
            const int sides = 6;
            var dice = new GameDie(sides);

            for (var i = 0; i < rolls; i++)
            {
                results[i] = dice.Roll();
            }
            var sequence = Enumerable.Range(1, sides).ToArray();
            Assert.That(results.SkipWhile(r => r != 1).Take(sides * 2).ToArray(),
                        Is.Not.EquivalentTo(sequence.Concat(sequence)),
                        "Sequential values returned");

            var groupings = results.GroupBy(r => r).ToArray();
            Assert.That(groupings.Count(),
                        Is.EqualTo(sides),
                        "Not all sides were returned");

            foreach (var group in groupings)
            {
                Assert.That(group.Count(),
                            Is.EqualTo(rolls/sides).Within(2).Percent,
                            "Group {0}", group.Key);
            }
        }
    }
}