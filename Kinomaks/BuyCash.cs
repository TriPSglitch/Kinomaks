using System.Collections.Generic;
using System.Linq;

namespace Kinomaks
{
    internal class BuyCash : IBuy
    {
        int IDFilm;
        decimal Price;
        int idTimetable;
        List<int> numberOfSeats;

        public BuyCash(int idFilm, List<int> numberOfSeats, int idTimetable)
        {
            this.IDFilm = idFilm;
            this.numberOfSeats = numberOfSeats;
            this.idTimetable = idTimetable;
            this.Price = Connection.db.Films.Where(item => item.ID == IDFilm).Select(item => item.Price).FirstOrDefault();
        }

        public void DoBuy()
        {
            foreach (int item in this.numberOfSeats)
            {
                UserTicket userTicket = new UserTicket()
                {
                    IDTimetable = this.idTimetable,
                    IDUser = User.IDUser,
                    IDPlace = item
                };
                Connection.db.UserTicket.Add(userTicket);
                Connection.db.SaveChanges();
            }
        }
    }
}