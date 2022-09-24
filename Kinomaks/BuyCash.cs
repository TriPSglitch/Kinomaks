using System.Collections.Generic;
using System.Linq;

namespace Kinomaks
{
    internal class BuyCash : IBuy
    {
        int IDFilm;
        decimal Price;
        int idTimetable;
        int IDHall;
        List<int> numberOfSeats;

        public BuyCash(int idFilm, List<int> numberOfSeats, int idTimetable, int idHall)
        {
            this.IDFilm = idFilm;
            this.numberOfSeats = numberOfSeats;
            this.idTimetable = idTimetable;
            this.IDHall = idHall;
            this.Price = Connection.db.Films.Where(item => item.ID == IDFilm).Select(item => item.Price).FirstOrDefault();
        }

        public void DoBuy()
        {
            foreach (int item in this.numberOfSeats)
            {
                HallTimetable hallTimetable = new HallTimetable()
                {
                    IDHall = this.IDHall,
                    IDTimetable = this.idTimetable,
                    IDUser = User.IDUser,
                    IDPlace = item
                };
                Connection.db.HallTimetable.Add(hallTimetable);
                Connection.db.SaveChanges();
            }
        }
    }
}