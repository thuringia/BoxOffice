using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoxOffice.Models;

namespace BoxOffice.Events
{
    public class Dispatch
    {
        private static BoxOfficeContext db = new BoxOfficeContext();

        /// <summary>
        /// checks if dvd is available, then dispatches it and sends user a message
        /// </summary>
        /// <param name="dvdRequest">the rental wanting a dvd</param>
        /// <returns>true if dispatched, false if not</returns>
        public static bool RequestDVD(Rental dvdRequest)
        {
            var dvds = db.DVDs.Where(d => d.MovieID == dvdRequest.MovieID && d.State.Equals("available"));

            if (dvds.Any())
            {
                return dispatch(dvdRequest, dvds.First()) && msgSend(dvdRequest.User, dvdRequest.Movie);
            }

            // no DVDs available
            return false;



        }

        /// <summary>
        /// dispatch the dvd
        /// </summary>
        /// <param name="dvdRequest"></param>
        /// <param name="dvds"></param>
        /// <returns></returns>
        private static bool dispatch(Rental dvdRequest, DVD dvd)
        {
            try
            {
                // add dvd to rental
                var therental = db.Rentals.First(r => r.RentalID == dvdRequest.RentalID);
                var theDVD = db.DVDs.First(d => d.DvdID == dvd.DvdID);
                therental.DateSent = DateTime.Now;
                therental.DateDue = DateTime.Now.AddDays(14);
                therental.QueuePosition = null;
                therental.Dvd = dvd;
                therental.DvdID = dvd.DvdID;
                db.SaveChanges();

                theDVD.Rentals.Add(therental);
                theDVD.State = "rented";

                // dispatch
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// send user a message of dispatch
        /// </summary>
        /// <param name="aUser"></param>
        /// <returns></returns>
        private static bool msgSend(User aUser, Movie aMovie)
        {
            try
            {
                // create message
                var usr = db.Users.First(u => u.UserID == aUser.UserID);
                var msg = new Message
                              {
                                  DateSent = DateTime.Now,
                                  User = usr,
                                  UserID = usr.UserID,
                                  Text = string.Format("A DVD of {0} was dispatched to you.", aMovie.Name)
                              };

                // add message to db
                db.Messages.Add(msg);
                db.SaveChanges();

                // add message to user
                usr.Messages.Add(msg);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// handles a returned dvd and realocates it
        /// </summary>
        /// <param name="dvdReturn"></param>
        /// <returns></returns>
        public static bool ReturnDVD(Rental dvdReturn)
        {
            try
            {
                // "return" dvd
                var toReturn = db.Rentals.First(r => r.RentalID == dvdReturn.RentalID);
                toReturn.DateReturned = DateTime.Now;
                toReturn.Dvd.State = "available";
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            // hand over to newt user
            return realocateDVD(dvdReturn.Dvd);
        }

        /// <summary>
        /// realocates a dvd
        /// </summary>
        /// <param name="aDvd"></param>
        /// <returns></returns>
        private static bool realocateDVD(DVD aDvd)
        {
            // get rentals requesting a dvd for this one's movie
            var request = from r in db.Rentals
                          where r.QueuePosition != null && r.DvdID == null
                          orderby r.QueuePosition
                          select r;

            // if request, then realocate
            if (request.Any())
            {
                var theNext = request.First();

                try
                {
                    if (dispatch(theNext, aDvd))
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }


    }
}