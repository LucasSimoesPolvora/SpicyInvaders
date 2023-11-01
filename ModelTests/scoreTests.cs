using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestClass()]
    public class scoreTests
    {
        [TestMethod()]
        public void doesTheScoreUpdate()
        {
            // Arrange
            score score = new score();

            // act
            score.update();

            // Assert
            Assert.AreEqual(score.ScoreValue, 40);
        }

        [TestMethod()]
        public void doesTheDeadValueApply()
        {
            // arrange
            score score = new score();

            // act
            score.downADeadValue();

            score.update();

            // assert
            Assert.AreEqual(score.writeScore(), $"Score : 39");
        }
    }
}