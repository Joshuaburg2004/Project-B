public class Customer : Account
{
    public Customer(string name, string email, string password) : base(name, email, password) { }

    public void Add_Reservation(int table, int guest, string date, string time)
    {

        Reservation.Add_Reservation(new Reservation(Account.GetAccount(this.id), table, guest, date, time));
    }

    public void View_Reservation()
    {

    }
}