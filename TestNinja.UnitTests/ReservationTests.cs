using TestNinja.Fundamentals;
using NUnit.Framework;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();
            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });
            // Assert
            // Assert.IsTrue(result);
            // Assert.That(result = true);
            Assert.That(result, Is.True);
            // Assert is a nice helper class for testing
        }

        [Test]
        public void CanBeCancelledBy_SameUser_ReturnsTrue()
        {
            var user = new User();
            var reservation = new Reservation { MadeBy = user };
            var result = reservation.CanBeCancelledBy(user);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_DifferentUser_ReturnsFalse()
        {
            var user = new User();
            var creator = new User();
            var reservation = new Reservation { MadeBy = creator };
            var result = reservation.CanBeCancelledBy(user);
            Assert.IsFalse(result);
        }
    }
}
