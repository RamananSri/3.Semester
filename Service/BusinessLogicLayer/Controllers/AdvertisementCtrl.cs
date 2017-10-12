using System;
using ModelLayer;
using DataAccessLayer;
using System.Collections.Generic;
using System.ServiceModel;

namespace BusinessLogicLayer
{
    public class AdvertisementCtrl
    {
        private readonly IAdvertisementDB _adDb;
        private readonly IBicycleDB _bikeDb;
        private readonly IUserDB _userDb;


        public AdvertisementCtrl()
        {
            _adDb = new AdvertisementDB();
            _bikeDb = new BicycleDB();
            _userDb = new UserDB();
        }

        public Advertisement FindAdById(int id)
        {
            try
            {
                Advertisement advertisement = _adDb.FindAdById(id);
                return advertisement;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateAd(string title, string description, double price, DateTime startDate, DateTime endDate, int? bikeId, int? userId)
        {
            var b = _bikeDb.Find(bikeId);
            var u = _userDb.GetUser(userId);
            try
            {
                if (b != null && u != null)
                {
                    if (price >= 0 && startDate >= DateTime.Now.Date && endDate >= startDate)
                    {
                        var advertisment = new Advertisement
                        {
                            Title = title,
                            Description = description,
                            Price = price,
                            StartDate = startDate,
                            EndDate = endDate,
                            BikeId = bikeId,
                            UserID = userId,
                        };
                        _adDb.AddAd(advertisment);
                    }
                    else
                    {
                        throw new FaultException("Please check if your given information is correct");
                    }
                }
                else
                {
                    throw new FaultException("No user and/or bicycle was found");
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifyAd(int id, string title, string description, double price, DateTime startDate, DateTime endDate, int? bikeId, int? userId)
        {
            Advertisement ad = _adDb.FindAdById(id);

            ad.Title = title;
            ad.Description = description;
            ad.Price = price;
            ad.StartDate = startDate;
            ad.EndDate = endDate;
            ad.BikeId = bikeId;
            ad.UserID = userId;

            _adDb.ModifyAd(ad);
        }

        public void RemoveAd(int id)
        {
            _adDb.RemoveAd(id);
        }

        public Advertisement GetAdByTitle(string title)
        {
            try
            {
                Advertisement ads = _adDb.GetAdByTitle(title);
                return ads;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Advertisement> GetAdvertisementsByUser(int Id)
        {
            try
            {
                List<Advertisement> ads = _adDb.GetAdvertisementsByUser(Id) ?? new List<Advertisement>();

                return ads;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Advertisement> GetAllAds()
        {
            try
            {
                List<Advertisement> adsList = _adDb.GetAllAds() ?? new List<Advertisement>();

                return adsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Advertisement> GetAvailableAds(DateTime start, DateTime end)
        {
            return _adDb.GetAvailableAds(start,end);
        }

        public void RemoveAdsByBikeId(int bicycleId)
        {
            _adDb.RemoveAdsByBikeId(bicycleId);
        }
    }
}