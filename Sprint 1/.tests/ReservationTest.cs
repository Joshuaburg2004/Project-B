//Gemaakt door Alperen en Aymane
namespace ProjectB.Tests;

[TestClass]
public class ReservationTest
{
    [TestMethod]
    public void TestReservationCreation()
    {
        int customerId = 1;
        int table = 3;
        int guests = 2;
        DateOnly date = new DateOnly(2023, 12, 15);
        string time = "18:30";

        Reservation reservation = new Reservation(customerId, table, guests, date, time);

        Assert.AreEqual(customerId, reservation.CustomerId);
        Assert.AreEqual(table, reservation.Table);
        Assert.AreEqual(guests, reservation.Guests);
        Assert.AreEqual(date, reservation.Date);
        Assert.AreEqual(time, reservation.Time);
    }

    [TestMethod]
    public void TestReservationInfo()
    {
        int customerId = 1;
        int table = 3;
        int guests = 2;
        DateOnly date = new DateOnly(2023, 12, 15);
        string time = "18:30";

        Reservation reservation = new Reservation(customerId, table, guests, date, time);

        string expectedInfo = $"Reservation_ID: {reservation.Reservation_ID}, Table {table}, number of guests: {guests}, Date: {date}, TimeSlot: {time}";

        Assert.AreEqual(expectedInfo, reservation.Reservation_Info());
    }
    
    [TestMethod]
    public void TestReservationInfoWithCustomer()
    {
        int customerId = 1;
        int table = 3;
        int guests = 2;
        DateOnly date = new DateOnly(2023, 12, 15);
        string time = "18:30";

        Reservation reservation = new Reservation(customerId, table, guests, date, time);
        Customer customer = new Customer("John Doe", "john@example.com", "password");

        string expectedInfo = $"{Customer.Info(customer)}, Table: {table}, Guests: {guests}, Date: {date}, Time: {time}";

        Assert.AreEqual(expectedInfo, Reservation.Info(reservation, customer));
    }
}
